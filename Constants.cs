namespace RCWA_Sweep_Tool;

public static class Constants
{
    private const int MinorVersion = 0;
    private const int BuildVersion = 1;

    public static string Version => $"{DateTime.Now:yyyy.MM.dd}.{MinorVersion}.{BuildVersion}";

    /// <summary>
    /// Number of significant digits (decimal places) for Spatial Frequency calculations.
    /// </summary>
    public const int SF_SIG = 0;

    /// <summary>
    /// Number of significant digits (decimal places) for Angle of Diffraction calculations.
    /// </summary>
    public const int AOD_SIG = 2;

    /// <summary>
    /// Number of significant digits (decimal places) for Symmetric Angle calculations.
    /// </summary>
    public const int SYMM_ANGLE_SIG = 2;

    /// <summary>
    /// Number of significant digits (decimal places) for Bragg Tilt calculations.
    /// </summary>
    public const int BRAGG_SIG = 2;

    /// <summary>
    /// Number of significant digits (decimal places) for Thick Film Threshold calculations.
    /// </summary>
    public const int TFT_SIG = 2;

    /// <summary>
    /// Number of significant digits (decimal places) for Delta N calculations.
    /// </summary>
    public const int DELTA_SIG = 3;

    /// <summary>
    /// Safety margin (in micrometers) added to Effective Thickness when it needs to exceed Thick Film Threshold.
    /// </summary>
    public const int ET_SAFETY_MARGIN = 2;
}
