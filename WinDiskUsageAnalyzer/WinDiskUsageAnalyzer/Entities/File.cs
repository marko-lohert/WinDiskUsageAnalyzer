namespace FoldersAndFilesSizeAnalyzer.Entities
{
    public class File : IDiskObject
    {
        public string? Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
    }
}
