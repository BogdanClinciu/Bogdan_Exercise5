public class PuzzleGenerator : PuzzleBase
{
    internal int[,] GeneratePuzzle()
    {
        int[,] grid = new int[Constants.GRID_SIZE,Constants.GRID_SIZE];

        PlaceValues(0, grid);

        while(!CheckPuzzleSolvable(grid))
        {
            grid = new int[Constants.GRID_SIZE,Constants.GRID_SIZE];
            PlaceValues(0, grid);
            UnityEngine.Debug.Log("Bad puzzle starting over");
        }

        RemoveRandom(Constants.CELLS_TO_CLEAR, grid);

        return grid;
    }

    private void PlaceValues(int rowY, int[,] grid)
    {
        if(rowY == Constants.GRID_SIZE)
        {
            return;
        }

        int[] nums = Constants.RANDOMIZED_RANGE;

        for (int x = 0; x < Constants.GRID_SIZE; x++)
        {
            for (int n = 0; n < nums.Length; n++)
            {
                //method from PuzzleBase
                if(CheckPosition(x, rowY, nums[n], grid))
                {
                    grid[x,rowY] = nums[n];
                    PlaceValues(rowY + 1, grid);
                }
            }
        }
    }

    private bool CheckPuzzleSolvable(int[,] grid)
    {
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                if(grid[i,j] == 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void RemoveRandom(int cellsToClear, int[,] grid)
    {
        int removed = 0;
        int randX = 0;
        int randY = 0;

        while(removed <= cellsToClear)
        {
            randX = RandomInt();
            randY = RandomInt();

            if(grid[randX,randY] != 0)
            {
                grid[randX,randY] = 0;
                removed ++;
            }
        }
    }
}
