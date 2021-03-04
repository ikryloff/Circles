using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    public int spellID;
    public string spellName;
    public string description;
    public string spellTag;
    public int level;
    public bool isTower;
    public bool isBarrier;
    public bool isTrap;
    public bool isTowerActive;
    public string school;
    public string target;
    public string code;
    public string bullet;
    public int damage;
    public int cost;
    public int targetRow; // 8 - all, 1 - top, 2 - bottom, 3 - center, 4 - all inside, 0 - tower
    public int [] targetCell;

    public int [] CalcTarget(int top, int bottom, int left, int right)
    {
        if ( isTower )
            return targetCell;
        if ( targetRow == 8 )
            return new int [] { 1, 2, 3, 4, 5, 6, 7};
        if (targetRow == 1 )
            return new int [] { top };
        if ( targetRow == 2 )
            return new int [] { bottom };
        if ( targetRow == 3 )
            return new int [] { bottom - (bottom - top) / 2 };
        if (targetRow == 4 )
        {
            int [] rows = new int [bottom - top + 1];
            int count = bottom;
            for ( int i = 0; i < rows.Length; i++ )
            {
                rows [i] = count;
                count -= 1;
            }
            return rows;
        }  
        return null;
    }
}

