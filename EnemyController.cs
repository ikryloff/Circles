﻿using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Creep> creeps;
    public Line [] lines;
    public Transform [] spawns;
    SpawnPoint [] sp;
    public TowerWizard [] towerWizards;
    public Tower [] sortedTowers;

    public static float displace = 0f;

    private void Awake()
    {
        lines = FindObjectsOfType<Line> ();
        sp = FindObjectsOfType<SpawnPoint> ();
        SortSpawns (sp);
        SortLines (lines);
        towerWizards = FindObjectsOfType<TowerWizard> ();
        SortTowers ();
    }

    // sorting wiz towers to find them by line
    private void SortTowers()
    {
        sortedTowers = new Tower [towerWizards.Length + 1];
        for ( int i = 0; i < towerWizards.Length; i++ )
        {
            sortedTowers [towerWizards [i].LinePosition] = towerWizards [i];
        }
    }

    private void SortSpawns( SpawnPoint [] _sp )
    {
        spawns = new Transform [sp.Length];
        for ( int i = 0; i < _sp.Length; i++ )
        {
            spawns [_sp [i].Line - 1] = _sp [i].GetComponent<Transform> ();
        }
    }

    private void SortLines( Line [] _lines )
    {
        Line [] ls = new Line [9];
        for ( int i = 0; i < _lines.Length; i++ )
        {
            ls [_lines [i].LineNumber] = _lines [i];
        }
        lines = ls;
    }

    public void AddTowerToEnemyList( Tower tower )
    {
        lines [tower.LinePosition].lineTowers.Add (tower);
        GameEvents.current.TowerAppear ();
    }

    public void AddTowerToEnemyList( Trap trap )
    {
        lines [trap.LinePosition].lineTraps.Add (trap);
    }

    public void RemoveTowerFromEnemyList( Tower tower )
    {
        lines [tower.LinePosition].lineTowers.Remove (tower);
        GameEvents.current.TowerAppear ();
    }

    public void RemoveTowerFromEnemyList( Trap trap )
    {
        lines [trap.LinePosition].lineTraps.Remove (trap);
    }

    public void AddCreepToEnemyList( Creep creep )
    {
        lines [creep.GetLinePosition ()].lineCreeps.Add (creep);
        GameEvents.current.EnemyAppear ();
    }

    public void RemoveCreepFromEnemyList( Creep creep )
    {
        lines [creep.GetLinePosition ()].lineCreeps.Remove (creep);
        GameEvents.current.EnemyAppear ();
    }

    public Tower GetTargetTower( Creep creep )
    {
        List<Tower> twrs = lines [creep.GetLinePosition ()].lineTowers;
        Tower target = GetMainTargetTower (creep);
        float temp = float.MaxValue;
        for ( int i = 0; i < twrs.Count; i++ )
        {
            float dist = Mathf.Abs (twrs [i].towerTransform.position.x - creep.creepTransform.position.x);
            if ( dist <= temp )
            {
                target = twrs [i];
                temp = dist;
            }
        }        
        return target;
    }

    public Tower GetMainTargetTower( Creep creep )
    {        
        return sortedTowers [creep.GetLinePosition ()];
    }



    public Creep GetClosestCreep( Tower tower )
    {
        List<Creep> creeps = lines [tower.LinePosition].lineCreeps;
        Creep target = null;
        float temp = float.MaxValue;
        for ( int i = 0; i < creeps.Count; i++ )
        {
            float dist = Mathf.Abs (tower.towerTransform.position.x - creeps [i].creepTransform.position.x);
            if ( dist <= temp )
            {
                target = creeps [i];
                temp = dist;
            }
        }
        return target;
    }

    public static float GetDisplace()
    {
        if ( displace > 0.9999f )
            displace = 0.0001f;
        displace += 0.0001f;
        return displace;
    }
}
