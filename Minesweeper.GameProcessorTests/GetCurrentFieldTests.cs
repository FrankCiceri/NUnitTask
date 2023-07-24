using FluentAssertions;
using Minesweeper.Core;
using Minesweeper.Core.Enums;
using NUnit.Framework;
using System.Linq;
using FluentAssertions;



namespace Minesweeper.GameProcessorTests

{
   

    [TestFixture]
    internal class GetCurrentFieldTests
    {
        private GameProcessor gameProcessor;
        private bool[,] field;
        [SetUp]
        public void SetUp()
        {
              field = new bool[,]{ {false, false,false,false },
                              {false,true, false, false},
                              { true, false,false,false}, };

            gameProcessor = new GameProcessor(field);

        }


        [Test]
       public void GetCurrentField_ProperlySetMines_True() {


            var currentField = gameProcessor.GetCurrentField(); //Initial state of the field


            for ( var row = 0; row < field.GetLength(0); row++ )
            {
                for (var col = 0; col < field.GetLength(1); col++)
                {

                    try { 
                        gameProcessor.Open(row, col); //start opening all fields

                    }
                    catch (Exception e) {
                        
                    }
                    currentField = gameProcessor.GetCurrentField();
                    var currentPoint = currentField[row, col];
                    
                    // if (currentPoint == PointState.Mine) currentPoint=PointState.Close; //here, each time we find a mine, we change it to close. Uncomment will cause the test to fail.
                    Console.Write(currentPoint + "," + field[row, col] + "|");

                    var actualStatus = (currentPoint != PointState.Mine);
                    var expectedStatus = (field[row, col] == true);
                   
                    Assert.IsTrue(!(actualStatus && expectedStatus), $"Mines Should be set properly from the initial field.\n" +
                           $"{field[row, col]} was expected, got {currentPoint} ");
                }
                Console.WriteLine();

            }
        }

        [TestCase(1,0)]
        [TestCase(3, 2)]
        public void GetCurrentField_OpeningSamePointRemainsSameField_true(int x, int y)
        {
            var currentField = gameProcessor.GetCurrentField(); //Initial state of the field.
           
            Console.WriteLine(currentField[y, x]);     

            gameProcessor.Open(x, y);            
            var expectedField = gameProcessor.GetCurrentField();
            Console.WriteLine(expectedField[y, x]);

            gameProcessor.Open(x, y);//start opening same Point
            var actualField = gameProcessor.GetCurrentField();
            //actualField[y, x] = PointState.Close; //this will change the state of the selected point.
            //In case the GetCurrentField method is not properly getting the states of each point it will cause the test to fail. 
            Console.WriteLine(actualField[y, x]);

            bool arraysEqual = true;  

            if (expectedField.GetLength(0) == actualField.GetLength(0) &&
                expectedField.GetLength(1) == actualField.GetLength(1)) // Here we check that the actual dimesions remains the same as the expected field if by any chance the method does not work properly
                    {
                        // Compare each element of the two arrays
                        for (int i = 0; i < expectedField.GetLength(0); i++)
                        {
                            for (int j = 0; j < expectedField.GetLength(1); j++)
                            {
                                if (expectedField[i, j] != actualField[i, j])
                                {
                                    arraysEqual = false;
                                    break;
                                }
                            }
                            if (!arraysEqual)
                                break;
                        }
                    }
                    else
                    {
                        arraysEqual = false;
                    }

                    Assert.IsTrue(arraysEqual,"Field should remain the same when clicking already opened points");
          
            Console.WriteLine();
        }

        [Test]
        public void GetCurrentField_StartingField_AllClosed() {

           var actualField = gameProcessor.GetCurrentField();
            bool allClosed = true;

            for (int i = 0; i < actualField.GetLength(0); i++)
            {
                for (int j = 0; j < actualField.GetLength(1); j++)
                {
                    if (actualField[i, j] != PointState.Close)
                    {
                        allClosed = false;
                        break;
                    }
                }
                if (!allClosed)
                    break;
            }

            Assert.IsTrue(allClosed, "Field should remain with all points closed when starting the game");

        }
            
        
    }
}
