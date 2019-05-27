using FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement;
using System;
using System.Globalization;
using System.Windows.Data;
using WinDiskUsageAnalyzer;
using WinDiskUsageAnalyzer.Utils;

namespace FoldersAndFilesSizeAnalyzer
{
    /// <summary>
    /// Converter for Size column in grid in XAML of <see cref="MainWindow"/>.
    /// </summary>
    public class ListViewConverterSize : IValueConverter
    {
        /// <summary>
        /// Convert size in bytes (from cache) into selected <see cref="SizeUnit" />. 
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is long sizeInBytes)
                return FormatForDisplay.FormatSize(size: Converter.ConvertFromByte(targetUnit: MainWindow.SelectedUnit, sizeInBytes), selectedUnit: MainWindow.SelectedUnit);
            else
                return SymbolForUnknown;
        }

        /// <summary>
        /// This method is (currently) not used, but it's a part of <see cref="IValueConverter"/> interface that must be implemented for WPF converter. 
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException(); // This method is (currently) not used.
        }

        private const string SymbolForUnknown = "?";
    }
}
