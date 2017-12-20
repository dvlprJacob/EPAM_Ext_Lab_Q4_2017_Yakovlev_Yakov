namespace Task07_1_TestApp
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task07_1;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsNotNull()
        {
            MyCircle circle = new MyCircle(999);

            Assert.IsNotNull(circle);
        }

        [TestMethod]
        public void RemoveEverySecondResult1()
        {
            MyCircle circle = new MyCircle(1223);
            circle.RemoveEverySecond();

            Assert.AreEqual(circle.Count, 1);
        }

        [TestMethod]
        public void MyDictTest()
        {
            MyDictionary dict = new MyDictionary(6);
            dict.RemoveAny(1);

            Assert.AreEqual(dict.Count, 6);
            Assert.AreEqual(dict.SerialNumberAt(1), 2);
        }
    }
}