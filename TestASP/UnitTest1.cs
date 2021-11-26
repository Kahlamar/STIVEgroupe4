using NUnit.Framework;

namespace TestASP
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {

            System.Console.WriteLine("coucou");

            Assert.Pass();
        }
    }
}