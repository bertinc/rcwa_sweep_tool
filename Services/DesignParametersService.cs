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
}
