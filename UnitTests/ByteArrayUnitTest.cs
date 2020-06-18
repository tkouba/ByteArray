using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class ByteArrayUnitTest
    {
        [DataTestMethod]
        [DataRow("01-23-45", "01-23-45")]
        [DataRow("01-23-45", "01:23:45")]
        [DataRow("01-23-45", "012345")]
        [DataRow("AB-CD-EF", "AB-CD-EF")]
        [DataRow("AB-CD-EF", "AB:CD:EF")]
        [DataRow("AB-CD-EF", "ABCDEF")]
        [DataRow("AB-CD-EF", "ab-cd-ef")]
        [DataRow("AB-CD-EF", "ab:cd:ef")]
        [DataRow("AB-CD-EF", "abcdef")]
        [DataRow("AB-CD-EF", "AB-cD-EF")]
        [DataRow("AB-CD-EF", "AB:cD:EF")]
        [DataRow("AB-CD-EF", "ABcDEF")]
        [DataRow("1A-B2", "1A-B2")]
        [DataRow("1A-B2", "1A:B2")]
        [DataRow("1A-B2", "1AB2")]
        public void ParseByteArray(string expected, string value)
        {
            Assert.AreEqual(expected, BitConverter.ToString(ByteArray.Parse(value)));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        public void ParseEmptyByteArray(string value)
        {
            byte[] buffer = ByteArray.Parse(value);
            Assert.IsNotNull(buffer);
            Assert.AreEqual(0, buffer.Length);
        }

        [DataTestMethod]
        [DataRow("123")]
        [DataRow("1AB")]
        [DataRow("12-3")]
        [DataRow("12:3")]
        [DataRow("123-4")]
        [DataRow("123:4")]
        [DataRow("12-345-6")]
        [DataRow("12:345:6")]
        [DataRow("12-")]
        [DataRow("12:")]
        [DataRow("12-34-")]
        [DataRow("12:34:")]
        [DataRow("12-34:56")]
        [DataRow("12:34-56")]
        [DataRow("12x34x56")]
        [ExpectedException(typeof(FormatException))]
        public void ParseEmptyByteArray_FormatException(string value)
        {
            ByteArray.Parse(value);
        }

        [TestMethod]
        public void IsEmptyByteArray()
        {
            Assert.IsTrue(ByteArray.IsEmpty(new byte[0]));
            Assert.IsTrue(ByteArray.IsEmpty(ByteArray.Empty));
            Assert.IsTrue(ByteArray.IsEmpty(Array.Empty<byte>()));
            Assert.IsFalse(ByteArray.IsEmpty(new byte[3]));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsEmptyByteArray_ArgumentNullException()
        {
            ByteArray.IsEmpty(null);
        }

        [TestMethod]
        public void IsNullorEmptyByteArray()
        {
            Assert.IsTrue(ByteArray.IsNullOrEmpty(null));
            Assert.IsTrue(ByteArray.IsNullOrEmpty(new byte[0]));
            Assert.IsTrue(ByteArray.IsNullOrEmpty(ByteArray.Empty));
            Assert.IsTrue(ByteArray.IsNullOrEmpty(Array.Empty<byte>()));
            Assert.IsFalse(ByteArray.IsNullOrEmpty(new byte[3]));
        }
    }
}
