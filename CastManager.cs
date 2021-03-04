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
        spellAction.MakeSpell(CastLine);
    }

    public void DeleteCast()
    {
        CastLine.Clear();
        CellsCount = 0;
    }

    public void ReloadCast()
    {
        StartCoroutine (ReloadCastWithDelay());
    }

    IEnumerator ReloadCastWithDelay()
    {
        yield return new WaitForSeconds (.2f);
        CastSpell ();
        GameEvents.current.CastReset ();
        DeleteCast ();
    }   
}
