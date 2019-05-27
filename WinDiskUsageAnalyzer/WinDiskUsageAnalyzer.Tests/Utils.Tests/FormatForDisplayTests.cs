using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinDiskUsageAnalyzer.Utils;
using static FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement.SizeUnit;

namespace WinDiskUsageAnalyzer.Tests
{
    [TestClass]
    public class FormatForDisplayTests
    {
        [TestMethod]
        public void FormatSize_1024KB()
        {
            // Arrange
            string expectedFormattedString = "1024 KB";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatSize(1024, KB);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }

        [TestMethod]
        public void FormatSize_3_14MB()
        {
            // Arrange
            string expectedFormattedString = "3.14 MB";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatSize(3.14159265359m, MB);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }

        [TestMethod]
        public void FormatDiskSpacePercentage_100Percent()
        {
            // Arrange
            string expectedFormattedString = "100 %";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatDiskSpacePercentage(100m);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }

        [TestMethod]
        public void FormatDiskSpacePercentage_3_14Percent()
        {
            // Arrange
            string expectedFormattedString = "3.142 %";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatDiskSpacePercentage(3.14159265359m);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }

        [TestMethod]
        public void FormatDiskSpacePercentage_0_001Percent()
        {
            // Arrange
            string expectedFormattedString = "0.001 %";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatDiskSpacePercentage(0.001m);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }

        [TestMethod]
        public void FormatDiskSpacePercentage_0_0009Percent()
        {
            // Arrange
            string expectedFormattedString = "< 0.001 %";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatDiskSpacePercentage(0.0009m);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }

        [TestMethod]
        public void FormatDiskSpacePercentage_0Percent()
        {
            // Arrange
            string expectedFormattedString = "0 %";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatDiskSpacePercentage(0m);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }

        [TestMethod]
        public void FormatDiskSpacePercentage_NullPercent()
        {
            // Arrange
            string expectedFormattedString = "Unknown disk space percentage";
            string actualFormattedString;

            // Act
            actualFormattedString = FormatForDisplay.FormatDiskSpacePercentage(null);

            // Assert
            Assert.AreEqual(expectedFormattedString, actualFormattedString);
        }
    }
}
