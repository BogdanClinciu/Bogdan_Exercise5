using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : PuzzleBase
{

    ///<summary>
    ///Genereates a new random and complete sudoku grid and removes the set number of cells.
    ///</summary>
    internal int[,] GeneratePuzzle()
    {

        int[,] grid = new int[Constants.GRID_SIZE,Constants.GRID_SIZE];
        int[] randomUL = Constants.RANDOMIZED_RANGE;
        int[] randomDR = Constants.RANDOMIZED_RANGE;

        //randomize the upper left
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                grid[i,j] = randomUL[i * 3 + j];
            }
        }

        //randomize the lower right
        for (int i = 6; i < 9; i++)
        {
            for (int j = 6; j < 9; j++)
            {
                grid[i,j] = randomDR[(i-6) * 3 + (j-6)];
            }
        }

        //complete the puzzle
        CompletePuzzle(grid);
        RemoveRandom(Constants.CELLS_TO_CLEAR, grid);
        return grid;
    }

    ///<summary>
    ///Completes the given sudoku <paramref name="grid"/> array.
    ///</summary>
    private void CompletePuzzle(int[,] grid)
    {
        System.Array.Copy(grid, grid, grid.Length);

        List<Vector2Int> positions = GetPositionsToSolve(grid);

        SolvePositions(0, positions, grid);
    }

    ///<summary>
    ///Clears cells until the given <paramref name="cellsToClear"/> ammount is left in the given <paramref name="grid"/>.
    ///</summary>
    private void RemoveRandom(int cellsToClear, int[,] grid)
    {
        int cellsLeft = Constants.GRID_SIZE * Constants.GRID_SIZE;
        int randX = 0;
        int randY = 0;

        while(cellsLeft > cellsToClear)
        {
            randX = Random08;
            randY = Random08;

            if(grid[randX,randY] != 0)
            {
                grid[randX,randY] = 0;
                cellsLeft --;
            }
        }
    }
}
