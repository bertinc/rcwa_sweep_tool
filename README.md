# RCWA Sweep Tool

A cross-platform .NET MAUI application for performing Rigorous Coupled-Wave Analysis (RCWA) calculations with an intuitive graphical interface.

## Overview

RCWA Sweep Tool is a multi-platform desktop application built with .NET MAUI that provides a user-friendly interface for optical and photonic simulations. It leverages native RCWA computation libraries to perform wavelength and modulation sweep analyses.

## Features

- **Cross-Platform Support**: Runs on Android, iOS, macOS, and Windows
- **Wavelength Sweep Analysis**: Perform spectral analysis across a range of wavelengths
- **Modulation Sweep Analysis**: Analyze optical properties across modulation parameter ranges
- **Native Performance**: Utilizes native DLL for high-performance computations
- **Intuitive UI**: Clean and responsive interface built with .NET MAUI

## System Requirements

- **.NET 10** or later
- **Platform-Specific Requirements**:
  - **Windows**: Windows 10 (version 17763) or later
  - **iOS**: iOS 15.0 or later
  - **macOS**: macOS 15.0 or later (Catalyst)
  - **Android**: Android 7.0 (API level 24) or later

## Installation

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) with C# extension
- Platform-specific development tools:
  - **Windows**: Windows SDK
  - **macOS**: Xcode
  - **iOS**: Xcode
  - **Android**: Android SDK

### Building from Source

1. Clone the repository:
   ```bash
   git clone https://github.com/bertinc/rcwa_sweep_tool.git
   cd rcwa_sweep_tool
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build for your target platform:
   ```bash
   # For Windows
   dotnet build -f net10.0-windows10.0.19041.0
   
   # For macOS
   dotnet build -f net10.0-maccatalyst
   
   # For iOS
   dotnet build -f net10.0-ios
   
   # For Android
   dotnet build -f net10.0-android
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

## Architecture

### Core Components

- **`App.xaml.cs`**: Application entry point and main window configuration
- **`MainPage.xaml.cs`**: Primary user interface
- **`RcwaDllInterop.cs`**: P/Invoke wrapper for native RCWA DLL functions
- **`MauiProgram.cs`**: MAUI dependency injection and service configuration
- **`Constants.cs`**: Application constants and versioning

### Native Integration

The application interfaces with `rcwa_dll.dll` through P/Invoke for performance-critical computations:

- `wavelength_sweep()`: Analyzes reflectivity across wavelength ranges
- `modulation_sweep()`: Analyzes reflectivity across modulation parameter ranges

## Usage

### Wavelength Sweep

1. Configure optical parameters:
   - Spatial frequency
   - Effective thickness
   - Refractive index modulation (ΔN)
   - Bulk and glass indices
   - Bragg tilt angle
   - Harmonic order

2. Define wavelength range:
   - Center wavelength
   - Start and stop wavelengths
   - Step size

3. Set incident angle (θ)

4. Execute sweep to obtain S and P polarization reflectivity data

### Modulation Sweep

1. Configure optical parameters (same as wavelength sweep)

2. Define modulation range:
   - Start and stop ΔN values
   - Step size

3. Execute sweep to analyze modulation response

## Development

### Project Structure

```
RCWA_Sweep_Tool/
├── App.xaml                    # Application shell
├── App.xaml.cs
├── MainPage.xaml               # Main UI
├── MainPage.xaml.cs
├── MauiProgram.cs              # Configuration
├── RcwaDllInterop.cs           # Native interop
├── Constants.cs                # Constants
├── Platforms/
│   ├── Android/                # Android-specific files
│   ├── iOS/                    # iOS-specific files
│   ├── MacCatalyst/            # macOS-specific files
│   └── Windows/                # Windows-specific files
└── Resources/
    ├── AppIcon/                # App icons
    ├── Splash/                 # Splash screen
    └── Images/                 # App images
```

### Building and Running

```bash
# Restore NuGet packages
dotnet restore

# Build the project
dotnet build

# Run with hot reload (development)
dotnet run

# Build release version
dotnet build -c Release
```

## Dependencies

- **Microsoft.Maui**: Core MAUI framework
- **Microsoft.Maui.Controls**: XAML controls
- **.NET 10 Runtime**: Target framework

## License

[Add your license information here]

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

For issues, questions, or suggestions, please open an [issue](https://github.com/bertinc/rcwa_sweep_tool/issues) on GitHub.

## Authors

- Original Author(s): [Add authors]

## Acknowledgments

- Built with [.NET MAUI](https://github.com/dotnet/maui)
- RCWA algorithm implementation in native DLL

---

**Version**: Dynamic (based on build date)  
**Target Framework**: .NET 10  
**Last Updated**: 2024
