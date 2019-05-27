using System;

namespace FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement
{
    public class Converter
    {
        public static decimal ConvertFromByte(SizeUnit targetUnit, decimal size)
        {
            decimal convertedSize;

            switch (targetUnit)
            {
                case SizeUnit.Byte:
                {
                    convertedSize = size;
                    break;
                }
                case SizeUnit.KB:
                {
                    convertedSize = size / 1024;
                    break;
                }
                case SizeUnit.MB:
                {
                    convertedSize = size / (1024 * 1024);
                    break;
                }
                case SizeUnit.GB:
                {
                    convertedSize = size / (1024 * 1024 * 1024);
                    break;
                }
                default:
                {
                    throw new ArgumentException("Unknown unit");
                }
            }

            return convertedSize;
        }
    }
}
