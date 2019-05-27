using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace FoldersAndFilesSizeAnalyzer
{
    /// <summary>
    /// Open dialog allows user to select a disk.
    /// </summary>
    public partial class OpenDialog : Window
    {
        /// <summary>
        ///  The disk that user has selected in this dialog.
        ///  In case user clicked on Cancel button, the value of this property is <see cref="DiskNotSelectedSymbol"/>;
        /// </summary>
        public char SelectedDisk { get; set; }

        public OpenDialog()
        {
            InitializeComponent();

            List<string> ListAllDisks = DriveInfo.GetDrives()
                                        .Select(x => x.Name.Trim('\\'))
                                        .ToList();

            comboBoxDisk.ItemsSource = ListAllDisks;

            if (comboBoxDisk.Items.Count > 0)
                comboBoxDisk.SelectedIndex = 0;
        }

        /// <summary>
        /// Vaue to which <see cref="SelectedDisk" /> is set when user clicks on Cancel button.
        /// </summary>
        public char DiskNotSelectedSymbol = '\0';

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxDisk != null && comboBoxDisk.SelectedItem is string)
                SelectedDisk = ((string)(comboBoxDisk.SelectedItem))[0];

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            SelectedDisk = DiskNotSelectedSymbol;

            DialogResult = false;
            Close();
        }
    }
}
