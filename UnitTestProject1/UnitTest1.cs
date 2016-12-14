using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Numerics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DecompressLineTest()
        {
            foreach(var item in new[]
            {
                new KeyValuePair<string, string>("ADVENT", "ADVENT"),
                new KeyValuePair<string, string>("A(1x5)BC", "ABBBBBC"),
                new KeyValuePair<string, string>("(3x3)XYZ", "XYZXYZXYZ"),
                new KeyValuePair<string, string>("A(2x2)BCD(2x2)EFG", "ABCBCDEFEFG"),
                new KeyValuePair<string, string>("(6x1)(1x3)A", "(1x3)A"),
                new KeyValuePair<string, string>("X(8x2)(3x3)ABCY", "X(3x3)ABC(3x3)ABCY"),
            })
            {
                string actual = Day09.Program.DecompressLinePart1(item.Key);
                Assert.AreEqual(item.Value, actual);
            }
        }

        [TestMethod]
        public void DecompressLinePart2Test()
        {
            foreach (var item in new[]
            {
                new KeyValuePair<string, int>("ADVENT", 6),
                new KeyValuePair<string, int>("A(1x5)BC", 7),
                new KeyValuePair<string, int>("(3x3)XYZ", 9),
                new KeyValuePair<string, int>("A(2x2)BCD(2x2)EFG", 11),
                new KeyValuePair<string, int>("(6x1)(1x3)A", 3),
                new KeyValuePair<string, int>("X(8x2)(3x3)ABCY", 20),
            })
            {
                BigInteger actual = Day09.Program.DecompressLinePart2(item.Key);
                Assert.AreEqual(item.Value, actual);
            }
        }
    }
}
