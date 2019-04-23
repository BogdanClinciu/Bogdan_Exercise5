using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleDataManager : MonoBehaviour
{
    ///<summary>
    ///Saves a puzzle data list object, to the predifined path.
    ///</summary>
    internal void SaveData(List<PuzzleData> dataList)
    {
		string contents = JsonUtility.ToJson(new PuzzleDataContainer(dataList), true);

        try
        {
            System.IO.File.WriteAllText (Application.persistentDataPath + PuzzleUtils.Constants.PUZZLE_DATA_FILENAME, contents);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
	}

    ///<summary>
    ///Loads a puzzle data list object from the predefined path and returns its data, in the event a document cannot be found a blank one is created;
    ///</summary>
    internal List<PuzzleData> LoadDatabase()
    {
        List<PuzzleData> loadedData = new List<PuzzleData>();

        if (System.IO.File.Exists(Application.persistentDataPath + PuzzleUtils.Constants.PUZZLE_DATA_FILENAME))
        {
            try
            {
				loadedData = JsonUtility.FromJson<PuzzleDataContainer>(System.IO.File.ReadAllText(Application.persistentDataPath + PuzzleUtils.Constants.PUZZLE_DATA_FILENAME)).dataList;
			}
            catch (System.Exception ex)
            {
                Debug.Log (ex.Message);
            }
        }
        else
        {
            try
            {
                Debug.Log(PuzzleUtils.Constants.PUZZLE_FILE_NOT_FOUND);
                SaveData(new List<PuzzleData>());
            }
            catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
        }

        return loadedData;
	}
}
