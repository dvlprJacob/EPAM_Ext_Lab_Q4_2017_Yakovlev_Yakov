namespace Task07_3_TestApp
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using Task07_3;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IntInitTest()
        {
            DinamicArray<int> a = new DinamicArray<int>(10);
            a.AddRange(new int[] { 4, 3, 2, 1 });

            Assert.AreEqual(a[a.Length - 1], 1);
        }

        [TestMethod]
        public void CharInitTest()
        {
            DinamicArray<char> a = new DinamicArray<char>(new char[] { 'a', 'b', 'c', 'd', 'f', 'g' });
            a.Remove(5);

            Assert.AreEqual(a[4], 'f');
        }

        [TestMethod]
        public void LengthAfterInsertTest()
        {
            DinamicArray<char> a = new DinamicArray<char>(new char[] { 'a', 'b', 'c', 'd', 'f', 'g' });
            a.Insert('A', 10);

            Assert.AreEqual(a.Length, 10);
        }

        [TestMethod]
        public void ForeachTest()
        {
            DinamicArray<int> a = new DinamicArray<int>(new int[] { 1, 2, 3, 4, 5 });
            List<int> currentNumbers = new List<int>();
            foreach (var el in a)
            {
                currentNumbers.Add(el);
            }

            Assert.AreEqual(currentNumbers[4], 5);
        }
    }
}