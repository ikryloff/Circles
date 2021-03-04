using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Field field;
    public Wizard wizard;
    private UIManager ui;

    private void Start()
    {
        field = ObjectsHolder.Instance.field;
        wizard = ObjectsHolder.Instance.wizard;
        ui = ObjectsHolder.Instance.uIManager;
    }

    public void BuildTower( Spell spell, int [] cellCoord )
    {
        int cellLine = cellCoord [0];
        int cellNum = cellCoord [1];
        print (cellNum);
        Cell cell = field.GetCell (cellLine, cellNum);
        Vector2 cellPos = cell.transform.position;
        if ( cell.IsEngaged )
        {
            PrintMessage ("Place is Engaged!");
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            return;
        }

        GameObject prefab = GameAssets.instance.GetAssetByString (spell.spellTag);
        GameObject newTower = Instantiate (prefab, new Vector3 (cellPos.x, cellPos.y + 0.3f, prefab.transform.position.z), Quaternion.identity) as GameObject;
        TowerAI tower = newTower.GetComponent<TowerAI> ();
        tower.LinePosition = cellLine;
        tower.CellPosition = cellNum;
        tower.SetCell (cell);
        wizard.ManaWaste (spell.cost);
        tower.cost = spell.cost;
        PrintMessage (spell.spellName);        
    }

    public void BuildBarriers( Spell spell, List<Cell> cells )
    {
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            return;
        }

        if ( spell.code.Equals (Constants.TOWER_CODE_SHIELD) )
        {
            BuildShield (spell, cells);
            return;
        }

        for ( int i = 0; i < cells.Count; i++ )
        {
            if ( cells [i].IsEngaged )
            {
                PrintMessage ("Place is Engaged!");
                return;
            }
        }        
        
        {
            for ( int i = 0; i < cells.Count; i++ )
            {
                Cell cell = cells [i];
                PlaceBuilding (spell, cell, cells.Count);
            }
        }
        
        wizard.ManaWaste (spell.cost);
        PrintMessage (spell.spellName);
        GameEvents.current.TowerAppear ();
    }

    public void BuildTrap( Spell spell, int [] cellCoord )
    {
        int cellLine = cellCoord [0];
        int cellNum = cellCoord [1];
        print (cellNum);
        Cell cell = field.GetCell (cellLine, cellNum);
        Vector2 cellPos = cell.transform.position;
        if ( cell.IsEngaged )
        {
            PrintMessage ("Place is Engaged!");
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            return;
        }

        GameObject prefab = GameAssets.instance.GetAssetByString (spell.spellTag);
        GameObject newTower = Instantiate (prefab, new Vector3 (cellPos.x, cellPos.y + 0.3f, prefab.transform.position.z), Quaternion.identity) as GameObject;
        Trap tower = newTower.GetComponent<Trap> ();
        tower.LinePosition = cellLine;
        tower.CellPosition = cellNum;
        tower.SetCell (cell);
        wizard.ManaWaste (spell.cost);
        tower.cost = spell.cost;
        tower.SetTrapCode (spell.code);
        PrintMessage (spell.spellName);
    }

    public void BuildShield( Spell spell, List<Cell> cells )
    {
        Cell cell = cells [0].LineNumber > cells [1].LineNumber ? cells [0] : cells [1];

        if ( cell.IsEngaged )
        {
            PrintMessage ("Place is Engaged!");
            return;
        }

        PlaceBuilding (spell, cell, 1);        
        wizard.ManaWaste (spell.cost);
        PrintMessage (spell.spellName);
        GameEvents.current.TowerAppear ();
    }

    private void PlaceBuilding( Spell spell, Cell cell, int quantity )
    {
        Vector2 cellPos = cell.transform.position;
        GameObject prefab = GameAssets.instance.GetAssetByString (spell.spellTag);
        GameObject newTower = Instantiate (prefab, new Vector3 (cellPos.x, cellPos.y + 0.3f, prefab.transform.position.z), Quaternion.identity) as GameObject;
        TowerBarrier tower = newTower.GetComponent<TowerBarrier> ();
        tower.LinePosition = cell.LineNumber;
        tower.CellPosition = cell.CellNumber;
        tower.cost = spell.cost / quantity;
        tower.SetCell (cell);
    }

    public void PrintMessage( string message )
    {
        ui.SetMessage (message);
    }
}


