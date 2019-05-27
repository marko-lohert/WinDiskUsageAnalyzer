using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FoldersAndFilesSizeAnalyzer.Dialogs;
using FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement;
using FoldersAndFilesSizeAnalyzer.Entities;

namespace FoldersAndFilesSizeAnalyzer
{
    /// <summary>
    /// Analyze all folders and files on a disk, and stores a result into <see cref="Cache" />.
    /// </summary>
    public class Analyzer
    {
        public Disk AnalyzeDisk(char diskLabel)
        {
            string diskType;
            string diskManufacturer;

            string rootFolderName = $"{diskLabel}:\\";
            Disk disk = new Disk
            {
                Label = diskLabel,
                RootFolder = AnalyzeFolder(rootFolderName, parentFolder: null)
            };

            disk.RootFolder.Size = disk.RootFolder?.Items?.Sum(x => x.Size) ?? 0;
            Cache.Update(disk);

            return disk;
        }

        private Folder AnalyzeFolder(string fullPath, Folder? parentFolder)
        {
            if (string.IsNullOrEmpty(fullPath) || !Directory.Exists(fullPath))
                return null;

            try
            {
                var (filesInfo, subfoldersInfo) = GetFolderContentInfo(fullPath);

                Folder folder = new Folder
                {
                    Name = parentFolder is null ? "\\" : Path.GetFileNameWithoutExtension(fullPath),
                    Extension = Path.GetExtension(fullPath)?.Replace(".", ""),
                    FullName = fullPath,
                    Items = new IDiskObject[filesInfo.Length + subfoldersInfo.Length],
                    ParentFolder = parentFolder
                };

                int currenttemIndex = 0;

                AnalyzeSubfolders(subfoldersInfo, folder, ref currenttemIndex);
                AnalyzeFiles(filesInfo, folder, ref currenttemIndex);

                SortBySizeDesc(folder.Items);

                return folder;
            }
            catch (UnauthorizedAccessException)
            {
                return new Folder
                {
                    Name = AccessDeniedMessage,
                    FullName = AccessDeniedMessage,
                    Extension = default,
                    Size = 0,
                    ParentFolder = parentFolder
                };
            }
        }

        private void SortBySizeDesc(IDiskObject[] items)
        {
            Array.Sort(items, ComparisonSizeDesc);
        }

        /// <summary>
        /// Compare disk objects by size descending.
        /// </summary>
        private int ComparisonSizeDesc(IDiskObject first, IDiskObject second)
        {
            return -1 * first.Size.CompareTo(second.Size);
        }

        /// <summary>
        /// Gets info about all files and all subfolders in a given folder.
        /// </summary>
        /// <param name="fullPath">Folder that will be analyzed.</param>
        private (FileInfo[] filesInfo, DirectoryInfo[] subfoldersInfo) GetFolderContentInfo(string fullPath)
        {
            var folderInfo = new DirectoryInfo(fullPath);
            var filesInfo = folderInfo.GetFiles();
            var subfoldersInfo = folderInfo.GetDirectories();
            return (filesInfo, subfoldersInfo);
        }

        private void AnalyzeSubfolders(DirectoryInfo[] subfoldersInfo, Folder parentFolder, ref int currenttemIndex)
        {
            foreach (var item in subfoldersInfo)
            {
                var newSubFolder = AnalyzeFolder(item.FullName, parentFolder);

                newSubFolder.Size = newSubFolder.Items?.Sum(x => x.Size) ?? 0;

                parentFolder.Items[currenttemIndex] = newSubFolder;
                currenttemIndex++;
            }
        }

        private void AnalyzeFiles(FileInfo[] filesInfo, Folder folder, ref int currenttemIndex)
        {
            foreach (var item in filesInfo)
            {
                var newFile = new Entities.File
                {
                    Name = Path.GetFileNameWithoutExtension(item.Name),
                    Extension = item.Extension?.Replace(".", ""),
                    Size = item.Length
                };

                folder.Items[currenttemIndex] = newFile;
                currenttemIndex++;
            }
        }

        /// <summary>
        ///  Message that is used in place of Name and FullName of disk object when user doesn't have permission to access that disk object.
        /// </summary>
        private const string AccessDeniedMessage = "** Access denied **";
    }
}
