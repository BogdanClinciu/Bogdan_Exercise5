using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private Text cellText;

    ///<summary>
    ///Updates the text, and color values of the text component to the givern <paramref name="value"/> and <paramref name="color"/>.
    ///</summary>
    internal void UpdateCellText(int value, Color color)
    {
        if(value == Constants.EMPTY_CELL)
        {
            cellText.enabled = false;
        }
        else
        {
            cellText.text = value.ToString();
            cellText.color = color;
            cellText.enabled = true;
        }
    }
}
