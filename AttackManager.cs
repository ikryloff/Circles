using System.Collections;
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
    private bool isCasting;

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
        if ( isCasting )
            return;
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
                StartCoroutine(PrepareForAttack (spell, bulletPrefab, towers [lines [i] - 1]));               
        }

        wizard.ManaWaste (spell.cost);
        PrintMessage (spell.spellName);
    }

    public void SlowEnemieMoving( Spell spell, int [] lines )
    {
        if ( isCasting )
            return;
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            return;
        }
        for ( int i = 0; i < lines.Length; i++ )
        {
            if ( lines [i] == 0 || lines [i] == 8 )
                return;
            if ( towers [lines [i] - 1] != null )
                 StartCoroutine (PrepareForSlow (spell, towers [lines [i] - 1]));
        }

        wizard.ManaWaste (spell.cost);
        PrintMessage (spell.spellName);
    }

    public void ReturnMana( Spell spell, int [] cellCoord )
    {
        if ( isCasting )
            return;
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
            print (towerToDestroy.name);
            StartCoroutine(PrepareForReturnMana (spell, towerToDestroy));
            wizard.ManaWaste (spell.cost);
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
            StartCoroutine(PrepareForReturnMana (spell, trapToDestroy));
            wizard.ManaWaste (spell.cost);
            PrintMessage (spell.spellName);
            return;
        }
        
        if(!trapToDestroy && !towerToDestroy )
            PrintMessage ("Bad attempt!");
    }

    public void HealTower( Spell spell, int [] cellCoord )
    {
        if ( isCasting )
            return;
        int cellLine = cellCoord [0];
        int cellNum = cellCoord [1];
        print (cellLine + " " + cellNum);
        Cell cell = field.GetCell (cellLine, cellNum);
        Vector2 cellPos = cell.transform.position;
        if ( !cell.IsEngaged )
        {
            PrintMessage ("Nothing to heal!");
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            return;
        }

        List<Tower> lineTowers = ec.lines [cellLine].lineTowers;
        Tower towerToHeal = null;
        for ( int i = 0; i < lineTowers.Count; i++ )
        {
            Tower towerTmp = lineTowers [i];
            if ( towerTmp != null )
            {
                if ( towerTmp.GetCell () == field.GetCell (cellLine, cellNum) )
                {
                    towerToHeal = towerTmp;
                }
            }
        }

        if ( towerToHeal )
        {            
            StartCoroutine (PrepareForHeal (spell, towerToHeal));
            wizard.ManaWaste (spell.cost);
            PrintMessage (spell.spellName);
            return;
        }      

        if (!towerToHeal )
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

    IEnumerator PrepareForAttack(Spell spell, GameObject bulletPrefab, TowerWizard tower )
    {
        isCasting = true;
        float time = spell.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 0;
        while (time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value += perc;
            yield return null;
        }
        ui.SetPrepareValue (100);
        tower.Attack (spell.damage, spell.school, spell.target, bulletPrefab);
        isCasting = false;
    }

    IEnumerator PrepareForSlow( Spell spell, TowerWizard tower )
    {
        isCasting = true;
        float time = spell.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 0; ;
        while ( time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value += perc;
            yield return null;
        }
        ui.SetPrepareValue (100);
        tower.SlowEnemyMoving (spell.school, spell.target);
        isCasting = false;
    }

    IEnumerator PrepareForHeal( Spell spell, Tower tower )
    {
        isCasting = true;
        float time = spell.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 0; ;
        while ( time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value += perc;
            yield return null;
        }
        ui.SetPrepareValue (100);
        float healPoints = Constants.TOWER_HEAL_POINTS;
        tower.HealTower (healPoints); ;
        isCasting = false;
    }

    IEnumerator PrepareForReturnMana( Spell spell, Tower tower )
    {        
        isCasting = true;
        float time = spell.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 0; ;
        while ( time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value += perc;
            yield return null;
        }
        ui.SetPrepareValue (100);
        wizard.ManaRecover (tower.cost * PlayerStats.GetPlayerManaReturn () * tower.hp_norm);
        print ("Return " + (tower.cost * PlayerStats.GetPlayerManaReturn () * tower.hp_norm));
        tower.TowerDeath ();
        isCasting = false;
    }

    IEnumerator PrepareForReturnMana( Spell spell, Trap trap )
    {
        isCasting = true;
        float time = spell.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 0; ;
        while ( time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value += perc;
            yield return null;
        }
        ui.SetPrepareValue (100);
        wizard.ManaRecover (trap.cost * PlayerStats.GetPlayerManaReturn () * trap.hp_norm);
        print ("Return " + (trap.cost * PlayerStats.GetPlayerManaReturn () * trap.hp_norm));
        trap.TowerDeath ();
        isCasting = false;
    }
}
