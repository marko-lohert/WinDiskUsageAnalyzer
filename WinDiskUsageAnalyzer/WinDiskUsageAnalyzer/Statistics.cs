using System;

namespace FoldersAndFilesSizeAnalyzer
{
    /// <summary>
    /// Calculates statistics of subfolders and files in a given folder.
    /// </summary>
    public class Statistics
    {
        public long TotalSize { get; private set; }
        public decimal AvgSize { get; private set; }
        public decimal DiskSpacePercentage { get; private set; }

        /// <summary>
        /// Calculate statistics for all objects in a given folder.
        /// </summary>
        public void CalculateStatsForAll(Entities.Disk? disk, Entities.Folder? folder)
        {
            TotalSize = folder?.Size ?? 0;

            int itemsCount = folder?.Items?.Length ?? 0;
            CalculateAvgSize(TotalSize, itemsCount);

            CalculateDiskSpacePercentage(disk, TotalSize);
        }

        /// <summary>
        /// Calculate statistics only for objects in a given range (in given folder).
        /// </summary>
        /// <param name="disk"></param>
        /// <param name="folder"></param>
        /// <param name="from">0-based index of the first object in the range.</param>
        /// <param name="to">0-based index of the last object in the range.</param>
        public void CalculateStatsForRange(Entities.Disk? disk, Entities.Folder folder, int? from, int? to)
        {
            if (from == null || to == null)
                return;

            TotalSize = 0;

            foreach (var item in folder.Items)
            {
                TotalSize += item.Size;
            }

            int itemsCount = ((to + 1) - from) ?? 0; 
            CalculateAvgSize(TotalSize, itemsCount);

            CalculateDiskSpacePercentage(disk, TotalSize);
        }

        /// <summary>
        /// Calculates an average size of items.
        /// </summary>
        private void CalculateAvgSize(long size, int length)
        {
            if (length > 0)
                AvgSize = (decimal)size / length;
            else
                AvgSize = 0;
        }

        /// <summary>
        /// Calculates what percentage of total used disk spaces is used in this folder.
        /// </summary>
        /// <param name="disk"></param>
        private void CalculateDiskSpacePercentage(Entities.Disk? disk, long totalSize)
        {
            if (disk != null && disk.RootFolder != null && disk.RootFolder.Size != 0)
                DiskSpacePercentage = (decimal)totalSize / disk.RootFolder.Size * 100;
            else
                DiskSpacePercentage = 0m;
        }
    }
}
