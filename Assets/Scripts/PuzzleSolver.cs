using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : PuzzleBase
{

    internal int[,] SolvePuzzle(int[,] grid)
    {
        int[,] solved = new int[Constants.GRID_SIZE,Constants.GRID_SIZE];

        System.Array.Copy(grid, solved, grid.Length);

        List<Vector2Int> positions = GetPositionsToSolve(solved);

        SolvePositions(0, positions, solved);

        return solved;
    }

    private bool SolvePositions(int posIndex, List<Vector2Int> p, int[,] grid)
    {
        if(posIndex == Constants.CELLS_TO_CLEAR + 1)
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
}
