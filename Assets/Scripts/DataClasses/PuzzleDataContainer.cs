using System.Collections.Generic;

[System.Serializable]
public class PuzzleDataContainer
{
    public List<PuzzleData> dataList;

    public PuzzleDataContainer(List<PuzzleData> data)
    {
        dataList = new List<PuzzleData>(data);
    }
}
