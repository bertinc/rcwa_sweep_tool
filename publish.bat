@echo off
REM RCWA Sweep Tool - Clean and Publish Script
REM This script cleans the project, restores dependencies, and publishes a Release build

setlocal enabledelayedexpansion

REM Colors and formatting
cls
echo.
echo ============================================
echo   RCWA Sweep Tool - Clean and Publish
echo ============================================
echo.

REM Set variables
set PROJECT_FILE=RCWA_Sweep_Tool.csproj
set FRAMEWORK=net10.0-windows10.0.19041.0
set CONFIGURATION=Release
set OUTPUT_DIR=bin\Publish

REM Check if project file exists
if not exist "%PROJECT_FILE%" (
    echo [ERROR] Project file '%PROJECT_FILE%' not found!
    echo Please run this script from the project root directory.
    pause
    exit /b 1
)

echo [INFO] Project: %PROJECT_FILE%
echo [INFO] Framework: %FRAMEWORK%
echo [INFO] Configuration: %CONFIGURATION%
echo [INFO] Output Directory: %OUTPUT_DIR%
echo.

REM Step 1: Clean the project
echo [STEP 1/4] Cleaning project...
echo.
dotnet clean "%PROJECT_FILE%" -c %CONFIGURATION% -f %FRAMEWORK%
if errorlevel 1 (
    echo [ERROR] Failed to clean the project.
    pause
    exit /b 1
)
echo [SUCCESS] Project cleaned.
echo.

REM Step 2: Restore NuGet packages
echo [STEP 2/4] Restoring NuGet packages...
echo.
dotnet restore "%PROJECT_FILE%"
if errorlevel 1 (
    echo [ERROR] Failed to restore NuGet packages.
    pause
    exit /b 1
)
echo [SUCCESS] NuGet packages restored.
echo.

REM Step 3: Build the project
echo [STEP 3/4] Building project in %CONFIGURATION% mode...
echo.
dotnet build "%PROJECT_FILE%" -c %CONFIGURATION% -f %FRAMEWORK% --no-restore
if errorlevel 1 (
    echo [ERROR] Build failed.
    pause
    exit /b 1
)
echo [SUCCESS] Project built successfully.
echo.

REM Step 4: Publish the project
echo [STEP 4/4] Publishing project...
echo.
dotnet publish "%PROJECT_FILE%" -f %FRAMEWORK% -c %CONFIGURATION% --no-build
if errorlevel 1 (
    echo [ERROR] Publish failed.
    pause
    exit /b 1
)
echo [SUCCESS] Project published successfully.
echo.

REM Final summary
echo ============================================
echo   PUBLISH COMPLETED SUCCESSFULLY
echo ============================================
echo.
echo [INFO] Output location: %OUTPUT_DIR%
echo.
echo [INFO] Next steps:
echo   1. Navigate to: %OUTPUT_DIR%
echo   2. Run: RCWA_Sweep_Tool.bat
echo   3. Share the contents of this folder with others
echo.
echo [INFO] To create a distribution ZIP:
echo   1. Open PowerShell
echo   2. Run: Compress-Archive -Path bin\Publish -DestinationPath RCWA_Sweep_Tool.zip
echo.

REM Open File Explorer to the published files
echo [INFO] Opening File Explorer...
start "" "%cd%\%OUTPUT_DIR%"

pause
