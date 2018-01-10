namespace Task09_2_UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task09_2;

    [TestClass]
    public class ExtensionMethodTask2Tests
    {
        [TestMethod]
        public void TestMethod1()
        {
            string[] temp = new string[] { "1", "10", "101" };
            foreach (var line in temp)
            {
                Assert.AreEqual(line.IsPositiveIntegerNumber(), true);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            string[] temp = new string[] { "0.1", "1,901", "-7" };
            foreach (var line in temp)
            {
                Assert.AreEqual(line.IsPositiveIntegerNumber(), false);
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            string[] temp = new string[] { "-6", "9O67", "903KJF0" };
            foreach (var line in temp)
            {
                Assert.AreEqual(line.IsPositiveIntegerNumber(), false);
            }
        }
    }
}