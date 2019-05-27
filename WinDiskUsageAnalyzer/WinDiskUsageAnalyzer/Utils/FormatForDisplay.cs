using FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement;
using System;
using static System.FormattableString;

namespace WinDiskUsageAnalyzer.Utils
{
    /// <summary>
    /// Utility for formatting different types of values for display.
    /// </summary>
    public class FormatForDisplay
    {
        public static string FormatSize(decimal size, SizeUnit selectedUnit) => Invariant($"{size:##0.##} {selectedUnit}");

        public static string FormatDiskSpacePercentage(decimal? diskSpacePercentage)
        {
            switch (diskSpacePercentage)
            {
                case null:
                    {
                        return "Unknown disk space percentage";
                    }
                case decimal d when d >= 0.001m || d == 0m:
                    {
                        return Invariant($"{d:##0.###} %");
                    }
                case decimal d when d < 0.001m:
                    {
                        return "< 0.001 %";
                    }
                default:
                    {
                        throw new Exception("Cannot format disk space percentage.");
                    }
            }
        }
    }
}
