namespace DIExplorer;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Loader;

public class DependencyInjectionAnalyzer
{
    private readonly Dictionary<string, List<string>> _dependencyGraph = new();

    public void AnalyzeDependenciesInFolder(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        Console.WriteLine($"\nProcessing assembly: {filePath}");
        var assembly = LoadAssembly(filePath);

        if (assembly != null)
        {
            AnalyzeDependenciesInAssembly(assembly);
        }
        else
        {
            Console.WriteLine($"Failed to load assembly: {filePath}");
        }

        DisplayDependencyGraph();
    }

    private Assembly LoadAssembly(string path)
    {
        try
        {
            var loadContext = new MetadataLoadContext(new PathAssemblyResolver(new[] {path}));
            return loadContext.LoadFromAssemblyPath(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading assembly from {path}: {ex.Message}");
            return null;
        }
    }

    private void AnalyzeDependenciesInAssembly(Assembly assembly)
    {
        var serviceCollectionType = typeof(IServiceCollection);

        foreach (var type in assembly.GetTypes())
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var method in methods)
            {
                if (method.GetParameters().Any(p => p.ParameterType == serviceCollectionType))
                {
                    Console.WriteLine($"Found DI configuration method: {method.Name} in {type.Name}");
                    AnalyzeServiceRegistrations(method);
                }
            }
        }
    }

    private void AnalyzeServiceRegistrations(MethodInfo method)
    {
        var serviceCollection = new ServiceCollection();
        var instance = Activator.CreateInstance(method.DeclaringType);

        method.Invoke(instance, new object[] {serviceCollection});

        foreach (var descriptor in serviceCollection)
        {
            var serviceName = descriptor.ServiceType.Name;
            if (descriptor.ImplementationType != null)
            {
                var dependencies = GetConstructorParameters(descriptor.ImplementationType);
                _dependencyGraph[serviceName] = dependencies.Select(d => d.Name).ToList();
            }
        }
    }

    private IEnumerable<Type> GetConstructorParameters(Type implementationType)
    {
        var constructor = implementationType.GetConstructors().OrderByDescending(c => c.GetParameters().Length)
            .FirstOrDefault();
        return constructor?.GetParameters().Select(p => p.ParameterType) ?? Enumerable.Empty<Type>();
    }

    public void DisplayDependencyGraph()
    {
        Console.WriteLine("\nCombined Dependency Injection Graph:");
        foreach (var service in _dependencyGraph)
        {
            Console.WriteLine($"{service.Key} depends on:");
            foreach (var dependency in service.Value)
            {
                Console.WriteLine($"  - {dependency}");
            }
        }
    }
}