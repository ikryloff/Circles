using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPanel : MonoBehaviour
{
    BonusText [] bonusTexts;

    private void Awake()
    {
        bonusTexts = GetComponentsInChildren<BonusText> ();
        SortBonusTexts ();
        SetBonusText ();
    }

    private void SortBonusTexts()
    {
        BonusText [] texts = new BonusText [bonusTexts.Length];
        for ( int i = 0; i < bonusTexts.Length; i++ )
        {
            texts [bonusTexts [i].LevelNumber -1] = bonusTexts [i];
        }
        bonusTexts = texts;
    }

    private void SetBonusText()
    {
        for ( int i = 0; i < bonusTexts.Length; i++ )
        {
            bonusTexts [i].SetText ("Bonus " + bonusTexts[i].LevelNumber);
        }
    }
}
