namespace Task07_1_TestApp
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Task07_1;

    [TestClass]
    public class UnitTest1//todo pn классы лучше говорящими названиями именовать MyCircleTest, например.
    {
        [TestMethod]
        public void IsNotNull()
        {
            MyCircle circle = new MyCircle(-1);//todo pn ничего не упало и тест завершился корректно. Значит, тест некорректный

            Assert.IsNotNull(circle);//todo pn у тебя это условие всегда верно. Потому что не заполнение поля circle в MyCircle не означает, что не создастся экземпляр
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