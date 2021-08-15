using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastManager : MonoBehaviour
{
    public static int CellsCount = 0;
    private ObjectsHolder objects;
    public List<Cell> CastLine;
    public SpellAction spellAction;

    private void Awake()
    {
        CastLine = new List<Cell> ();
        objects = FindObjectOfType<ObjectsHolder> ();
        spellAction = FindObjectOfType<SpellAction> ();
    }

    public void CastSpell()
    {
        spellAction.MakeSpell (CastLine);
    }

    public void DeleteCast()
    {
        CastLine.Clear ();
        CellsCount = 0;
    }

    public void MakeCast()
    {
        StartCoroutine (MakeCastWithDelay ());
    }

    public void ClearCast()
    {
        DeleteCast ();
        GameEvents.current.CastReset ();
    }

    IEnumerator MakeCastWithDelay()
    {
        yield return new WaitForSeconds (.1f);
        CastSpell ();
        ColorizeSpell (CastLine);
    }

    private void ColorizeSpell( List<Cell> castLine )
    {
        if ( castLine == null )
            return;
        for ( int i = 0; i < castLine.Count; i++ )
        {
            castLine [i].ColorCell ();
        }
    }
}
