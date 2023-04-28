using Minesweeper.Core;
using Minesweeper.Core.Enums;
using Minesweeper.Core.Models;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Minesweeper.GameProcessorTests
{
    [TestFixture]
    internal class GetCurrentFieldTests
    {
       [Test]
       public void GameProcessorTests_GetCurrentFieldTest_ProperlySetMines() {



            bool[,] field = { {false, false,true,false },
                              {false,true, false, false},
                              { true, false,false,false}, };

            var gameProcessor = new GameProcessor(field);

            var currentField = gameProcessor.GetCurrentField();


            for ( var row = 0; row < field.GetLength(0); row++ )
            {
                for (var col = 0; col < field.GetLength(1); col++)
                {

                    try { 
                        gameProcessor.Open(row, col);
                    }
                    catch (Exception e) {
                        
                    }
                    currentField = gameProcessor.GetCurrentField();
                    var currentPoint = currentField[row, col];
                    
                    //if (currentPoint == PointState.Mine) currentPoint=PointState.Close; //here each time we find a mine, we change it to close. Uncomment will cause the test to fail.
                    Console.Write(currentPoint + "," + field[row, col] + "|");


                    var actualStatus = (currentPoint != PointState.Mine);
                    var expectedStatus = (field[row, col] == true);
                    if (actualStatus && expectedStatus)
                    {
                        throw new Exception($"Mines Should be set properly from the initial field.\n" +
                            $"{field[row, col]} was expected, got {currentPoint} ");
                    }
                }
                Console.WriteLine();

            }
        }



    }
}
