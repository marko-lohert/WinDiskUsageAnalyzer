using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows;

namespace WinDiskUsageAnalyzer.Dialogs
{
    /// <summary>
    /// About dialog displays info about tihs application.
    /// </summary>
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
            lblAbout.Content = GenerateAboutMessage();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private string GenerateAboutMessage()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            
            return new StringBuilder().
                    AppendLine("Win Disk Usage Analyzer").
                    AppendLine($"Version: {versionInfo.FileVersion}").
                    AppendLine().
                    AppendLine($"Copyright: {versionInfo.LegalCopyright}").
                    AppendLine("License: MIT").
                    ToString();
        }
    }
}
