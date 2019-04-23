using System.Collections.Generic;
using UnityEngine;

public class PuzzleBase : MonoBehaviour
{
    ///<summary>
    ///Returns a random value between 0 and the sudoku grid size (8), used for indexing.
    ///</summary>
    protected int Random08
    {
        get => Random.Range(0, PuzzleUtils.Constants.GRID_SIZE);
    }


    ///<summary>
    ///Returns true if the given <paramref name="value"/> is compatible with the given <paramref name="x"/> and <paramref name="y"/> coordinates whithin the given <paramref name="grid"/> .
    ///</summary>
    protected bool CheckPosition(int x, int y, int value, int[,] grid)
    {
        if(value == 0)
        {
            return false;
        }
        //check 3x3 cluster
        for (int i = RoundToThree(x); i < RoundToThree(x) + 3; i++)
        {
            for (int j = RoundToThree(y); j < RoundToThree(y) + 3; j++)
            {
                if(grid[i,j] == value)
                {
                    return false;
                }
            }
        }

        //check column and row
        for (int i = 0; i < PuzzleUtils.Constants.GRID_SIZE; i++)
        {
            //check column
            if(grid[x,i] == value)
            {
                return false;
            }
            //check row
            if(grid[i,y] == value)
            {
                return false;
            }
        }

        return true;
    }

    ///<summary>
    ///Recusrsive method that attempts to find numbers that are compatible with all empty cells given int the  <paramref name="p"/> vector2int list, within the given <paramref name="grid"/>. <paramref name="posIndex"/> is used to identify position index during recursion.
    ///</summary>
    protected bool SolvePositions(int posIndex, List<Vector2Int> p, int[,] grid)
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
                if(SolvePositions(posIndex + 1, p, grid))
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
    protected bool EmptyCellsLeft(int[,] grid)
    {
        for (int i = 0; i < PuzzleUtils.Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < PuzzleUtils.Constants.GRID_SIZE; j++)
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
    protected List<Vector2Int> GetPositionsToSolve(int[,] grid)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        for (int i = 0; i < PuzzleUtils.Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < PuzzleUtils.Constants.GRID_SIZE; j++)
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
    ///Rounds the given int <paramref name="val"/> to the nearest quadrant coordinate in the given axis, of a sudoku sub-grid at the relative zero cooridinates Ex: for x = 4 the quadrant x coordinate will be 3.
    ///</summary>
    private int RoundToThree(int val)
    {
        return Mathf.FloorToInt(val/3.0f) * 3;
    }
}
