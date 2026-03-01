namespace RCWA_Sweep_Tool;

public static class Constants
{
    private const int MinorVersion = 0;
    private const int BuildVersion = 1;

    public static string Version => $"{DateTime.Now:yyyy.MM.dd}.{MinorVersion}.{BuildVersion}";
}
