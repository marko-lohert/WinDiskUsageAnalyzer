namespace FoldersAndFilesSizeAnalyzer.Entities
{
    public class Folder : IDiskObject
    {
        public string? Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }

        public string? FullName { get; set; }
        public IDiskObject[] Items { get; set; }
        public Folder? ParentFolder { get; set; }
    }
}
