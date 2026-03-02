# RCWA Sweep Tool

A cross-platform .NET MAUI application for performing Rigorous Coupled-Wave Analysis (RCWA) calculations with an intuitive graphical interface.

## Overview

RCWA Sweep Tool is a multi-platform desktop application built with .NET MAUI that provides a user-friendly interface for optical and photonic simulations. It leverages native RCWA computation libraries to perform wavelength and modulation sweep analyses with interactive visualization and analysis capabilities.

## Features

### Core Analysis
- **Wavelength Sweep Analysis**: Perform spectral analysis across a range of wavelengths
- **Modulation Sweep Analysis**: Analyze optical properties across index modulation (ΔN) ranges
- **Automatic Optimization**: Intelligently finds optimal ΔN values that maximize modulation efficiency
- **Native Performance**: Utilizes native DLL for high-performance computations

### Interactive Charts
- **Dual Chart Display**: Simultaneous visualization of efficiency curves and modulation curves
- **Dynamic Titles**: Charts display real-time efficiency percentages and optimal values
- **Vertical Reference Lines**: Red lines mark center wavelength and current Delta N positions
- **Zoom Capability**: Scroll mouse wheel to zoom in/out on charts
- **Touch Support**: Pinch zoom on touch devices
- **Reset Controls**: Dedicated ↺ buttons in bottom-right corner of each chart for quick reset
- **Three-Line Analysis**: S-Pol, P-Pol, and Average efficiency curves simultaneously

### Intelligent Parameter Management
- **Auto-Calculated Fields**: 
  - Spatial Frequency (SF) - auto-calculates from angles and wavelength
  - Angle of Diffraction (AOD) - auto-calculates from grating equation
  - Bragg Tilt - auto-calculates from incidence/diffraction angles
  - Thick Film Threshold - auto-calculates for optical design constraints
- **Dependency Tracking**: Parameters automatically update based on which field is set as readonly
- **Real-Time Validation**: Start/End wavelengths adjust automatically relative to center wavelength

### User Interface
- **Professional Design**: Clean, modern UI with dark mode support
- **Responsive Layout**: Input controls on left, interactive charts on right
- **Persistent Footer**: Status messages and version information always visible
- **Intuitive Controls**: 
  - Radio buttons to toggle readonly fields
  - Helpful tooltips on key controls
  - Consistent styling across all inputs

### Cross-Platform Support
- Runs on Windows, macOS, iOS, and Android
- Respects system dark mode preferences
- Responsive layout adapts to different screen sizes

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

- `wavelength_sweep()`: Analyzes reflectivity across wavelength ranges (S-Pol and P-Pol)
- `modulation_sweep()`: Analyzes reflectivity across modulation parameter ranges

### Frontend Components

- **`Home.razor`**: Blazor component containing the main UI with input controls and chart containers
- **`Home.razor.css`**: Responsive styling with dark mode support using CSS variables
- **`Home.razor.js`**: JavaScript module for Chart.js integration with:
  - Dual interactive charts using Chart.js library
  - Custom plugins for vertical reference lines
  - Zoom plugin for interactive data exploration
  - Real-time chart updates with title calculations

### Calculation Engine

- **`Calc.cs`**: Mathematical utility functions for optical calculations
  - `Precision()`: Consistent rounding across all numeric values
  - `GetSpatialFrequency()`: Calculates SF from angles and wavelength
  - `GetAOD()`: Grating equation solver for angle of diffraction
  - `GetBraggTilt()`: Calculates Bragg tilt from incident/diffracted angles
  - `GetThickFilmThreshold()`: Determines optical design constraint thickness
  - `GetSymetricAngles()`: Calculates symmetric incidence/diffraction angles
  - Trigonometric conversion utilities

- **`Constants.cs`**: Centralized constant management
  - Version information
  - Precision settings for each parameter type (SF_SIG, AOD_SIG, DELTA_SIG, etc.)
  - Safety margins and constraints

## Technical Stack

- **Framework**: .NET MAUI 10
- **Language**: C# 14.0
- **Frontend**: Blazor Web Components with Chart.js
- **Charts**: Chart.js with zoom plugin for interactive visualization
- **Native Integration**: P/Invoke for Windows RCWA DLL
- **Styling**: CSS with CSS custom properties for theming
- **Version Control**: Git

## Usage

### Basic Workflow

1. **Configure Optical Design Parameters**:
   - Spatial Frequency (l/mm) - or set as readonly to auto-calculate
   - Center Wavelength (nm) - primary design wavelength
   - Angle of Incidence (deg) - or let system calculate from grating equation
   - Angle of Diffraction (deg) - or set as readonly to auto-calculate
   - Bulk Index (n) - substrate refractive index
   - Glass Index (n) - optical element refractive index
   - Delta N (Δn) - refractive index modulation depth (0.000-0.200)
   - Harmonic Order - diffraction order (0 or 1)
   - Effective Thickness (µm) - grating thickness
   - Start/End Wavelength (nm) - analysis range

