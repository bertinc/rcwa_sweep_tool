using System.Runtime.InteropServices;

namespace RCWA_Sweep_Tool
{
    public static class RcwaDllInterop
    {
        [DllImport("rcwa_dll.dll", CharSet = CharSet.Ansi)]
        public static extern void wavelength_sweep(
            double spatialFreq,
            double effThickness,
            double deltaN,
            double bulkIndex,
            double glassIndex,
            double braggTilt,
            int harmonicOrder,
            double centerWavelength,
            double startWavelength,
            double stopWavelength,
            double stepWavelength,
            double theta,
            [In, Out] double[] returnSPol,
            [In, Out] double[] returnPPol);

        [DllImport("rcwa_dll.dll", CharSet = CharSet.Ansi)]
        public static extern void modulation_sweep(
            double spatialFreq,
            double effThickness,
            double deltaN,
            double bulkIndex,
            double glassIndex,
            double braggTilt,
            int harmonicOrder,
            double centerWavelength,
            double startDelta,
            double stopDelta,
            double stepDelta,
            double theta,
            [In, Out] double[] returnSPol,
            [In, Out] double[] returnPPol);
    }
}
