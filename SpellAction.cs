using System.Collections.Generic;
using UnityEngine;

public class SpellAction : MonoBehaviour
{
    private Spells spells;
    private Field field;
    public List<Cell> Cells;
    public int Top;
    public int Bottom;
    public int Left;
    public int Right;
    public int SpellLenght;
    public string SpellCode;
    private void Awake()
    {
        Cells = new List<Cell> ();

    }
    private void Start()
    {
        field = ObjectsHolder.Instance.field;
        spells = ObjectsHolder.Instance.spells;
    }

    public void MakeSpell( List<Cell> cells )
    {
        if ( cells.Count == 0 ) return;

        DefineSpell (cells);
        spells.FindAndActivateSpell (SpellCode, Top, Bottom, Left, Right, cells);


    }

    public void DefineSpell( List<Cell> cells )
    {
        if ( cells.Count == 0 ) return;
        int left = 17;
        int right = 0;
        int top = 8;
        int bottom = 0;
        foreach ( Cell item in cells )
        {            
            if ( item.LineNumber < top )
                top = item.LineNumber;
            if ( item.LineNumber > bottom )
                bottom = item.LineNumber;
            if ( item.CellNumber < left )
                left = item.CellNumber;
            if ( item.CellNumber > right )
                right = item.CellNumber;
        }
        Top = top;
        Bottom = bottom;
        Left = left;
        Right = right;
        //print ("top " + top + " left " + left + " bottom " + bottom + " right " + right);
        SpellLenght = (Bottom - Top + 1) * (Right - Left + 1);
        int [] spell = new int [SpellLenght + Bottom - Top + 1];
        int count = 0;
        for ( int i = top; i <= bottom; i++ )
        {
            for ( int j = left; j <= right; j++ )
            {
                //Debug.Log (i + " " + j);
                if ( field.GetCell (i, j).IsLoaded )
                    spell [count] = 1;
                else
                    spell [count] = 0;

                count += 1;

                if ( j == right )
                {
                    spell [count] = 2;
                    count++;
                }
            }
        }

        SpellCode = string.Join ("", spell);

    }
}

