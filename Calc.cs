namespace RCWA_Sweep_Tool;

/// <summary>
/// Utility class for mathematical calculations and conversions.
/// </summary>
public static class Calc
{
    /// <summary>
    /// Converts an angle from radians to degrees.
    /// </summary>
    /// <param name="radians">The angle in radians.</param>
    /// <returns>The angle in degrees.</returns>
    public static double Degrees(double radians)
    {
        return radians * 180 / Math.PI;
    }

    /// <summary>
    /// Converts an angle from degrees to radians.
    /// </summary>
    /// <param name="degrees">The angle in degrees.</param>
    /// <returns>The angle in radians.</returns>
    public static double Radians(double degrees)
    {
        return degrees * Math.PI / 180;
    }

    /// <summary>
    /// Calculates the spatial frequency based on angles of incidence and diffraction, and center wavelength.
    /// </summary>
    /// <param name="aoi">Angle of Incidence in degrees.</param>
    /// <param name="aod">Angle of Diffraction in degrees.</param>
    /// <param name="cw">Center Wavelength in nanometers.</param>
    /// <returns>The spatial frequency in lines per millimeter, rounded to the number of significant digits defined by Constants.SF_SIG.</returns>
    public static double GetSpatialFrequency(double aoi, double aod, double cw)
    {
        return Precision((Math.Sin(Radians(aoi)) + Math.Sin(Radians(aod))) / (cw * 1e-6), Constants.SF_SIG);
    }

    /// <summary>
    /// Rounds a double value to a specified number of decimal places.
    /// </summary>
    /// <param name="value">The value to round.</param>
    /// <param name="decimalPlaces">The number of decimal places (0 or more).</param>
    /// <returns>The rounded value.</returns>
    public static double Precision(double value, int decimalPlaces)
    {
        return Math.Round(value, decimalPlaces);
    }

    /// <summary>
    /// Calculates the Angle of Diffraction (AOD) using the standard grating equation.
    /// </summary>
    /// <param name="sf">Spatial Frequency in lines per millimeter.</param>
    /// <param name="cw">Center Wavelength in nanometers.</param>
    /// <param name="aoi">Angle of Incidence in degrees.</param>
    /// <returns>The Angle of Diffraction in degrees, rounded to the number of significant digits defined by Constants.AOD_SIG.</returns>
    public static double GetAOD(double sf, double cw, double aoi)
    {
        return Precision(Degrees(Math.Asin(sf * cw * 1e-6 - Math.Sin(Radians(aoi)))), Constants.AOD_SIG);
    }

    /// <summary>
    /// Calculates the symmetric angle where AOI equals AOD using the standard grating equation.
    /// </summary>
    /// <param name="sf">Spatial Frequency in lines per millimeter.</param>
    /// <param name="cw">Center Wavelength in nanometers.</param>
    /// <returns>The symmetric angle in degrees (AOI = AOD), rounded to the number of significant digits defined by Constants.SYMM_ANGLE_SIG.</returns>
    public static double GetSymetricAngles(double sf, double cw)
    {
        return Precision(Degrees(Math.Asin(sf * cw * 1e-6 / 2)), Constants.SYMM_ANGLE_SIG);
    }

    /// <summary>
    /// Calculates the Bragg tilt angle based on angles of incidence and diffraction, and bulk index.
    /// The calculation accounts for refraction in the bulk material.
    /// </summary>
    /// <param name="aoi">Angle of Incidence in degrees.</param>
    /// <param name="aod">Angle of Diffraction in degrees.</param>
    /// <param name="bulkIndex">Bulk refractive index.</param>
    /// <returns>The Bragg tilt angle in degrees, rounded to the number of significant digits defined by Constants.BRAGG_SIG.</returns>
    public static double GetBraggTilt(double aoi, double aod, double bulkIndex)
    {
        // Adjust AOI and AOD for refraction in the bulk material
        double adjustedAOI = Degrees(Math.Asin(Math.Sin(Radians(aoi)) / bulkIndex));
        double adjustedAOD = Degrees(Math.Asin(Math.Sin(Radians(aod)) / bulkIndex));
        
        // Calculate Bragg tilt
        double braggTilt = 90 + ((adjustedAOI - adjustedAOD) / 2);
        
        return Precision(braggTilt, Constants.BRAGG_SIG);
    }

    /// <summary>
    /// Calculates the thick film threshold based on spatial frequency, Bragg tilt, bulk index, and center wavelength.
    /// </summary>
    /// <param name="sf">Spatial Frequency in lines per millimeter.</param>
    /// <param name="bt">Bragg Tilt in degrees.</param>
    /// <param name="bi">Bulk refractive index.</param>
    /// <param name="cw">Center Wavelength in nanometers.</param>
    /// <returns>The thick film threshold in micrometers, rounded to the number of significant digits defined by Constants.TFT_SIG.</returns>
    public static double GetThickFilmThreshold(double sf, double bt, double bi, double cw)
    {
        // Calculate fringe spacing
        double fringeSpacing = (1000 / sf) * Math.Sin(Radians(bt));
        
        // Calculate thick film threshold thickness
        double thickness = (5 * bi * (fringeSpacing * fringeSpacing * 1000)) / (Math.PI * cw);
        
        return Precision(thickness, Constants.TFT_SIG);
    }
}
