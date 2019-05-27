using FoldersAndFilesSizeAnalyzer.Entities;
using System.Windows;
using System.Windows.Controls;

namespace FoldersAndFilesSizeAnalyzer
{
    /// <summary>
    /// Selector for disk object column in XAML of <see cref="MainWindow"/>.
    /// </summary>
    public class DiskObjectTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is null || container is null)
                return null;

            FrameworkElement? element = container as FrameworkElement;

            if (item is Folder)
                return element?.FindResource("FolderTemplate") as DataTemplate;
            else if (item is File)
                return element?.FindResource("FileTemplate") as DataTemplate;
            else
                return null;
        }
    }
}
