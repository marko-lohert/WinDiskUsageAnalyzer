namespace FoldersAndFilesSizeAnalyzer.Entities
{
    public interface IDiskObject
    {
        string? Name { get; set; }
        string Extension { get; set; }
        long Size { get; set; }
    }
}
