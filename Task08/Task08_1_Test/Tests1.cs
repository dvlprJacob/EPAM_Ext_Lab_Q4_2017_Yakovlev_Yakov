namespace Task08_1_Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task08;
    using WayOfCompare;
    using System.Collections.Generic;

    [TestClass]
    public class Tests1
    {
        private string[] initArray = { "g", "z", "aaa", "a", "aa", "aa", "aaaa", "a" };

        public int TestCompareWay(string f, string s)
        {
            if (f.Length == s.Length) return string.Compare(f, s);
            return f.Length < s.Length ? -1 : 1;
        }

        [TestMethod]
        public void InitTest()
        {
            MyStringArray temp = new MyStringArray(initArray);

            for (int i = 0; i < this.initArray.Length; i++)
            {
                Assert.AreEqual(temp[i], this.initArray[i]);
            }
        }

        [TestMethod]
        public void SortResultTest1()
        {
            MyStringArray temp = new MyStringArray(this.initArray);
            temp.Sort(WaysOfCompare.ByLength);
            List<string> sortedInitArray = new List<string>(this.initArray);
            sortedInitArray.Sort(this.TestCompareWay);

            for (int i = 0; i < this.initArray.Length; i++)
            {
                Assert.AreEqual(temp[i], sortedInitArray[i]);
            }
        }
    }
}