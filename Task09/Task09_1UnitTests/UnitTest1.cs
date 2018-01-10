namespace Task09_1UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task09;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int[] intArray = new int[] { 2, 25, 3 };
            int sum = default(int);
            var resStatus = intArray.TryCalcSumOfElements(out sum);
            Assert.AreEqual(sum, 30);
        }

        [TestMethod]
        public void TestMethod2()
        {
            double[] doubleArray = new double[] { 2.3, 1, 3.65 };
            double sum2 = default(double);
            var resStatus = doubleArray.TryCalcSumOfElements(out sum2);
            Assert.AreNotEqual(sum2, 6.95);
            Assert.AreEqual(resStatus, true);
        }
    }
}