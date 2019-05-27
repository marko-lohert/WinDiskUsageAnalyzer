using FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static FoldersAndFilesSizeAnalyzer.UnitsOfMeasurement.SizeUnit;

namespace WinDiskUsageAnalyzer.Tests.UnitsOfMeasurement.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void ConvertFromByte_1024_ToBytes()
        {
            // Arrange
            decimal expectedConvertedSize = 1024m;
            decimal actualConvertedSize;

            // Act
            actualConvertedSize = Converter.ConvertFromByte(SizeUnit.Byte, 1024);

            // Assert
            Assert.AreEqual(expectedConvertedSize, actualConvertedSize);
        }

        [TestMethod]
        public void ConvertFromByte_1024_ToKB()
        {
            // Arrange
            decimal expectedConvertedSize = 1m;
            decimal actualConvertedSize;

            // Act
            actualConvertedSize = Converter.ConvertFromByte(KB, 1024);

            // Assert
            Assert.AreEqual(expectedConvertedSize, actualConvertedSize);
        }

        [TestMethod]
        public void ConvertFromByte_2097152_ToMB()
        {
            // Arrange
            decimal expectedConvertedSize = 2m;
            decimal actualConvertedSize;

            // Act
            actualConvertedSize = Converter.ConvertFromByte(MB, 2097152);

            // Assert
            Assert.AreEqual(expectedConvertedSize, actualConvertedSize);
        }

        [TestMethod]
        public void ConvertFromByte_1610612736_ToGB()
        {
            // Arrange
            decimal expectedConvertedSize = 1.5m;
            decimal actualConvertedSize;

            // Act
            actualConvertedSize = Converter.ConvertFromByte(GB, 1610612736);

            // Assert
            Assert.AreEqual(expectedConvertedSize, actualConvertedSize);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertFromByte_UnknownUnit_ExceptionThrown()
        {
            // Act
            Converter.ConvertFromByte(Unknown, 1);

            // Assert
            // Exception should be thrown (for details see attribute ExpectedException of this test). 
        }
    }
}
