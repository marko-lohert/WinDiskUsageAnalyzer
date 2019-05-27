using FoldersAndFilesSizeAnalyzer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinDiskUsageAnalyzer.Utils;

namespace WinDiskUsageAnalyzer.Tests
{
    [TestClass]
    public class RangeUtilityTests
    {
        [TestMethod]
        public void ParseRange_1to3_From0To2()
        {
            // Arrange
            string inputRange = "1-3"; // 1-based indexes.
            int? expectedFrom = 0; // 0-based index.
            int? expectedTo = 2; // 0-based index.
            int? actualFrom;
            int? actualTo;

            // Act
            (actualFrom, actualTo) = RangeUtility.ParseRange(inputRange);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void ParseRange_2to5WithWhiteSpace_From1To4()
        {
            // Arrange
            string inputRange = " \t2 \t-\t5 \t "; // 1-based indexes.
            int? expectedFrom = 1;  // 0-based index.
            int? expectedTo = 4; // 0-based index.
            int? actualFrom;
            int? actualTo;

            // Act
            (actualFrom, actualTo) = RangeUtility.ParseRange(inputRange);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void ParseRange_EmptyRange_ToIsNullFromIsNull()
        {
            // Arrange
            string inputRange = string.Empty; // 1-based indexes.
            int? expectedFrom = null; // 0-based index.
            int? expectedTo = null; // 0-based index.
            int? actualFrom;
            int? actualTo;

            // Act
            (actualFrom, actualTo) = RangeUtility.ParseRange(inputRange);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void ParseRange_From1ToMissing_ToIsNullFromIsNull()
        {
            // Arrange
            string inputRange = "1-"; // 1-based indexes.
            int? expectedFrom = null; // 0-based index.
            int? expectedTo = null; // 0-based index.
            int? actualFrom;
            int? actualTo;

            // Act
            (actualFrom, actualTo) = RangeUtility.ParseRange(inputRange);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void ParseRange_FromMissingTo3_ToIsNullFromIsNull()
        {
            // Arrange
            string inputRange = "-3"; // 1-based indexes.
            int? expectedFrom = null; // 0-based index.
            int? expectedTo = null; // 0-based index.
            int? actualFrom;
            int? actualTo;

            // Act
            (actualFrom, actualTo) = RangeUtility.ParseRange(inputRange);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void MakeSureRangeNotToBig_FolderWith5Item_FromChangedTo5()
        {
            // Arrange
            Folder folder = new Folder()
            {
                Items = new IDiskObject[]
                {
                    new File(),
                    new File(),
                    new File(),
                    new File(),
                    new File(),
                }
            };
            int? expectedFrom = 5;
            int? expectedTo = 3;
            int? actualFrom = 7;
            int? actualTo = 3;

            // Act
            RangeUtility.MakeSureRangeNotToBig(ref actualFrom, ref actualTo, folder);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void MakeSureRangeNotToBig_FolderWith5Item_ToChangedTo5()
        {
            // Arrange
            Folder folder = new Folder()
            {
                Items = new IDiskObject[]
                {
                    new File(),
                    new File(),
                    new File(),
                    new File(),
                    new File(),
                }
            };
            int? expectedFrom = 2;
            int? expectedTo = 5;
            int? actualFrom = 2;
            int? actualTo = 10;

            // Act
            RangeUtility.MakeSureRangeNotToBig(ref actualFrom, ref actualTo, folder);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void MakeSureRangeNotToBig_FolderWith5Item_ToAndFromRemain3()
        {
            // Arrange
            Folder folder = new Folder()
            {
                Items = new IDiskObject[]
                {
                    new File(),
                    new File(),
                    new File(),
                    new File(),
                    new File(),
                }
            };
            int? expectedFrom = 3;
            int? expectedTo = 3;
            int? actualFrom = 3;
            int? actualTo = 3;

            // Act
            RangeUtility.MakeSureRangeNotToBig(ref actualFrom, ref actualTo, folder);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void MakeSureRangeNotToBig_FolderWith3Item_ToAndFromRemain3()
        {
            // Arrange
            Folder folder = new Folder()
            {
                Items = new IDiskObject[]
                {
                    new File(),
                    new File(),
                    new File(),
                }
            };
            int? expectedFrom = 3;
            int? expectedTo = 3;
            int? actualFrom = 3;
            int? actualTo = 3;

            // Act
            RangeUtility.MakeSureRangeNotToBig(ref actualFrom, ref actualTo, folder);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void MakeSureRangeNotToBig_FolderWith1Item_ToAndFromChangedTo1()
        {
            // Arrange
            Folder folder = new Folder()
            { 
                Items = new IDiskObject[]
                {
                    new File()
                }
            };
            int? expectedFrom = 1;
            int? expectedTo = 1;
            int? actualFrom = 3;
            int? actualTo = 5;

            // Act
            RangeUtility.MakeSureRangeNotToBig(ref actualFrom, ref actualTo, folder);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void MakeSureRangeNotToBig_EmptyFolder_ToAndFromChangedTo0()
        {
            // Arrange
            Folder folder = new Folder()
            {
                Items = new IDiskObject[0]
            };
            int? expectedFrom = 0;
            int? expectedTo = 0;
            int? actualFrom = 1;
            int? actualTo = 2;

            // Act
            RangeUtility.MakeSureRangeNotToBig(ref actualFrom, ref actualTo, folder);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }

        [TestMethod]
        public void MakeSureRangeNotToBig_NullFolder_ToAndFromChangedTo0()
        {
            // Arrange
            int? expectedFrom = 0;
            int? expectedTo = 0;
            int? actualFrom = 1;
            int? actualTo = 1;

            // Act
            RangeUtility.MakeSureRangeNotToBig(ref actualFrom, ref actualTo, null);

            // Assert
            Assert.AreEqual(expectedFrom, actualFrom);
            Assert.AreEqual(expectedTo, actualTo);
        }
    }
}
