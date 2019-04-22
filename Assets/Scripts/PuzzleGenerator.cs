using System.Collections.Generic;
using UnityEngine;

public class PuzzleGenerator : PuzzleBase
{

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

    private void CompletePuzzle(int[,] grid)
    {
        System.Array.Copy(grid, grid, grid.Length);

        List<Vector2Int> positions = GetPositionsToSolve(grid);

        FillCells(0, positions, grid);
    }

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

    private void RemoveRandom(int cellsToClear, int[,] grid)
    {
        int removed = 0;
        int randX = 0;
        int randY = 0;

        while(removed <= cellsToClear)
        {
            randX = RandomInt();
            randY = RandomInt();

            if(grid[randX,randY] != 0)
            {
                grid[randX,randY] = 0;
                removed ++;
            }
        }
    }
}
    // internal int[,] GeneratePuzzle()
    // {
    //     Stopwatch t = new Stopwatch();
    //     t.Start();

    //     int[,] grid = new int[Constants.GRID_SIZE,Constants.GRID_SIZE];

    //     PlaceValues(0, grid);

    //     while(!CheckPuzzleSolvable(grid))
    //     {
    //         grid = new int[Constants.GRID_SIZE,Constants.GRID_SIZE];
    //         PlaceValues(0, grid);
    //     }

    //     RemoveRandom(Constants.CELLS_TO_CLEAR, grid);

    //     t.Stop();
    //     UnityEngine.Debug.Log(t.ElapsedMilliseconds);
    //     return grid;
    // }

    // private void PlaceValues(int rowY, int[,] grid)
    // {
    //     if(rowY == Constants.GRID_SIZE)
    //     {
    //         return;
    //     }

    //     int[] nums = Constants.RANDOMIZED_RANGE;

    //     for (int x = 0; x < Constants.GRID_SIZE; x++)
    //     {
    //         for (int n = 0; n < nums.Length; n++)
    //         {
    //             //method from PuzzleBase
    //             if(CheckPosition(x, rowY, nums[n], grid))
    //             {
    //                 grid[x,rowY] = nums[n];
    //                 PlaceValues(rowY + 1, grid);
    //             }
    //         }
    //     }
    // }

    // private bool CheckPuzzleSolvable(int[,] grid)
    // {
    //     for (int i = 0; i < Constants.GRID_SIZE; i++)
    //     {
    //         for (int j = 0; j < Constants.GRID_SIZE; j++)
    //         {
    //             if(grid[i,j] == 0)
    //             {
    //                 return false;
    //             }
    //         }
    //     }

    //     return true;
    // }
