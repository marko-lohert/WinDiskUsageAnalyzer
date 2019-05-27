using FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement;
using System.Collections.Generic;
using System.Windows;
using static FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement.SizeUnit;

namespace FoldersAndFilesSizeAnalyzer.Dialogs
{
    /// <summary>
    /// Options dialog allows user to customize this app.
    /// </summary>
    public partial class OptionsDialog : Window
    {
        /// <summary>
        /// The unit that user has selected in this dialog.
        /// </summary>
        public SizeUnit SelectedUnit { get; private set; }

        public OptionsDialog()
        {
            InitializeComponent();

            List<SizeUnit> ListAllUnits = new List<SizeUnit>
            {
                Byte,
                KB,
                MB,
                GB
            };

            comboBoxUnit.ItemsSource = ListAllUnits;

            if (comboBoxUnit.Items.Count > 0)
                comboBoxUnit.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            SelectedUnit = (SizeUnit)comboBoxUnit.SelectedValue;

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            SelectedUnit = Unknown;

            DialogResult = false;
            Close();
        }
    }
}
