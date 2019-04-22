public class Constants
{
    public const string PUZZLE_LABEL = "Puzzle no. ";
    public const string PUZZLE_LABEL_NONE = "No puzzle";
    public const string DATA_UNIT_SEPARATOR = ",";
    public const string DATA_ROW_SEPARATOR = ";";

    public const string PUZZLE_DATA_FILENAME = "/Puzzles.json";
    public const string PUZZLE_FILE_NOT_FOUND = "Unable to find puzzle data file. Writing blank puzzle data file.";

    public const int GRID_SIZE = 9;
    public const int EMPTY_CELL = 0;

    public static int[] RANDOMIZED_RANGE
    {
        get => Randomize(numberRange);
    }


    private static int[] numberRange = new int[] {1,2,3,4,5,6,7,8,9};

    private static int[] Randomize(int[] nums)
    {
        int[] randomized = new int[nums.Length];
        System.Array.Copy(nums, randomized, nums.Length);

        for (int t = 0; t < randomized.Length; t++ )
        {
            int tmp = randomized[t];
            int r = UnityEngine.Random.Range(t, randomized.Length);
            randomized[t] = randomized[r];
            randomized[r] = tmp;
        }

        return randomized;
    }
}
