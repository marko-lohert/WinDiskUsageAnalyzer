using System;

namespace FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement
{
    public class Converter
    {
        public static decimal ConvertFromByte(SizeUnit targetUnit, decimal size)
        {
            return targetUnit switch
            {
                SizeUnit.Byte => size,
                SizeUnit.KB   => size / 1024m,
                SizeUnit.MB   => size / (1024m * 1024),
                SizeUnit.GB   => size / (1024m * 1024 * 1024),
                _             => throw new ArgumentException("Unknown unit")
            };            
        }
    }
}
