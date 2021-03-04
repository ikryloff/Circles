using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private EnemyController ec;
    public TowerWizard [] rawTowers;
    public TowerWizard [] towers;
    private Wizard wizard;
    private UIManager ui;
    public Field field;

    private void Awake()
    {
        rawTowers = FindObjectsOfType<TowerWizard> ();
    }

    private void Start()
    {
        field = ObjectsHolder.Instance.field;
        wizard = ObjectsHolder.Instance.wizard;
        towers = new TowerWizard [rawTowers.Length];
        ui = ObjectsHolder.Instance.uIManager;
        ec = ObjectsHolder.Instance.enemyController;
        SortTowers ();
    }

    private void SortTowers()
    {
        for ( int i = 0; i < rawTowers.Length; i++ )
        {
            towers [rawTowers [i].LinePosition - 1] = rawTowers [i];
        }
    }

    public void AttackEnemies( Spell spell, int [] lines )
    {
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            return;
        }
        GameObject bulletPrefab = GetBulletPrefab (spell.bullet);
        for ( int i = 0; i < lines.Length; i++ )
        {
            if ( lines [i] == 0 || lines [i] == 8 )
                return;
            if ( towers [lines [i] - 1] != null )
                towers [lines [i] - 1].Attack (spell.damage, spell.school, spell.target, bulletPrefab);
        }

        wizard.ManaWaste (spell.cost);
        PrintMessage (spell.spellName);
    }

    public void ReturnMana( Spell spell, int [] cellCoord )
    {
        int cellLine = cellCoord [0];
        int cellNum = cellCoord [1];
        print (cellLine + " " + cellNum);
        Cell cell = field.GetCell (cellLine, cellNum);
        Vector2 cellPos = cell.transform.position;
        if ( !cell.IsEngaged )
        {
            PrintMessage ("Nothing to return!");
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            return;
        }

        List<Tower> lineTowers = ec.lines [cellLine].lineTowers;
        Tower towerToDestroy = null;
        for ( int i = 0; i < lineTowers.Count; i++ )
        {
            Tower towerTmp = lineTowers [i];
            if ( towerTmp != null )
            {               
                if ( towerTmp.GetCell () == field.GetCell (cellLine, cellNum) )
                {
                    towerToDestroy = towerTmp;
                }
            }
            
        }

        if ( towerToDestroy )
        {
            wizard.ManaRecover (towerToDestroy.cost * PlayerStats.GetPlayerManaReturn () * towerToDestroy.hp_norm);
            print ("Return " + (towerToDestroy.cost * PlayerStats.GetPlayerManaReturn () * towerToDestroy.hp_norm));
            towerToDestroy.TowerDeath ();
            PrintMessage (spell.spellName);
            return;
        }  
        
        List<Trap> lineTraps = ec.lines [cellLine].lineTraps;
        Trap trapToDestroy = null;
        for ( int i = 0; i < lineTraps.Count; i++ )
        {
            Trap trapTmp = lineTraps [i];
            if ( trapTmp != null )
            {
                if ( trapTmp.GetCell () == field.GetCell (cellLine, cellNum) )
                {
                    trapToDestroy = trapTmp;
                }
            }
        }

        if ( trapToDestroy )
        {
            wizard.ManaRecover (trapToDestroy.cost * PlayerStats.GetPlayerManaReturn ());
            print ("Return " + (trapToDestroy.cost * PlayerStats.GetPlayerManaReturn ()));
            trapToDestroy.TowerDeath ();
            PrintMessage (spell.spellName);
            return;
        }
        
        if(!trapToDestroy && !towerToDestroy )
            PrintMessage ("Bad attempt!");
    }

    private GameObject GetBulletPrefab( string bulletName )
    {
        return GameAssets.instance.GetAssetByString (bulletName);
    }

    public void PrintMessage( string message )
    {
        ui.SetMessage (message);
    }
}
