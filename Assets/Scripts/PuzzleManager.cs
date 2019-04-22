using System.Collections;
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

    internal void SolvePuzzle(int puzzleIndex)
    {
        solver.SolvePuzzle(data[puzzleIndex].ToGrid());
    }

    internal void GeneratePuzzle()
    {
        currentPuzzle = generator.GeneratePuzzle();
        puzzleUi.DisplayPuzzle(currentPuzzle);
    }

    internal void DisplayPuzzle(int puzzleId)
    {
        Debug.Log(puzzleId);
        if(puzzleId != 0)
        {
            puzzleUi.DisplayPuzzle(data[puzzleId - 1].ToGrid());
        }
        else
        {
            puzzleUi.ClearPuzzle();
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
