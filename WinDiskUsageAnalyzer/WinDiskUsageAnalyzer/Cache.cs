using FoldersAndFilesSizeAnalyzer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoldersAndFilesSizeAnalyzer
{
    /// <summary>
    /// Cache contains info about all folders and files of every disk that was analyzed.
    /// </summary>
    public static class Cache
    {
        static Cache()
        {
            ListDisk = new List<Disk>();
        }

        public static List<Disk> ListDisk { get; set; }

        public static void Update(Disk disk)
        {
            ListDisk.RemoveAll(x => x.Label == disk.Label);
            ListDisk.Add(disk);
        }
    }
}
