using FoldersAndFilesSizeAnalyzer;
using FoldersAndFilesSizeAnalyzer.Dialogs;
using FoldersAndFilesSizeAnalyzer.Entities;
using FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement;
using System.Windows;
using System.Windows.Controls;
using WinDiskUsageAnalyzer.Dialogs;
using WinDiskUsageAnalyzer.Utils;

namespace WinDiskUsageAnalyzer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SelectedUnit = defaultUnit;
            if (this.IsInitialized)
                ((GridView)listAll.View).Columns[indexColumnSize].Header = $"Size [{SelectedUnit}]";
        }
        
        Disk? CurrentDisk { get; set; }
        Folder? CurrentFolder { get; set; }
        public static SizeUnit SelectedUnit { get; set; }

        /// <summary>
        /// Default unit for displaying size of folders anf files.
        /// </summary>
        private const SizeUnit defaultUnit = SizeUnit.MB;

        /// <summary>
        /// Index of column "Size" in a grid.
        /// </summary>
        private const int indexColumnSize = 3;

        private void FileOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog dialog = new OpenDialog();
            bool? pressedOk = dialog.ShowDialog();
            if (pressedOk == true)
            {
                Analyzer analyzer = new Analyzer();
                analyzer.AnalyzeDisk(dialog.SelectedDisk);
                DisplayDisk(dialog.SelectedDisk);
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToolsOptionsMenu_Click(object sender, RoutedEventArgs e)
        {
            OptionsDialog options = new OptionsDialog();
            bool? dialogResult = options.ShowDialog();
            if (dialogResult == true)
            {
                SelectedUnit = options.SelectedUnit;
                ((GridView)listAll.View).Columns[indexColumnSize].Header = $"Size [{SelectedUnit}]";
                if (CurrentFolder != null)
                {
                    listAll.ItemsSource = null;
                    DisplayFolder(CurrentFolder);
                }
            }
        }

        private void HelpAboutMenu_Click(object sender, RoutedEventArgs e)
        {
            AboutDialog dialog = new AboutDialog();
            dialog.ShowDialog();
        }

        private void BtnGoToParentFolder_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFolder?.ParentFolder != null)
                DisplayFolder(CurrentFolder.ParentFolder);
        }

        private void SelectFolder_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listAll.SelectedItem is Folder selectedFolder)
                DisplayFolder(selectedFolder);
        }

        private void RadioBtnStatsRange_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsInitialized)
                UpdateStats();
        }

        private void TxtSelectedRange_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.IsInitialized)
            {
                string? newText = (sender as TextBox)?.Text;
                if (!string.IsNullOrWhiteSpace(newText) && RangeUtility.ParseRange(newText) != (null, null))
                    UpdateStats();
            }
        }

        private void DisplayFolder(Folder folder)
        {
            CurrentFolder = folder;
            txtCurrentFolder.Text = CurrentFolder?.FullName;
            listAll.ItemsSource = CurrentFolder?.Items;
            UpdateStats();
        }

        private void DisplayDisk(char diskLabel)
        {
            Disk? diskInCache = Cache.ListDisk.Find(x => x.Label == diskLabel);

            if (diskInCache != null)
            {
                CurrentDisk = diskInCache;

                //if (Found(diskInCache.RootFolder))
                if (diskInCache.RootFolder != null)
                {
                    DisplayFolder(diskInCache.RootFolder);
                }
            }
        }

        private bool Found(object? possibleNullObject)
        {
            return possibleNullObject != null;
        }

        private void UpdateStats()
        {
            Statistics stats = new Statistics();
            if (radioAll.IsChecked is true)
            {
                if (CurrentFolder != null)
                    stats.CalculateStatsForAll(CurrentDisk, CurrentFolder);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(txtSelectedRange.Text))
                {
                    var (from, to) = RangeUtility.ParseRange(txtSelectedRange.Text);
                    if (from != null && to != null)
                    {
                        RangeUtility.MakeSureRangeNotToBig(ref from, ref to, CurrentFolder);

                        //if (CurrentFolder != null)
                        stats.CalculateStatsForRange(CurrentDisk, CurrentFolder, from, to);
                    }
                }
            }

            // If selected unit is not the unit used in cache (= byte), then conversion is necessary.
            decimal displayTotalSize;
            decimal displayAvgSize;
            if (SelectedUnit != SizeUnit.Byte)
            {
                displayTotalSize = Converter.ConvertFromByte(SelectedUnit, stats.TotalSize);
                displayAvgSize = Converter.ConvertFromByte(SelectedUnit, stats.AvgSize);
            }
            else
            {
                displayTotalSize = stats.TotalSize;
                displayAvgSize = stats.AvgSize;
            }

            // Display stats.
            txtTotalSize.Text = FormatForDisplay.FormatSize(displayTotalSize, SelectedUnit);
            txtAvgSize.Text = FormatForDisplay.FormatSize(displayAvgSize, SelectedUnit);
            txtDiskSpacePercentage.Text = FormatForDisplay.FormatDiskSpacePercentage(stats.DiskSpacePercentage);
        }
    }
}
