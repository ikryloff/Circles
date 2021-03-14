using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    public Spell [] spellScript;
    public Field field;
    public string bulletName;
    public BuildingManager buildingManager;
    public AttackManager attackManager;
    public Wizard wizard;
    private ObjectsHolder oh;
    private UIManager ui;

    private int [] natureSchoolSpellList = { 7, 8, 9, 10, 11, 12, 13 };
    private int [] natureSchoolCallllList = { 42, 43, 44, 45, 46, 47, 48 };
    private int [] elementalSchoolSpellList = { 0, 1, 2, 3, 4, 5, 6 };
    private int [] elementalSchoolCallList = { 35, 36, 37, 38, 39, 40, 41 };
    private int [] demonologySchoolSpellList = { 14, 15, 16, 17, 18, 19, 20 };
    private int [] demonologySchoolCallList = { 49, 50, 51, 52, 53, 54, 55 };
    private int [] necromancySchoolSpellList = { 21, 22, 23, 24, 25, 26, 27 };
    private int [] necromancySchoolCallList = { 56, 57, 58, 59, 60, 61, 62 };
    private int [] defenciveSchoolSpellList = { 28, 29, 30, 31, 32, 33, 34 };
    private int [] defenciveSchoolCallList = { 63, 64, 65, 66, 67, 68, 69 };


   

    private Dictionary<string, Spell> spellsDictionary;
    private Dictionary<int, Spell> spellsIDDictionary;    

    private void Awake()
    {
        MakeSpellsDictionary ();
        MakeSpellsIDDictionary ();       
    }

    private void MakeSpellsDictionary()
    {
        spellsDictionary = new Dictionary<string, Spell> ();
        foreach ( var item in spellScript )
        {
            spellsDictionary.Add (item.code, item);
        }
    }

    private void MakeSpellsIDDictionary()
    {
        spellsIDDictionary = new Dictionary<int, Spell> ();
        foreach ( var item in spellScript )
        {
            spellsIDDictionary.Add (item.spellID, item);
            PlayerStats.AddSpellToPlayerSpellsIDList (item.spellID);
        }
    }
    

    private void Start()
    {
        oh = ObjectsHolder.Instance;
        field = oh.field;
        buildingManager = oh.buildingManager;
        attackManager = oh.attackManager;
        wizard = oh.wizard;
        ui = oh.uIManager;
    }


    public void FindAndActivateSpell( string spellCode, int top, int bottom, int left, int right, List<Cell> cells )
    {
        print (spellCode);
        Spell spell = GetSpell (spellCode);
        if ( spell != null )
        {
            if ( spell.isTower )
            {
                if ( spell.isBarrier )
                {
                    ExecuteMultipleSpell (spell, cells);
                }
                else if ( spell.isTrap )
                {
                    ExecuteTrapSpell (spell, top, left);
                }
                else
                    ExecuteTowerBuildSpell (spell, top, left);

            }
            else if ( spell.isTowerActive )
            {
                ExecuteTowerSpell (spell, top, bottom, left, right);
            }
            else
            {
                ExecuteActiveSpell (spell, top, bottom, left, right);
            }
        }
        else
        {
            PrintMessage ("Unknown spell!!");
        }
    }

    public Spell GetSpellByID( int id)
    {
        if ( spellsIDDictionary.ContainsKey (id) )
            return spellsIDDictionary [id];
        else
            return spellsIDDictionary [0];

    }

    private void ExecuteTrapSpell( Spell spell, int top, int left )
    {
        buildingManager.BuildTrap (spell, new int [] { top + spell.targetCell [0], left + spell.targetCell [1] });
    }

    private void ExecuteTowerBuildSpell( Spell spell, int top, int left )
    {
        buildingManager.BuildTower (spell, new int [] { top + spell.targetCell [0], left + spell.targetCell [1] });
    }

    private void ExecuteTowerSpell( Spell spell, int top, int bottom, int left, int right )
    {
        if ( spell.code.Equals (Constants.SPELL_CODE_VICTIMS_RETURN) )
        {
            attackManager.ReturnMana (spell, new int [] { top + spell.targetCell [0], left + spell.targetCell [1] });
        }
    }

    private void ExecuteActiveSpell( Spell spell, int top, int bottom, int left, int right )
    {
        int [] arr = spell.CalcTarget (top, bottom, left, right);
        attackManager.AttackEnemies (spell, arr);
    }

    private void ExecuteMultipleSpell( Spell spell, List<Cell> cells )
    {
        buildingManager.BuildBarriers (spell, cells);
    }

    public Spell GetSpell( string code )
    {
        if ( spellsDictionary.ContainsKey (code) )
            return spellsDictionary [code];
        return null;
    }

    public void PrintMessage( string message )
    {
        ui.SetMessage (message);
    }


    public int[] GetNatureListByIndex(int index)
    {
        return index == 0 ? natureSchoolSpellList : natureSchoolCallllList; 
    }

    public int [] GetElementalListByIndex( int index )
    {
        return index == 0 ? elementalSchoolSpellList : elementalSchoolCallList;
    }

    public int [] GetDemonologyListByIndex( int index )
    {
        return index == 0 ? demonologySchoolSpellList : demonologySchoolCallList;
    }

    public int [] GetNecromancyListByIndex( int index )
    {
        return index == 0 ? necromancySchoolSpellList : necromancySchoolCallList;
    }

    public int [] GetDefenciveListByIndex( int index )
    {
        return index == 0 ? defenciveSchoolSpellList : defenciveSchoolCallList;
    }

    public int GetSchoolLearnedSpells( int [] spells , int [] calls)
    {
        int count = 0;

        for ( int i = 0; i < spells.Length; i++ )
        {
            count += PlayerStats.GetPlayerSpellsValueByIndex(spells[i]);
        }

        for ( int i = 0; i < calls.Length; i++ )
        {
            count += PlayerStats.GetPlayerSpellsValueByIndex (calls [i]);
        }

        return count;
    }

}


