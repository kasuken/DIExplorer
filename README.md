# DI Explorer

**DI Explorer** is a .NET console application designed to analyze and visualize Dependency Injection (DI) relationships within a .NET solution. By running DI Explorer against your solution, you can easily identify the services registered, their scopes, and dependencies, helping to understand and optimize your application's DI setup.

## Features
- **Dependency Graph**: Automatically discovers and visualizes dependencies between services.
- **Multi-Assembly Analysis**: Runs across all assemblies within a specified folder to provide a comprehensive overview.
- **Service Scopes and Lifetimes**: Identifies the lifetimes (Transient, Scoped, Singleton) of registered services.
- **Console Output**: Simple, readable output directly in the console.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Example Output](#example-output)
- [Contributing](#contributing)
- [License](#license)

## Installation

1. Clone this repository:
   ```bash
   git clone https://github.com/yourusername/DIExplorer.git
   cd DIExplorer
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

## Usage

To analyze the dependency injection structure of a .NET project, run DI Explorer with the path to the project folder:

```bash
dotnet run -- <path_to_project_folder>
```

### Command-Line Arguments
- **`<path_to_project_folder>`**: Path to the folder containing your .NET assemblies (.dll files). DI Explorer will scan all assemblies in this folder and subfolders.

### Example

If your project folder is located at `/myproject/bin/Debug/net8.0/`, you can run DI Explorer as follows:

```bash
dotnet run -- /myproject/bin/Debug/net8.0/
```

## Example Output

Upon running DI Explorer, you will see output similar to this:

```
Loading assemblies from: /myproject/bin/Debug/net6.0/

Processing assembly: /myproject/bin/Debug/net6.0/ProjectA.dll
Found DI configuration method: ConfigureServices in Startup

Processing assembly: /myproject/bin/Debug/net6.0/ProjectB.dll
Found DI configuration method: ConfigureAdditionalServices in Config

Combined Dependency Injection Graph:
ExampleService depends on:
AnotherService depends on:
  - ExampleService
...
```

## Contributing

We welcome contributions! To contribute:

1. **Fork** the repository.
2. **Clone** your fork:
   ```bash
   git clone https://github.com/yourusername/DIExplorer.git
   ```
3. **Create a branch** for your feature:
   ```bash
   git checkout -b feature-name
   ```
4. **Make your changes** and commit:
   ```bash
   git commit -m "Add feature description"
   ```
5. **Push** to your fork and open a **Pull Request**.

Please ensure that any changes align with the project’s purpose and are well-tested.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
```
