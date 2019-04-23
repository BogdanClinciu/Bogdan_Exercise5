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
        //load the data list
        data = dataManager.LoadDatabase();
        //trigger the ui manager to update the puzzle dropdown
        puzzleUi.UpdateDropDown(data);
    }

    ///<summary>
    ///Solves the currently selected puzzle if it is not null and updates the resolved cells in the ui manager.
    ///</summary>
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

    ///<summary>
    ///Generates a new, solvable, sudoku puzzle, triggers the ui manager to display it and sets it as the current puzzle.
    ///</summary>
    internal void GeneratePuzzle()
    {
        currentPuzzle = generator.GeneratePuzzle();
        puzzleUi.DisplayPuzzle(currentPuzzle);
    }

    ///<summary>
    ///Updates or clears the current puzzle and triggers the ui manager to update the grid.
    ///</summary>
    internal void DisplayPuzzle(int puzzleId)
    {
        if(puzzleId != 0)
        {
            currentPuzzle = data[puzzleId - 1].ToGrid();
            puzzleUi.DisplayPuzzle(currentPuzzle);
        }
        else
        {
            currentPuzzle = null;
            puzzleUi.ClearPuzzleAction();
        }
    }

    ///<summary>
    ///Clears the current puzzle.
    ///</summary>
    internal void ClearCurrentPuzzle()
    {
        currentPuzzle = null;
    }

    ///<summary>
    ///Triggers the puzzle data manager to save the current puzzle to storage and triggers the ui to update.
    ///</summary>
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
