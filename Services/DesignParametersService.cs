namespace RCWA_Sweep_Tool.Services;

/// <summary>
/// Shared service to persist design parameters across pages
/// </summary>
public class DesignParametersService
{
    public double SpatialFrequency { get; set; } = 1200;
    public double CenterWavelength { get; set; } = 830;
    public double StartWavelength { get; set; } = 760;
    public double EndWavelength { get; set; } = 900;
    public double AngleOfIncidence { get; set; } = 29.87;
    public double AngleOfDiffraction { get; set; } = 29.87;
    public double BraggTilt { get; set; } = 90;
    public double BulkIndex { get; set; } = 1.33;
    public double GlassIndex { get; set; } = 1.444;
    public double DeltaN { get; set; } = 0.112;
    public double HarmonicOrder { get; set; } = 1;
    public double EffectiveThickness { get; set; } = 4;
    public double ThickFilmThreshold { get; set; } = 1.77;
    public string SelectedReadonlyField { get; set; } = "SF";
    public double GlassThickness { get; set; } = 3.0; // Glass thickness in mm

    // Physical Designer properties
    public string GratingShape { get; set; } = "Round"; // "Round" or "Rectangular"
    public double Diameter { get; set; } = 25.4; // mm (1 inch default)
    public double Width { get; set; } = 25.4; // mm
    public double Height { get; set; } = 25.4; // mm

    /// <summary>
    /// Save current values as user defaults using MAUI Preferences API
    /// </summary>
    public void SaveAsDefaults()
    {
        Preferences.Set("Default_SpatialFrequency", SpatialFrequency);
        Preferences.Set("Default_CenterWavelength", CenterWavelength);
        Preferences.Set("Default_StartWavelength", StartWavelength);
        Preferences.Set("Default_EndWavelength", EndWavelength);
        Preferences.Set("Default_AngleOfIncidence", AngleOfIncidence);
        Preferences.Set("Default_AngleOfDiffraction", AngleOfDiffraction);
        Preferences.Set("Default_BulkIndex", BulkIndex);
        Preferences.Set("Default_GlassIndex", GlassIndex);
        Preferences.Set("Default_DeltaN", DeltaN);
        Preferences.Set("Default_HarmonicOrder", HarmonicOrder);
        Preferences.Set("Default_EffectiveThickness", EffectiveThickness);
        Preferences.Set("Default_GlassThickness", GlassThickness);
        Preferences.Set("Default_SelectedReadonlyField", SelectedReadonlyField);
        Preferences.Set("Default_GratingShape", GratingShape);
        Preferences.Set("Default_Diameter", Diameter);
        Preferences.Set("Default_Width", Width);
        Preferences.Set("Default_Height", Height);
    }

    /// <summary>
    /// Load user defaults from MAUI Preferences API, falling back to hardcoded defaults
    /// </summary>
    public void LoadDefaults()
    {
        SpatialFrequency = Preferences.Get("Default_SpatialFrequency", 1200.0);
        CenterWavelength = Preferences.Get("Default_CenterWavelength", 830.0);
        StartWavelength = Preferences.Get("Default_StartWavelength", 760.0);
        EndWavelength = Preferences.Get("Default_EndWavelength", 900.0);
        AngleOfIncidence = Preferences.Get("Default_AngleOfIncidence", 29.87);
        AngleOfDiffraction = Preferences.Get("Default_AngleOfDiffraction", 29.87);
        BulkIndex = Preferences.Get("Default_BulkIndex", 1.33);
        GlassIndex = Preferences.Get("Default_GlassIndex", 1.444);
        DeltaN = Preferences.Get("Default_DeltaN", 0.112);
        HarmonicOrder = Preferences.Get("Default_HarmonicOrder", 1.0);
        EffectiveThickness = Preferences.Get("Default_EffectiveThickness", 4.0);
        GlassThickness = Preferences.Get("Default_GlassThickness", 3.0);
        SelectedReadonlyField = Preferences.Get("Default_SelectedReadonlyField", "SF");
        GratingShape = Preferences.Get("Default_GratingShape", "Round");
        Diameter = Preferences.Get("Default_Diameter", 25.4);
        Width = Preferences.Get("Default_Width", 25.4);
        Height = Preferences.Get("Default_Height", 25.4);
        
        // BraggTilt and ThickFilmThreshold are calculated, not saved
    }
}
