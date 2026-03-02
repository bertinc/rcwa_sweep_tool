@echo off
REM RCWA Sweep Tool Launcher
REM This batch script launches the RCWA Sweep Tool application

cd /d "%~dp0AppFiles"
start RCWA_Sweep_Tool.exe
exit /b 0
