[System.Serializable]
public class PuzzleData
{
    public string data;

    ///<summary>
    ///Constructor for an empty sudoku grid data string, denoting no data for a specific sudoku cell by a -1
    ///</summary>
    public PuzzleData()
    {
        data = string.Empty;

        for (int i = 0; i < PuzzleUtils.Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < PuzzleUtils.Constants.GRID_SIZE; j++)
            {
                data += -1 + (j < PuzzleUtils.Constants.GRID_SIZE - 1 ? PuzzleUtils.Constants.DATA_UNIT_SEPARATOR : PuzzleUtils.Constants.DATA_ROW_SEPARATOR);
            }
        }
    }

    ///<summary>
    ///Constructs a sudoku grid data string based on the 2d array <paramref name="grid"/>.
    ///</summary>
    public PuzzleData(int[,] grid)
    {
        data = string.Empty;

        for (int i = 0; i < PuzzleUtils.Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < PuzzleUtils.Constants.GRID_SIZE; j++)
            {
                data += grid[i,j] + (j < PuzzleUtils.Constants.GRID_SIZE - 1 ? PuzzleUtils.Constants.DATA_UNIT_SEPARATOR : PuzzleUtils.Constants.DATA_ROW_SEPARATOR);
            }
        }

        UnityEngine.Debug.Log(data);
    }

    ///<summary>
    ///Returns the sudoku grid as a 2d array.
    ///</summary>
    public int[,] ToGrid()
    {
        int[,] grid = new int[PuzzleUtils.Constants.GRID_SIZE,PuzzleUtils.Constants.GRID_SIZE];

        string[] rows = data.Split(PuzzleUtils.Constants.DATA_ROW_SEPARATOR[0]);

        for (int i = 0; i < PuzzleUtils.Constants.GRID_SIZE; i++)
        {
            string[] units = rows[i].Split(PuzzleUtils.Constants.DATA_UNIT_SEPARATOR[0]);

            for (int j = 0; j < PuzzleUtils.Constants.GRID_SIZE; j++)
            {
                grid[i,j] = int.Parse(units[j]);
            }
        }

        return grid;
    }
}
