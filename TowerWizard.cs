using UnityEngine;

public class TowerWizard : Tower
{
    private FirePoints firePoints;
    public Wizard wizard;
    public Creep targetEnemy;


    public override void Start()
    {
        base.Start ();
        firePoints = ObjectsHolder.Instance.firePoints;
        isAI = false;
        AddTower ();
        CellPosition = -1;
    }


    public override void Attack( float _damage, string school, string spellTarget, GameObject bullet )
    {
        if ( spellTarget == Constants.SPELL_TARGET_ALL )
        {
            for ( int i = 0; i < creepsInLine.Count; i++ )
            {
                if ( creepsInLine[i] != null )
                {
                    BulletHit (creepsInLine [i], _damage, bullet);
                }
            }
            return;
        }

        targetEnemy = GetTarget (spellTarget);

        if ( targetEnemy == null )
        {
            BulletFly (GetEmptyTarget (LinePosition), bullet);
            return;
        }
        if ( !targetEnemy.isDead )
        {
            BulletHit (targetEnemy, _damage, bullet);
        }
    }

    private Creep GetTarget( string spellTarget )
    {
        if ( creepsInLine.Count == 0 )
            return null;
        Creep target = null;
        if ( spellTarget == Constants.SPELL_TARGET_RANDOM_IN_LINE )
        {
            int rand = Random.Range (0, creepsInLine.Count);
            target = creepsInLine [rand];
        }        
        else
        {
            float temp = float.MaxValue;
            for ( int i = 0; i < creepsInLine.Count; i++ )
            {
                float dist = Mathf.Abs (towerTransform.position.x - creepsInLine [i].creepTransform.position.x);
                if ( dist <= temp )
                {
                    target = creepsInLine [i];
                    temp = dist;
                }
            }
        }

        return target;
    }

    public override void CalcDamage( float _damage )
    {
        wizard.CalcDamage (_damage);
    }

    private void BulletFly( Transform target, GameObject bulletPref )
    {
        GameObject bulletGO = Instantiate (bulletPref, firePoints.GetRandomPoint ().position, Quaternion.identity) as GameObject;
        Bullet bullet = bulletGO.GetComponent<Bullet> ();
        if ( bullet != null )
        {
            bullet.SeekEmpty (target);
        }

    }

    private void BulletHit( Creep creep, float _damage, GameObject bulletPref )
    {
        GameObject bulletGO = Instantiate (bulletPref, firePoints.GetRandomPoint ().position, Quaternion.identity) as GameObject;
        Bullet bullet = bulletGO.GetComponent<Bullet> ();
        if ( bullet != null )
        {
            bullet.SeekEnemy (creep, _damage);
        }
        creep.GetClosestTowerAfterHit (this);

    }
}
