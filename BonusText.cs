using TMPro;
using UnityEngine;

public class BonusText : MonoBehaviour
{
    public int LevelNumber;
    public TextMeshProUGUI bonusText;

    public void SetText( string txt )
    {
        bonusText = GetComponent<TextMeshProUGUI> ();
        bonusText.text = txt;
    }
}
