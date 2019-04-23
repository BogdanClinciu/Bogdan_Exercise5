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

        FillCells(0, positions, grid);
    }

    ///<summary>
    ///Recursive method that loops and backtracks until a solution is found. The <paramref name="p"/> should be a list of vector2int containing all unfilled cells, and <paramref name="posIndex"/> should be 0 when first calling the method.
    ///</summary>
    private bool FillCells(int posIndex, List<Vector2Int> p, int[,] grid)
    {
        if(!EmptyCellsLeft(grid))
        {
            return true;
        }

        for (int n = 1; n <= 9; n++)
        {
            if(CheckPosition(p[posIndex].x, p[posIndex].y, n, grid))
            {
                grid[p[posIndex].x, p[posIndex].y] = n;
                if(FillCells(posIndex + 1, p, grid))
                {
                    return true;
                }
                grid[p[posIndex].x, p[posIndex].y] = 0;
            }
        }

        return false;
    }

    ///<summary>
    ///Returns true if any unfilled cells are left in the given <paramref name="grid"/>.
    ///</summary>
    private bool EmptyCellsLeft(int[,] grid)
    {
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                if(grid[i,j] == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    ///<summary>
    ///Returns a list of all unfilled positions in the given <paramref name="grid"/>.
    ///</summary>
    private List<Vector2Int> GetPositionsToSolve(int[,] grid)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                if(grid[i,j] == 0)
                {
                    positions.Add(new Vector2Int(i, j));
                }
            }
        }

        return positions;
    }

    ///<summary>
    ///Removes <paramref name="cellsToClear"/> ammount from the given <paramref name="grid"/>.
    ///</summary>
    private void RemoveRandom(int cellsToClear, int[,] grid)
    {
        int cellsLeft = Constants.GRID_SIZE * Constants.GRID_SIZE;
        int randX = 0;
        int randY = 0;

        while(cellsLeft > cellsToClear)
        {
            randX = RandomInt();
            randY = RandomInt();

            if(grid[randX,randY] != 0)
            {
                grid[randX,randY] = 0;
                cellsLeft --;
            }
        }
    }
}
