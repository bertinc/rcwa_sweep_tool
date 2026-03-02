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

    /// <summary>
    /// Calculates the refraction angle when light passes from one medium to another using Snell's Law.
    /// </summary>
    /// <param name="incidentAngle">The incident angle in degrees.</param>
    /// <param name="n1">Refractive index of the first medium.</param>
    /// <param name="n2">Refractive index of the second medium.</param>
    /// <returns>The refraction angle in degrees.</returns>
    public static double GetRefractionAngle(double incidentAngle, double n1, double n2)
    {
        // Snell's Law: n1 * sin(θ1) = n2 * sin(θ2)
        // θ2 = arcsin((n1/n2) * sin(θ1))
        double incidentRad = Radians(incidentAngle);
        double refractionRad = Math.Asin((n1 / n2) * Math.Sin(incidentRad));
        return Degrees(refractionRad);
    }

    /// <summary>
    /// Calculates the total beam displacement (lateral offset) through a glass substrate
    /// accounting for refraction at both incident and diffracted angles.
    /// </summary>
    /// <param name="aoi">Angle of Incidence in degrees (in air).</param>
    /// <param name="aod">Angle of Diffraction in degrees (in air).</param>
    /// <param name="glassThickness">Glass thickness in millimeters.</param>
    /// <param name="glassIndex">Refractive index of glass.</param>
    /// <returns>Total lateral beam displacement in millimeters.</returns>
    public static double GetBeamDisplacement(double aoi, double aod, double glassThickness, double glassIndex)
    {
        // Calculate refraction angles in glass using Snell's law
        double angleInGlassIncident = GetRefractionAngle(aoi, 1.0, glassIndex);
        double angleInGlassDiffracted = GetRefractionAngle(aod, 1.0, glassIndex);
        
        // Calculate lateral displacement through glass for each beam
        double offset1 = glassThickness * Math.Tan(Radians(angleInGlassIncident));
        double offset2 = glassThickness * Math.Tan(Radians(angleInGlassDiffracted));
        
        // Return total displacement (worst case, both offsets add up)
        return Math.Abs(offset1) + Math.Abs(offset2);
    }

    /// <summary>
    /// Finds the index of the crossing point with the highest value in two arrays.
    /// A crossing occurs when the difference between array values changes sign.
    /// </summary>
    /// <param name="array1">First data array.</param>
    /// <param name="array2">Second data array.</param>
    /// <returns>The index of the crossing with the highest value, or -1 if no crossing is found.</returns>
    public static int FindHighestValueCrossing(double[] array1, double[] array2)
    {
        if (array1.Length != array2.Length || array1.Length < 2)
            return -1;

        int highestCrossingIndex = -1;
        double highestValue = double.MinValue;
        
        // Find all crossings between the two arrays
        for (int i = 0; i < array1.Length - 1; i++)
        {
            // Check if there's a crossing between point i and i+1
            // A crossing occurs when the difference changes sign
            double diff1 = array1[i] - array2[i];
            double diff2 = array1[i + 1] - array2[i + 1];
            
            // If signs are different (diff1 * diff2 < 0), there's a crossing
            if (diff1 * diff2 < 0)
            {
                // Find the highest value at this crossing point
                double maxAtCrossing = Math.Max(array1[i], array2[i]);
                if (maxAtCrossing > highestValue)
                {
                    highestValue = maxAtCrossing;
                    highestCrossingIndex = i;
                }
            }
        }
        
        return highestCrossingIndex;
    }

    /// <summary>
    /// Finds the index of the crossing point with the lowest value in two arrays.
    /// A crossing occurs when the difference between array values changes sign.
    /// </summary>
    /// <param name="array1">First data array.</param>
    /// <param name="array2">Second data array.</param>
    /// <returns>The index of the crossing with the lowest value, or -1 if no crossing is found.</returns>
    public static int FindLowestValueCrossing(double[] array1, double[] array2)
    {
        if (array1.Length != array2.Length || array1.Length < 2)
            return -1;

        int lowestCrossingIndex = -1;
        double lowestValue = double.MaxValue;
        
        // Find all crossings between the two arrays
        for (int i = 0; i < array1.Length - 1; i++)
        {
            // Check if there's a crossing between point i and i+1
            // A crossing occurs when the difference changes sign
            double diff1 = array1[i] - array2[i];
            double diff2 = array1[i + 1] - array2[i + 1];
            
            // If signs are different (diff1 * diff2 < 0), there's a crossing
            if (diff1 * diff2 < 0)
            {
                // Find the lowest value at this crossing point
                double maxAtCrossing = Math.Max(array1[i], array2[i]);
                if (maxAtCrossing < lowestValue)
                {
                    lowestValue = maxAtCrossing;
                    lowestCrossingIndex = i;
                }
            }
        }
        
        return lowestCrossingIndex;
    }
}
