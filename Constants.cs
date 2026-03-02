namespace RCWA_Sweep_Tool;

public static class Constants
{
    private const int MinorVersion = 1;
    private const int BuildVersion = 0;

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

    // Physical Designer scaling constants
    /// <summary>
    /// Maximum dimension (in mm) for grating that fits comfortably in preview canvas.
    /// This is the reference dimension for scaling calculations.
    /// </summary>
    public const double PHYSICAL_DESIGNER_MAX_DIMENSION = 250.0;

    /// <summary>
    /// Scale factor for round grating radius in preview (pixels per reference dimension).
    /// Higher value = larger preview. (e.g., 280px radius for 250mm diameter)
    /// </summary>
    public const double PHYSICAL_DESIGNER_RADIUS_SCALE = 280.0 / PHYSICAL_DESIGNER_MAX_DIMENSION;

    /// <summary>
    /// Scale factor for rectangular grating dimensions in preview (pixels per reference dimension).
    /// Higher value = larger preview. (e.g., 550px for 250mm dimension)
    /// </summary>
    public const double PHYSICAL_DESIGNER_RECT_SCALE = 550.0 / PHYSICAL_DESIGNER_MAX_DIMENSION;

    /// <summary>
    /// Maximum preview radius (in pixels) for round gratings.
    /// </summary>
    public const double PHYSICAL_DESIGNER_MAX_RADIUS = 280.0;

    /// <summary>
    /// Maximum preview dimension (in pixels) for rectangular gratings.
    /// </summary>
    public const double PHYSICAL_DESIGNER_MAX_RECT = 550.0;
}
