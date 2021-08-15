using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private EnemyController ec;
    public TowerWizard [] rawTowers;
    public TowerWizard [] towers;
    private Wizard wizard;
    private CastManager castManager;
    private UIManager ui;
    public Field field;
    private List<TowerWizard> attackTowers;

    private void Awake()
    {
        rawTowers = FindObjectsOfType<TowerWizard> ();
    }

    private void Start()
    {
        field = ObjectsHolder.Instance.field;
        wizard = ObjectsHolder.Instance.wizard;
        towers = new TowerWizard [rawTowers.Length];
        attackTowers = new List<TowerWizard> ();
        ui = ObjectsHolder.Instance.uIManager;
        ec = ObjectsHolder.Instance.enemyController;
        castManager = ObjectsHolder.Instance.castManager;
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
            EndCasting ();
            return;
        }

        GameObject bulletPrefab = GetBulletPrefab (spell.bullet);
        attackTowers.Clear ();

        for ( int i = 0; i < lines.Length; i++ )
        {
            if ( lines [i] != 0 && lines [i] != 8 )
            {
                attackTowers.Add (towers [lines [i] - 1]);
            }
        }

        if ( attackTowers.Count == 0 )
        {
            PrintMessage ("Can`t do that here!");
            EndCasting ();
            return;
        }

        StartCoroutine (PrepareSpell (Constants.ATTACK, spell, null, null, null, bulletPrefab, attackTowers));
        wizard.ManaWaste (spell.cost);
        PrintMessage (spell.spellName);
    }

    public void SlowEnemieMoving( Spell spell, int [] lines )
    {
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            EndCasting (); ;
            return;
        }
        for ( int i = 0; i < lines.Length; i++ )
        {
            if ( lines [i] == 0 || lines [i] == 8 )
                return;
            if ( towers [lines [i] - 1] != null )
                StartCoroutine (PrepareSpell (Constants.SLOW, spell, null, null, towers [lines [i] - 1], null, null));
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
            EndCasting ();
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            EndCasting ();
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
            StartCoroutine (PrepareSpell (Constants.RETURN_TOWER, spell, towerToDestroy, null, null, null, null));
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
            StartCoroutine (PrepareSpell (Constants.RETURN_TRAP, spell, null, trapToDestroy, null, null, null));
            wizard.ManaWaste (spell.cost);
            PrintMessage (spell.spellName);
            return;
        }

        if ( !trapToDestroy && !towerToDestroy )
        {
            PrintMessage ("Bad attempt!");
            EndCasting ();
        }
    }

    public void HealTower( Spell spell, int [] cellCoord )
    {
        int cellLine = cellCoord [0];
        int cellNum = cellCoord [1];
        print (cellLine + " " + cellNum);
        Cell cell = field.GetCell (cellLine, cellNum);
        Vector2 cellPos = cell.transform.position;
        if ( !cell.IsEngaged )
        {
            PrintMessage ("Nothing to heal!");
            EndCasting ();
            return;
        }
        if ( spell.cost > wizard.GetManapoints () )
        {
            PrintMessage ("You have no mana!");
            EndCasting ();
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
            StartCoroutine (PrepareSpell (Constants.HEAL, spell, towerToHeal, null, null, null, null));
            wizard.ManaWaste (spell.cost);
            PrintMessage (spell.spellName);
            return;
        }

        if ( !towerToHeal )
        {
            PrintMessage ("Bad attempt!");
            EndCasting ();
        }
    }

    private GameObject GetBulletPrefab( string bulletName )
    {
        return GameAssets.instance.GetAssetByString (bulletName);
    }

    public void PrintMessage( string message )
    {
        ui.SetMessage (message);
    }



    IEnumerator PrepareSpell( string spellType, Spell spell, Tower tower, Trap trap, TowerWizard towerWizard, GameObject bulletPrefab, List<TowerWizard> atTowers )
    {
        Wizard.StopCasting = true;
        ui.SetPrepareIcon (spell.spellID);
        float time = spell.prepareTime;
        float perc = Time.deltaTime / time * 100;
        float value = 100; ;
        while ( time > 0 )
        {
            ui.SetPrepareValue (value);
            time -= Time.deltaTime;
            value -= perc;
            yield return null;
        }
        ui.SetPrepareValue (0);

        if ( spellType.Equals (Constants.ATTACK) )
        {
            for ( int i = 0; i < attackTowers.Count; i++ )
            {
                attackTowers [i].Attack (spell.damage, spell.school, spell.target, bulletPrefab);
            }
        }
        else if ( spellType.Equals (Constants.SLOW) )
        {
            towerWizard.SlowEnemyMoving (spell.school, spell.target);
        }
        else if ( spellType.Equals (Constants.HEAL) )
        {
            if ( tower )
            {
                float healPoints = Constants.TOWER_HEAL_POINTS;
                tower.HealTower (healPoints);
            }
            else
                PrintMessage ("Too late");
        }

        else if ( spellType.Equals (Constants.RETURN_TOWER) )
        {
            if ( tower )
            {
                wizard.ManaRecover (tower.cost * PlayerStats.GetPlayerManaReturn () * tower.hp_norm);
                print ("Return " + (tower.cost * PlayerStats.GetPlayerManaReturn () * tower.hp_norm));
                tower.TowerDeath ();
            }
            else
                PrintMessage ("No target");
        }

        else if ( spellType.Equals (Constants.RETURN_TRAP) )
        {
            if ( trap )
            {
                wizard.ManaRecover (trap.cost * PlayerStats.GetPlayerManaReturn ());
                print ("Return " + (trap.cost * PlayerStats.GetPlayerManaReturn ()));
                trap.TowerDeath ();
            }
            else
                PrintMessage ("No target");
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
}
