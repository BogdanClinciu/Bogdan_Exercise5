namespace PuzzleUtils
{
    ///<summary>
    ///Contains constant values commonly used to generate sudoku puzzles.
    ///</summary>
    internal class Constants
    {
        internal const string PUZZLE_LABEL = "Puzzle no. ";
        internal const string PUZZLE_LABEL_NONE = "No puzzle";
        internal const string DATA_UNIT_SEPARATOR = ",";
        internal const string DATA_ROW_SEPARATOR = ";";

        internal const string PUZZLE_DATA_FILENAME = "/Puzzles.json";
        internal const string PUZZLE_FILE_NOT_FOUND = "Unable to find puzzle data file. Writing blank puzzle data file.";

        internal const int CELLS_TO_CLEAR = 15;
        internal const int GRID_SIZE = 9;
        internal const int EMPTY_CELL = 0;

    }

    ///<summary>
    ///Contains number utilities for generating sudoku puzzles (currently only contains method to return random valid cell number array).
    ///</summary>
    internal class NumberUtils
    {
        internal static int[] RANDOMIZED_RANGE
        {
            get => Randomize(numberRange);
        }

        private static int[] numberRange = new int[] {1,2,3,4,5,6,7,8,9};


        ///<summary>
        ///Returns a randomized copy of the given <paramref name="nums"/> array.
        ///</summary>
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
}
