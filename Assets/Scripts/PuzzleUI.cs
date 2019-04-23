using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUI : MonoBehaviour
{

    [SerializeField]
    private bool isGenerator;
    [SerializeField]
    private PuzzleManager manager;

    [Space]
    [SerializeField]
    private Dropdown puzzleSelector;

    [Space]
    [SerializeField]
    private RectTransform sudokuRect;
    [SerializeField]
    private Transform columnParent;

    [Space]
    [SerializeField]
    private GameObject savePuzzleButton;
    [SerializeField]
    private GameObject solvePuzzleButton;

    [Header("Cell text colors")]
    [SerializeField]
    private Color defaultCellColor;
    [SerializeField]
    private Color solvedCellColor;

    [Space]
    [Header("Prefabs")]
    [SerializeField]
    private GameObject cellPrefab;
    [SerializeField]
    private GameObject columnPrefab;

    private Cell[,] cells;

    private void Start()
    {
        //disable the save/solve button initially
        if(isGenerator)
        {
            savePuzzleButton.SetActive(false);
        }
        else
        {
            solvePuzzleButton.SetActive(false);
        }

        //square the parent rect of the sudoku grid
        SizeSudokuRect();
        //Create text objects within the grid
        InitializeGrid();

        //assign onValueChanged function to puzzle selector
        puzzleSelector.onValueChanged.AddListener((value) => manager.DisplayPuzzle(value));

        //assign aditional use based functionality to the onValueChanged event of the puzzle selector
        if(!isGenerator)
        {
            puzzleSelector.onValueChanged.AddListener((value) => solvePuzzleButton.SetActive(value > 0));
        }
        else
        {
            puzzleSelector.onValueChanged.AddListener((value) => savePuzzleButton.SetActive(false));
        }

    }

    private void OnDestroy()
    {
        puzzleSelector.onValueChanged.RemoveAllListeners();
    }

    #region public ButtonActions

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
            puzzleSelector.value = 0;
            manager.GeneratePuzzle();
            savePuzzleButton.SetActive(true);
        }

        public void SolvePuzzleButton()
        {
            manager.SolvePuzzle();
            solvePuzzleButton.SetActive(false);
        }

        public void ClearPuzzleAction()
        {
            ClearPuzzle();
            manager.ClearCurrentPuzzle();
            puzzleSelector.value = 0;

            if(isGenerator)
            {
                savePuzzleButton.SetActive(false);
            }
            else
            {
                solvePuzzleButton.SetActive(false);
            }
        }

    #endregion

    ///<summary>
    ///Updates puzzle selector dropdown based on the given <paramref name="data"/> list.
    ///</summary>
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

    ///<summary>
    ///Updates each cell acording to the given sudoku <paramref name="grid"/>.
    ///</summary>
    internal void DisplayPuzzle(int[,] grid)
    {
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                UpdateCell(i,j, grid[i,j], defaultCellColor);
            }
        }
    }

    ///<summary>
    ///Clears each cell in the ui sudoku grid.
    ///</summary>
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

    ///<summary>
    ///Updates a specific cell at the given <paramref name="x"/> and <paramref name="y"/> coordinates with the given <paramref name="value"/>. In this case the color is automatically assigned as this method's intended use is for updating cells that have been solved.
    ///</summary>
    internal void UpdateCell(int x, int y, int value)
    {
        cells[x,y].UpdateCellText(value, solvedCellColor);
    }

    ///<summary>
    ///Updates a specific cell at the given <paramref name="x"/> and <paramref name="y"/> coordinates with the given <paramref name="value"/> and <paramref name="color"/>.
    ///</summary>
    private void UpdateCell(int x, int y, int value, Color color)
    {
        cells[x,y].UpdateCellText(value, color);
    }

    ///<summary>
    ///Clears a specific cell at the given <paramref name="x"/> and <paramref name="y"/> coordinates.
    ///</summary>
    private void ClearCell(int x, int y)
    {
        cells[x,y].UpdateCellText(0, Color.black);
    }

    ///<summary>
    ///Creates and refrences cell objects for later use, should only oocur once at start().
    ///</summary>
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

    ///<summary>
    ///Resizes the sudoku grid parent rect such that its widht is equal to its height.
    ///</summary>
    private void SizeSudokuRect()
    {
        sudokuRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sudokuRect.rect.height);
    }
}
