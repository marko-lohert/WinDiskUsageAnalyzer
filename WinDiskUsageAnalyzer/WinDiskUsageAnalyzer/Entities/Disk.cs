namespace FoldersAndFilesSizeAnalyzer.Entities
{
    public class Disk
    {
        public char Label { get; set; }
        public Folder? RootFolder { get; set; }
    }
}
