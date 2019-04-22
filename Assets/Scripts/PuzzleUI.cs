﻿using System.Collections;
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
        if(isGenerator)
        {
            savePuzzleButton.SetActive(false);
        }
        else
        {
            solvePuzzleButton.SetActive(false);
        }


        SizeSudokuRect();
        InitializeGrid();

        puzzleSelector.onValueChanged.AddListener((value) => manager.DisplayPuzzle(value));

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
                UpdateCell(i,j, grid[i,j], defaultCellColor);
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

    internal void UpdateCell(int x, int y, int value)
    {
        cells[x,y].UpdateCellText(value, solvedCellColor);
    }

    private void UpdateCell(int x, int y, int value, Color color)
    {
        cells[x,y].UpdateCellText(value, color);
    }

    private void ClearCell(int x, int y)
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
