using UnityEngine;

public class Field : MonoBehaviour
{
    public Line [] Lines;

    private void Awake()
    {
        Lines = GetComponentsInChildren<Line> ();
        DefineLines (Lines);

    }

    private void DefineLines( Line [] lines )
    {
        for ( int i = 0; i < lines.Length; i++ )
        {
            lines [i].DefineCells ();
        }
    }

    public Line GetLine( int num )
    {
        for ( int i = 0; i < Lines.Length; i++ )
        {
            if ( num == Lines [i].LineNumber )
                return Lines [i];
        }
        return null;
    }

    public Cell GetCell( int lineNumber, int cellNumber )
    {
        return GetLine (lineNumber).GetCell (cellNumber);
    }
}
