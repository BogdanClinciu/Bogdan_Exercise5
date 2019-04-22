using UnityEngine;

public class PuzzleBase : MonoBehaviour
{
    protected bool CheckPosition(int x, int y, int value, int[,] grid)
    {
        //check 3x3 cluster
        for (int i = RoundToThree(x); i < RoundToThree(x) + 3; i++)
        {
            for (int j = RoundToThree(y); j < RoundToThree(y) + 3; j++)
            {
                if(grid[i,j] == value)
                {
                    return false;
                }
            }
        }

        //check column and row
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            //check column
            if(grid[x,i] == value)
            {
                return false;
            }
            //check row
            if(grid[i,y] == value)
            {
                return false;
            }
        }

        return true;
    }

    protected int RandomInt()
    {
        return Random.Range(0, Constants.GRID_SIZE);
    }

    private int RoundToThree(int val)
    {
        if(val < 3)
        {
            return 0;
        }
        if (val < 6)
        {
            return 3;
        }
        if(val < 9)
        {
            return 6;
        }

        Debug.Log("Roun To Three Error");
        return -1;
    }
}