2. **Review Auto-Calculated Values**:
   - Bragg Tilt automatically updates when angles change
   - Thick Film Threshold shows optical design constraint
   - System validates parameter dependencies

3. **Run Analysis**:
   - Click **"Update Charts"** button to perform both sweeps
   - Modulation sweep runs first to find optimal ΔN value
   - If an optimal crossing is found, ΔN automatically updates
   - Wavelength sweep runs with optimized ΔN value

4. **Analyze Results**:
   - **Left Chart (Efficiency Curves)**:
     - Shows S-Pol (dashed red), P-Pol (dashed blue), and Average (green) efficiency curves
     - Red vertical line marks center wavelength
     - Title displays average efficiency % at center wavelength
     - Zoom with mouse wheel to inspect specific wavelength regions
   - **Right Chart (Modulation Curves)**:
     - Shows S-Pol and P-Pol efficiency across Delta N range (0.0-0.15)
     - Red vertical line shows current Delta N value
     - Title displays optimal ΔN value from crossing analysis
     - Useful for finding modulation balance between polarizations

5. **Explore Charts**:
   - **Zoom In**: Scroll mouse wheel on chart area
   - **Zoom Out**: Scroll mouse wheel opposite direction
   - **Reset Zoom**: Click ↺ button in bottom-right corner of chart
   - **Touch Devices**: Pinch to zoom

### Parameter Dependencies

The tool intelligently manages parameter relationships:

- **When SF is Readonly**: Spatial Frequency auto-calculates from angles and wavelength
- **When AOD is Readonly**: Angle of Diffraction auto-calculates from grating equation
- Both modes work independently - choose which constraint makes sense for your design
- Bragg Tilt, Thick Film Threshold, and other derived values update automatically

### Dark Mode

The application automatically respects your system's dark mode preference. If you have dark mode enabled, the interface will use dark colors; otherwise, it uses light colors.

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

## Publishing and Distribution

### Publishing for Windows

The application is configured to publish for Windows 10+ (x64 architecture). The published files are output to `bin\Publish\` with the following structure:

```
bin\Publish\
├── RCWA_Sweep_Tool.bat       ← Run this to start the app
└── AppFiles\
    ├── RCWA_Sweep_Tool.exe
    ├── wwwroot\               # Web assets (CSS, JavaScript)
    └── [dependencies]         # All required runtime files
```

### How to Publish

**Option 1: Using the Command Line**

```bash
# Publish the Release build
dotnet publish -f net10.0-windows10.0.19041.0 -c Release
```

The output will be in: `bin\Publish\`

**Option 2: Using Visual Studio**

1. Right-click the project in Solution Explorer
2. Select **Publish**
3. Configure the publish profile for Windows x64
4. Click **Publish**

### Prerequisites for Running Published App

- **Windows 10** (version 17763) or later, x64
- **.NET 10 Runtime** (must be installed on user's machine)
  - Download from: https://dotnet.microsoft.com/download/dotnet

### How to Run the Published App

1. **Extract the published folder:**
   - Navigate to `bin\Publish\`
   - Copy the entire folder to the desired location (e.g., Program Files)

2. **Launch the app:**
   - Double-click `RCWA_Sweep_Tool.bat`
   - The application window will open

### Distributing to Users

1. **Create a distribution package:**
   ```bash
   # Publish the app
   dotnet publish -f net10.0-windows10.0.19041.0 -c Release
   
   # Zip the bin\Publish\ folder
   # On Windows PowerShell:
   Compress-Archive -Path bin\Publish -DestinationPath RCWA_Sweep_Tool.zip
   ```

2. **Share with users:**
   - Provide the `RCWA_Sweep_Tool.zip` file
   - Include instructions to:
     1. Install .NET 10 Runtime (if not already installed)
     2. Extract the ZIP file
     3. Run `RCWA_Sweep_Tool.bat`

3. **User installation instructions:**
   ```
   1. Download and install .NET 10 Runtime from:
      https://dotnet.microsoft.com/download/dotnet
   
   2. Extract the RCWA_Sweep_Tool.zip file
   
   3. Navigate to the extracted folder
   
   4. Double-click RCWA_Sweep_Tool.bat to run the application
   ```

### Build Configuration Details

The Release build is configured with the following optimizations in `RCWA_Sweep_Tool.csproj`:

- **Trimming**: Partial trimming enabled to reduce output size
- **Debug Symbols**: Removed to reduce file size
- **R2R (Ready2Run)**: Native images pre-compiled for faster startup
- **Publish Directory**: `bin\Publish\AppFiles\` (launcher batch file copied to parent)

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
