using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{
    [SerializeField]
    private PuzzleManager manager;

    [SerializeField]
    private Dropdown puzzleSelector;

    [SerializeField]
    private RectTransform sudokuRect;
    [SerializeField]
    private Transform columnParent;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject cellPrefab;
    [SerializeField]
    private GameObject columnPrefab;

    [SerializeField]
    private GameObject savePuzzleButton;

    private Cell[,] cells;

    private void Start()
    {
        savePuzzleButton.SetActive(false);

        SizeSudokuRect();
        InitializeGrid();

        puzzleSelector.onValueChanged.AddListener((value) => manager.DisplayPuzzle(value));
    }

    #region ButtonActions

        public void SavePuzzleAction()
        {
            if(manager.SavePuzzle())
            {
                ClearPuzzle();
                savePuzzleButton.SetActive(false);
            }
        }

        public void GeneratePuzzleAction()
        {
            manager.GeneratePuzzle();
            savePuzzleButton.SetActive(true);
        }

        public void ClearPuzzleAction()
        {
            ClearPuzzle();
            manager.ClearCurrentPuzzle();
            savePuzzleButton.SetActive(false);
            puzzleSelector.value = 0;
        }

    #endregion

    internal void UpdateDropDown(List<PuzzleData> data)
    {
        puzzleSelector.ClearOptions();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        options.Add(new Dropdown.OptionData(Constants.PUZZLE_LABEL_NONE));

        for (int i = 0; i < data.Count; i++)
        {
            options.Add(new Dropdown.OptionData(Constants.PUZZLE_LABEL + i));
        }

        puzzleSelector.options = options;
    }

    internal void DisplayPuzzle(int[,] grid)
    {
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                UpdateCell(i,j, grid[i,j], Color.black);
            }
        }
    }

    internal void ClearPuzzle()
    {
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                ClearCell(i,j);
            }
        }
    }

    internal void UpdateCell(int x, int y, int value, Color color)
    {
        cells[x,y].UpdateCellText(value, color);
    }

    internal void ClearCell(int x, int y)
    {
        cells[x,y].UpdateCellText(0, Color.black);
    }

    private void InitializeGrid()
    {
        cells = new Cell[Constants.GRID_SIZE,Constants.GRID_SIZE];

        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            Transform col = Instantiate(columnPrefab, columnParent).transform;
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                cells[i,j] = Instantiate(cellPrefab, col).GetComponent<Cell>();
                cells[i,j].UpdateCellText(0, Color.black);
            }
        }
    }

    private void SizeSudokuRect()
    {
        sudokuRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sudokuRect.rect.height);
    }
}
