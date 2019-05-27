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
            return diskSpacePercentage switch
            {
                null                                    => "Unknown disk space percentage",
                decimal d when d >= 0.001m || d == 0m   => Invariant($"{d:##0.###} %"),
                decimal d when d < 0.001m               => "< 0.001 %",
                _                                       => throw new Exception("Cannot format disk space percentage.")
            };
        }
    }
}
