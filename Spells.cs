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
   
}

