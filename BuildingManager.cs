using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Field field;
    public Wizard wizard;
    private UIManager ui;
    private CastManager castManager;

    private void Start()
    {
        field = ObjectsHolder.Instance.field;
        wizard = ObjectsHolder.Instance.wizard;
        ui = ObjectsHolder.Instance.uIManager;
        castManager = ObjectsHolder.Instance.castManager;
    }

    public void BuildTower( Spell spell, int [] cellCoord )
    {
        int cellLine = cellCoord [0];
        int cellNum = cellCoord [1];
        print (cellNum);
        Cell cell = field.GetCell (cellLine, cellNum);


        if ( spell.code.Equals (Constants.TOWER_CODE_WOOD_WALL) )
        {
            Cell cell1 = field.GetCell (cell.LineNumber + 1, cell.CellNumber + 1);
            Cell cell2 = field.GetCell (cell1.LineNumber + 1, cell1.CellNumber);
            Cell [] cellsWoodWall = new Cell [] { cell, cell1, cell2 };
            for ( int i = 0; i < 3; i++ )
            {
                if ( cellsWoodWall [i].IsEngaged )
                {
                    PrintMessage ("Can`t do that here!");
                    EndCasting ();
                    return;
                }
            }
        }

        if ( spell.code.Equals (Constants.TOWER_CODE_STONE_WALL) )
        {
            Cell cell1 = field.GetCell (cell.LineNumber + 1, cell.CellNumber);
            Cell cell2 = field.GetCell (cell1.LineNumber + 1, cell1.CellNumber);
            Cell [] cellsStoneWall = new Cell [] { cell, cell1, cell2 };
            for ( int i = 0; i < 3; i++ )
            {
                if ( cellsStoneWall [i].IsEngaged )
                {
                    PrintMessage ("Can`t do that here!");
                    EndCasting ();
                    return;
                }
            }
        }

        if ( cell.IsEngaged )
        {
            PrintMessage ("Can`t do that here!");
            EndCasting ();
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            EndCasting ();
            return;
        }


        StartCoroutine (PrepareBuilding (Constants.TOWER, spell, cell, null));
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
            PrintMessage ("Can`t do that here!");
            EndCasting ();
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            EndCasting ();
            return;
        }
        
        StartCoroutine (PrepareBuilding (Constants.TRAP, spell, cell, null));
    }


    IEnumerator PrepareBuilding( string spellType, Spell spell, Cell cell, List<Cell> cells )
    {
        Wizard.StopCasting = true;
        ui.SetPrepareIcon (spell.spellID);
        wizard.ManaWaste (spell.cost);
        PrintMessage (spell.spellName);
        float dp;
        float time = spell.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 100;
        while ( time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value -= perc;
            yield return null;
        }
        ui.SetPrepareValue (0);

        if ( spellType.Equals (Constants.TOWER) )
        {
            if ( spell.code.Equals (Constants.TOWER_CODE_WOOD_WALL) )
            {
                Tower w_wall;
                Vector2 cellPos0 = cell.transform.position;
                Cell cell1 = field.GetCell (cell.LineNumber + 1, cell.CellNumber + 1);
                Cell cell2 = field.GetCell (cell1.LineNumber + 1, cell1.CellNumber);
                Cell [] cellsWoodWall = new Cell [] { cell, cell1, cell2 };

                GameObject pref = GameAssets.instance.GetAssetByString (spell.spellTag);

                for ( int i = 0; i < 3; i++ )
                {
                    GameObject ww = Instantiate (pref, new Vector3 (cellsWoodWall [i].transform.position.x, cellsWoodWall [i].transform.position.y + 0.3f, pref.transform.position.z + EnemyController.GetDisplace ()), Quaternion.identity) as GameObject;
                    w_wall = ww.GetComponent<TowerBarrier> ();
                    w_wall.LinePosition = cellsWoodWall [i].LineNumber;
                    w_wall.CellPosition = cellsWoodWall [i].CellNumber;
                    w_wall.cost = spell.cost / 3;
                    w_wall.SetCell (cellsWoodWall [i]);
                }
                
            }
            else if ( spell.code.Equals (Constants.TOWER_CODE_STONE_WALL) )
            {
                Tower s_wall;
                Vector2 cellPos0 = cell.transform.position;
                Cell cell1 = field.GetCell (cell.LineNumber + 1, cell.CellNumber);
                Cell cell2 = field.GetCell (cell1.LineNumber + 1, cell1.CellNumber);
                Cell [] cellsStoneWall = new Cell [] { cell, cell1, cell2 };

                GameObject pref = GameAssets.instance.GetAssetByString (spell.spellTag);

                for ( int i = 0; i < 3; i++ )
                {
                    GameObject ww = Instantiate (pref, new Vector3 (cellsStoneWall [i].transform.position.x, cellsStoneWall [i].transform.position.y + 0.3f, pref.transform.position.z + EnemyController.GetDisplace ()), Quaternion.identity) as GameObject;
                    s_wall = ww.GetComponent<TowerBarrier> ();
                    s_wall.LinePosition = cellsStoneWall [i].LineNumber;
                    s_wall.CellPosition = cellsStoneWall [i].CellNumber;
                    s_wall.cost = spell.cost / 3;
                    s_wall.SetCell (cellsStoneWall [i]);
                }
            }
            else
            {
                Tower tower;
                Vector2 cellPos = cell.transform.position;
                dp = EnemyController.GetDisplace ();
                GameObject prefab = GameAssets.instance.GetAssetByString (spell.spellTag);
                GameObject newTower = Instantiate (prefab, new Vector3 (cellPos.x, cellPos.y + 0.3f, prefab.transform.position.z + dp), Quaternion.identity) as GameObject;

                if ( spell.code.Equals (Constants.TOWER_CODE_SHIELD) )
                    tower = newTower.GetComponent<TowerBarrier> ();
                else
                    tower = newTower.GetComponent<TowerAI> ();

                tower.LinePosition = cell.LineNumber;
                tower.CellPosition = cell.CellNumber;
                tower.cost = spell.cost;
                tower.SetCell (cell);
            }
        }

        else if ( spellType.Equals (Constants.TRAP) )
        {
            Vector2 cellPos = cell.transform.position;
            GameObject prefab = GameAssets.instance.GetAssetByString (spell.spellTag);
            dp = EnemyController.GetDisplace ();
            GameObject newTrap = Instantiate (prefab, new Vector3 (cellPos.x, cellPos.y + 0.3f, prefab.transform.position.z + dp), Quaternion.identity) as GameObject;
            Trap trap = newTrap.GetComponent<Trap> ();
            trap.LinePosition = cell.LineNumber;
            trap.CellPosition = cell.CellNumber;
            trap.SetCell (cell);
            wizard.ManaWaste (spell.cost);
            trap.cost = spell.cost;
            trap.SetTrapCode (spell.code);            
        }
        
        
        EndCasting ();
    }

    private void EndCasting()
    {
        GameEvents.current.CastReset ();
        castManager.ClearCast ();
        Wizard.StopCasting = false;
        ui.SetPrepareValue (100);
        ui.SetDefaultPrepareIcon ();
    }

    public void PrintMessage( string message )
    {
        ui.SetMessage (message);
    }
}


