using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : PuzzleBase
{

    ///<summary>
    ///Returns a solved and complete version of the given sudoku <paramref name="grid"/>.
    ///</summary>
    internal int[,] SolvePuzzle(int[,] grid)
    {
        int[,] solved = new int[PuzzleUtils.Constants.GRID_SIZE,PuzzleUtils.Constants.GRID_SIZE];

        System.Array.Copy(grid, solved, grid.Length);

        List<Vector2Int> positions = GetPositionsToSolve(solved);

        SolvePositions(0, positions, solved);

        return solved;
    }
}
