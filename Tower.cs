using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, IDamageable
{
    public EnemyController ec;

    public float hitPoints;
    public float damage;
    public float range;
    public float fireRate;
    public int cost;
    protected float fireCountDown = 0f;
    public string school;
    public bool IsDead;
    public int LinePosition;
    public int CellPosition;
    public float hp_norm;
    public float startHp;
    public bool isAI;
    private Cell cell;


    public Transform firePoint;
    public Transform towerTransform;
    public List<Creep> creepsInLine;
    public List<Creep> creeps;

    public ParticleSystem fog;

    protected void Awake()
    {
        towerTransform = transform;
        fog = transform.GetComponentInChildren<ParticleSystem> ();
    }

    public virtual void Start()
    {
        ec = ObjectsHolder.Instance.enemyController;
        creepsInLine = ec.lines [LinePosition].lineCreeps;
        creeps = ec.creeps;
        UpdateTarget ();       

    }

    public virtual void UpdateTarget()
    {
    }

    public virtual void HealTower(float hp)
    {
    }

    public virtual void UpdateTargetAfterHit( Creep creep )
    {
        
    }

    public virtual void TowerDeath()
    {
    }

    public virtual void Attack( float damage, string school, string spellTarget, GameObject bullet )
    {
    }

    public void PlayFog()
    {
        fog.Play ();
    }

    public void AddTower()
    {
        ec.AddTowerToEnemyList (this);

    }

    public void AddTower( Trap trap)
    {
        ec.AddTowerToEnemyList (trap);

    }
    public void RemoveTower()
    {
        FreeCell ();
        ec.RemoveTowerFromEnemyList (this);
        Destroy (gameObject);

    }

    public void RemoveTower( Trap trap )
    {
        FreeCell ();
        ec.RemoveTowerFromEnemyList (trap);
        Destroy (gameObject);

    }

    public void SetCell( Cell _cell )
    {
        cell = _cell;
        cell.IsEngaged = true;
    }

    public Cell GetCell( )
    {
        return cell;
    }

    public void FreeCell()
    {
        cell.IsEngaged = false;
    }

    public Transform GetEmptyTarget( int _line )
    {
        return ec.spawns [_line - 1];
    }

    public virtual void CalcDamage( float damage )
    {
    }
}


