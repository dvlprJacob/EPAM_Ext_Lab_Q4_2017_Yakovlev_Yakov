namespace Task07_2_TestApp
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task07_2;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InitTest1()
        {
            string text = "a ss dd.d.,d. A.a.Dd SS";
            MyString temp = new MyString(text);

            Assert.AreEqual(temp.Count, 5);
        }

        [TestMethod]
        public void InitTest2()
        {
            string text = "a ss dd.d.,d. A.a.Dd SS";
            MyString temp = new MyString(text);

            Assert.AreEqual(temp.Text, text);
        }

        [TestMethod]
        public void GetKvpByKeyTest()
        {
            string text = "a ss dd.d.,d. A.a.Dd SS ss sss d";
            MyString temp = new MyString(text);

            Assert.AreEqual(temp.GetPairByKey(3).Length, 2);
        }

        [TestMethod]
        public void GetKvpByWordTest()
        {
            string text = "a ss dd.d.,d. A.a.Dd SS ss sss d";
            MyString temp = new MyString(text);

            Assert.AreEqual(temp.GetPairByWord("A").Value, 3);
        }

        [TestMethod]
        public void SeparatorsInitTest()
        {
            string text = "a ss dd.d.,d. A.a.Dd SS ss sss d";
            var separ = new char[] { ' ', '.', ',' };
            MyString temp = new MyString(text, separ);

            Assert.AreEqual(temp.Separators, separ);
        }

        [TestMethod]
        public void ResultWithOtherSeparatorsTest()
        {
            string text = "GTR.Z.sdGtr.f gtr gTr";
            var separ = new char[] { 'd', '.', ' ' };
            MyString temp = new MyString(text, separ);

            Assert.AreEqual(temp.GetPairByWord("gtr").Value, 4);
        }
    }
}