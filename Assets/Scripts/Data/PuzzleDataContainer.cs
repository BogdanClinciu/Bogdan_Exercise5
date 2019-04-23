using System.Collections.Generic;

[System.Serializable]
public class PuzzleDataContainer
{
    public List<PuzzleData> dataList;

    ///<summary>
    ///Constructor. Creates a PuzzleDataContainer with the contained data take from the given PuzzleData list, <paramref name="data"/>.
    ///</summary>
    public PuzzleDataContainer(List<PuzzleData> data)
    {
        dataList = new List<PuzzleData>(data);
    }
}
