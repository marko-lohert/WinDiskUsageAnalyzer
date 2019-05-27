using FoldersAndFilesSizeAnalyzer;
using FoldersAndFilesSizeAnalyzer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace WinDiskUsageAnalyzer.Tests
{
    [TestClass]
    public class StatisticsTests
    {
        static Statistics statistics;

        Disk? MockDisk;

        [TestInitialize]
        public void InitTest()
        {
            statistics = new Statistics();
            GenerateMockDisk();
        }

        [TestMethod]
        public void CalculateStatsForRange_Disk5Folders_CalcForFirst3Folders()
        {
            // Arrange
            long expectedTotalSize = 82944L;
            decimal expectedAvgSize = 27648m;
            decimal expectedDiskSpacePercentage = 66.942m;

            // Act
            if (MockDisk?.RootFolder != null)
                statistics.CalculateStatsForRange(MockDisk, MockDisk.RootFolder, 0, 2);

            // Assert
            Assert.AreEqual(expectedTotalSize, statistics.TotalSize);
            // For avg size and disk space percentage it is ok if result is in +- 0.01 range.
            Assert.AreEqual((double)expectedAvgSize, (double)statistics.AvgSize, delta: 0.01);
            Assert.AreEqual((double)expectedDiskSpacePercentage, (double)statistics.DiskSpacePercentage, delta: 0.01);
        }

        [TestMethod]
        public void CalculateStatsForRange_NullFromNullTo_ResultZero()
        {
            // Arrange
            long expectedTotalSize = 0L;
            long expectedAvgSize = 0L;
            decimal expectedDiskSpacePercentage = 0m;

            // Act
            if (MockDisk?.RootFolder != null)
                statistics.CalculateStatsForRange(MockDisk, MockDisk.RootFolder, null, null);

            // Assert
            Assert.AreEqual(expectedTotalSize, statistics.TotalSize);
            Assert.AreEqual(expectedAvgSize, statistics.AvgSize);
            Assert.AreEqual(expectedDiskSpacePercentage, statistics.DiskSpacePercentage);
        }

        [TestMethod]
        public void CalculateStatsForAll_Disk5Folders_CalcFor2ndFolder()
        {
            // Arrange
            long expectedTotalSize = 16384L;
            decimal expectedAvgSize = 5461.33m;
            decimal expectedDiskSpacePercentage = 13.223m;
            
            Folder? mockedFolder = MockDisk?.RootFolder?.Items[1] as Folder;

            // Act
            statistics.CalculateStatsForAll(MockDisk, mockedFolder);

            // Assert
            Assert.AreEqual(expectedTotalSize, statistics.TotalSize);
            // For avg size and disk space percentage it is ok if result is in +- 0.01 range.
            Assert.AreEqual((double)expectedAvgSize, (double)statistics.AvgSize, delta: 0.01);
            Assert.AreEqual((double)expectedDiskSpacePercentage, (double)statistics.DiskSpacePercentage, delta: 0.01);
        }

        [TestMethod]
        public void CalculateStatsForAll_EmptyDiskEmptyFolder_ResultZero()
        {
            // Arrange
            long expectedTotalSize = 0L;
            long expectedAvgSize = 0L;
            decimal expectedDiskSpacePercentage = 0m;
            Folder emptyFolder = new Folder
            {
                Items = new IDiskObject[0],
                Size = 0
            };
            Disk emptyDisk = new Disk()
            {
                Label = 'C',
                RootFolder = emptyFolder
            };

            // Act
            statistics.CalculateStatsForAll(emptyDisk, emptyFolder);

            // Assert
            Assert.AreEqual(expectedTotalSize, statistics.TotalSize);
            Assert.AreEqual(expectedAvgSize, statistics.AvgSize);
            Assert.AreEqual(expectedDiskSpacePercentage, statistics.DiskSpacePercentage);
        }

        [TestMethod]
        public void CalculateStatsForAll_NullDiskNullFolder_ResultZero()
        {
            // Arrange
            long expectedTotalSize = 0L;
            long expectedAvgSize = 0L;
            decimal expectedDiskSpacePercentage = 0m;

            // Act
            statistics.CalculateStatsForAll(null, null);

            // Assert
            Assert.AreEqual(expectedTotalSize, statistics.TotalSize);
            Assert.AreEqual(expectedAvgSize, statistics.AvgSize);
            Assert.AreEqual(expectedDiskSpacePercentage, statistics.DiskSpacePercentage);
        }

        private void GenerateMockDisk()
        {
            MockDisk = new Disk
            {
                Label = 'C',
                RootFolder = new Folder()
                {
                    Items = new IDiskObject[]
                    {
                        new Folder
                        {
                            Name = "test folder a",
                            Items = new IDiskObject[]
                            {
                                new File
                                {
                                    Name = "test file a",
                                    Size = 1024
                                }
                            },
                            Size = 1024
                        },
                        new Folder
                        {
                            Name = "test folder b",
                            Items = new IDiskObject[]
                            {
                                new File
                                {
                                    Name = "test file b1",
                                    Size = 2048
                                },
                                new File
                                {
                                    Name = "test file b2",
                                    Size = 4096
                                },
                                new File
                                {
                                    Name = "test file b3",
                                    Size = 10240
                                }
                            },
                            Size = 16384
                        },
                        new Folder
                        {
                            Name = "test folder c",
                            Items = new IDiskObject[]
                            {
                                new File
                                {
                                    Name = "test file c",
                                    Size = 65536
                                }
                            },
                            Size = 65536
                        },
                        new Folder
                        {
                            Name = "test folder d",
                           Items = new IDiskObject[]
                            {
                                new File
                                {
                                    Name = "test file d",
                                    Size = 32768
                                }
                            },
                            Size = 32768
                        },
                        new Folder
                        {
                            Name = "test folder e",
                            Items = new IDiskObject[]
                            {
                                new File
                                {
                                    Name = "test file e",
                                    Size = 8192
                                }
                            },
                            Size = 8192
                        },
                    }
                }
            };

            MockDisk.RootFolder.Size = MockDisk.RootFolder.Items.Sum(x => x.Size);
        }
    }
}
