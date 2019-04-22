using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private PuzzleDataManager dataManager;
    [SerializeField]
    private PuzzleGenerator generator;
    [SerializeField]
    private PuzzleSolver solver;
    [SerializeField]
    private PuzzleUI puzzleUi;

    private List<PuzzleData> data;
    private int[,] currentPuzzle;

    private void Start()
    {
        data = dataManager.LoadDatabase();
        puzzleUi.UpdateDropDown(data);
    }

    internal void SolvePuzzle()
    {
        if(currentPuzzle != null)
        {
            int[,] solvedPuzzle = solver.SolvePuzzle(currentPuzzle);

            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    if(currentPuzzle[i,j] == 0)
                    {
                        puzzleUi.UpdateCell(i,j, solvedPuzzle[i,j]);
                    }
                }
            }
        }
    }

    internal void GeneratePuzzle()
    {
        currentPuzzle = generator.GeneratePuzzle();
        puzzleUi.DisplayPuzzle(currentPuzzle);
    }

    internal void DisplayPuzzle(int puzzleId)
    {
        if(puzzleId != 0)
        {
            currentPuzzle = data[puzzleId - 1].ToGrid();
            puzzleUi.DisplayPuzzle(currentPuzzle);
        }
        else
        {
            puzzleUi.ClearPuzzleAction();
        }
    }

    internal void ClearCurrentPuzzle()
    {
        currentPuzzle = null;
    }

    internal bool SavePuzzle()
    {
        if(currentPuzzle == null)
        {
            return false;
        }

        data.Add(new PuzzleData(currentPuzzle));
        dataManager.SaveData(data);
        currentPuzzle = null;
        puzzleUi.UpdateDropDown(data);
        return true;
    }

}
