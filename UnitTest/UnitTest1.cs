using ImageClassificationConsole._2_Application;
using Xunit;


namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void DummyTest()
        {
            Assert.True(true);
        }

        //Arrenge 
        Application _application= new Application();


        [Fact]
        public void TestAccuracyOfAI()
        {
            //Arrenge
            var expectedResult = 0.77;
            //Act
            var actualResult = _application.CalculateAccuracy();
            //Assert
            Assert.Equal(expectedResult, actualResult, 0.02);
        }


        [Fact]
        public void TestPrecisionOfAI()
        {
            //Arrenge
            var expectedResult = 0.714;
            //Act
            var actualResult = _application.CalculatePrecision("Truck");
            //Assert
            Assert.Equal(expectedResult, actualResult,0.02);
        }

        [Fact]
        public void TestRecallOfAI()
        {
            //Arrenge
            var expectedResult = 0.353;
            //Act
            var actualResult = _application.CalculateRecall("Car");
            //Assert
            Assert.Equal(expectedResult, actualResult,0.02);

        }

        [Fact]
        public void TestF1OfAI()
        {
            //Arrenge
            var expectedResult = 0.833;
            //Act
            var actualResult = _application.CalculateF1("Truck");
            //Assert
            Assert.Equal(expectedResult, actualResult,0.02);

        }
    }
}
