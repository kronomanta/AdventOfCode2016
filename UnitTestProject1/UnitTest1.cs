using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
                new KeyValuePair<string, List<string>>("ADVENT", new List<string> { "ADVENT" }),
                new KeyValuePair<string, List<string>>("A(1x5)BC", new List<string> { "A", "BBBBB", "C"}),
                new KeyValuePair<string, List<string>>("(3x3)XYZ", new List<string> { "XYZXYZXYZ"}),
                new KeyValuePair<string, List<string>>("A(2x2)BCD(2x2)EFG", new List<string> { "A", "BCBC", "D", "EFEF", "G"}),
                new KeyValuePair<string, List<string>>("(6x1)(1x3)A", new List<string> { "AAA"}),
                new KeyValuePair<string, List<string>>("X(8x2)(3x3)ABCY", new List<string> { "X", "Y", "ABCABCABC", "ABCABCABC"}),
            })
            {
                List<string> actual = Day09.Program.DecompressLinePart2(item.Key);
                CollectionAssert.AreEqual(item.Value, actual);
            }
        }
    }
}
