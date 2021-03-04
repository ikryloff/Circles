using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public int LineNumber;
    public Cell [] cells;
    public List<Tower> lineTowers;
    public List<Trap> lineTraps;
    public List<Creep> lineCreeps;

    private void Awake()
    {
        lineTowers = new List<Tower> ();
        lineCreeps = new List<Creep> ();
    }

    public void DefineCells()
    {
        cells = GetComponentsInChildren<Cell> ();
        for ( int i = 0; i < cells.Length; i++ )
        {
            cells [i].LineNumber = LineNumber;
        }
    }

    public Cell GetCell( int num )
    {
        for ( int i = 0; i < cells.Length; i++ )
        {
            if ( num == cells [i].CellNumber )
                return cells [i];
        }
        return null;
    }
}
