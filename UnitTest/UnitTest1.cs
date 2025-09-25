using ImageClassificationConsole._2_Application;
namespace UnitTest
{
    public class UnitTest1
    {

        //Arrenge 
        application _application= new application();
        [Fact]
        public void TestAccuracyOfAI()
        {
            //Arrenge
            var expectedResult = 85;
            //Act
            var actualResult = _application.CalculateAccuracy();
            //Assert
            Assert.Equal(expectedResult, actualResult);

        }


        [Fact]
        public void TestPrecisionOfAI()
        {
            //Arrenge
            var expectedResult = 85;
            //Act
            var actualResult = _application.CalculatePrecision();
            //Assert
            Assert.Equal(expectedResult, actualResult);

        }

        [Fact]
        public void TestRecallOfAI()
        {
            //Arrenge
            var expectedResult = 85;
            //Act
            var actualResult = _application.CalculateRecall();
            //Assert
            Assert.Equal(expectedResult, actualResult);

        }

        [Fact]
        public void TestF1OfAI()
        {
            //Arrenge
            var expectedResult = 85;
            //Act
            var actualResult = _application.CalculateF1();
            //Assert
            Assert.Equal(expectedResult, actualResult);

        }

        [Fact]
        public void TestCorrectPicture()
        {
            //arrange
            bool HasBeenOpened = false;

            //Act
            var selectedPNG = _application.SelectPicture();

            if (selectedPNG != null)
            {
                HasBeenOpened = true;
            }

            //Assert
            Assert.True(HasBeenOpened);
        }
    }
}
