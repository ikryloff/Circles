using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creep : MonoBehaviour, IDamageable
{
    EnemyController ec;
    private CreepAnimation creepAnimation;

    public float xp;
    public float speed;
    public bool IsRanger;
    public string Type;
    public bool isDead;
    public bool isInFight;
    public float hitPoints;
    public float startHp;
    public float hp_norm;
    public float attackRange;
    private int linePosition;
    public float fireDelay;
    public float seekRate = 2f;
    public float fireCountDown = 2f;
    public float seekCountDown = 0f;
    public int damage;
    public string bulletName;
    public GameObject bulletPref;
    public GameObject impactPref;
    public GameObject deathPref;
    private ObjectsHolder oh;
    private SpriteRenderer sprite;

    public Transform creepTransform;
    public Tower targetTower;
    public HealthBar healthBar;
    private GameObject healthBarGO;
    private XPpoints xpPoints;


    private void Awake()
    {
        creepAnimation = GetComponent<CreepAnimation> ();
        healthBarGO = healthBar.gameObject;
        creepTransform = gameObject.transform;
        sprite = GetComponent<SpriteRenderer> ();
    }

    private void Start()
    {
        GameEvents.current.OnTowerAppear += GetClosestTower;
        oh = ObjectsHolder.Instance;
        xpPoints = oh.xpPoints;
        hp_norm = 1f;
        startHp = hitPoints;
        ec = oh.enemyController;
        SetBullet ();
        healthBarGO.SetActive (false);
        RandZPosition (); // to prevent flicking
        ec.AddCreepToEnemyList (this);
        GetClosestTower ();
    }

    void Update()
    {

        if ( isDead )
            return;        
        CheckCollision ();
        if ( !isInFight )
        {
            creepTransform.Translate (Vector2.left * speed * Time.deltaTime);
            
        }        
    }    

    private void RandZPosition()
    {
        float rand = UnityEngine.Random.Range (.0001f, .9999f);
        sprite.sortingOrder = linePosition;
        creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y, creepTransform.position.z + rand);

    }

    public void MoveUp()
    {
        ec.RemoveCreepFromEnemyList (this);
        linePosition -= 1;
        sprite.sortingOrder = linePosition;
        creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y + Constants.CELL_HEIGHT, creepTransform.position.z);
        ec.AddCreepToEnemyList (this);
    }

    public void MoveDown()
    {
        ec.RemoveCreepFromEnemyList (this);
        linePosition += 1;
        sprite.sortingOrder = linePosition;
        creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y - Constants.CELL_HEIGHT, creepTransform.position.z);
        ec.AddCreepToEnemyList (this);
    }

    private void CheckHP()
    {
        if ( hitPoints <= 0 )
        {
            isDead = true;
            isInFight = false;
            GameEvents.current.OnTowerAppear -= GetClosestTower;
            creepAnimation.StopFightAnimation ();
            healthBarGO.SetActive (false);
            ec.RemoveCreepFromEnemyList (this);
            ec.creeps.Remove (this);
            xpPoints.AddPoints (xp, transform.position.x);
            MakeDeath ();        
        }
    }

    public int GetLinePosition()
    {
        return linePosition;
    }

    public void SetLinePosition( int _line )
    {
        linePosition = _line;
    }

    public void CalcDamage( float damage)
    {
        if ( !healthBarGO.activeSelf )
            healthBarGO.SetActive (true);
        hitPoints -= damage;
        hp_norm = hitPoints / startHp;
        healthBar.SetHBSize (hp_norm);
        MakeImpact ();        
        creepAnimation.HitAnimation ();
        CheckHP ();
    }

    private void MakeImpact()
    {
        Instantiate (impactPref, creepTransform.position, Quaternion.identity);
    }

    private void MakeDeath()
    {
        
        Instantiate (deathPref, creepTransform.position, Quaternion.identity);
        Destroy (gameObject);
    }

    public void GetClosestTower()
    {
        targetTower = ec.GetTargetTower (this);
        print ("Update Tower");
    }

    public void GetClosestTowerAfterHit(Tower tower)
    {
        if ( tower == targetTower )
            return;
        targetTower = ec.GetTargetTower (this);
        print ("Update TowerAfterHit");
    }

    public void CheckCollision()
    {
        if ( !isInFight && creepAnimation.IsFightAnimationIsOn () )
            creepAnimation.StopFightAnimation ();

        float dist = Vector2.Distance (targetTower.towerTransform.position, creepTransform.position);
        if ( dist < Constants.CELL_WIDTH * attackRange )
        {
            if ( !isInFight )
            {
                isInFight = true;
                creepAnimation.FaceToAnimation (targetTower);
            }   
            AttackSerias ();
        }
        else
        {
            if ( isInFight )
            {
                isInFight = false;
            }
        }
    }

    private void AttackSerias()
    {
        if ( fireCountDown <= 0 )
        {
            Attack (damage);
            fireCountDown = fireDelay;
        }
        fireCountDown -= Time.deltaTime;
    }
   

    private void Attack( int damage )
    {
        if ( targetTower == null )
        {
            GetClosestTower ();
            return;
        }
        creepAnimation.FaceToAnimation (targetTower);
        creepAnimation.AttackAnimation (targetTower);
        StartCoroutine (FireAfterAnimation (damage));
    }

    public void Fire( float damage)
    {
        if ( IsRanger )
        {
            GameObject bulletGO = Instantiate (bulletPref, creepTransform.position, Quaternion.identity) as GameObject;
            Bullet bullet = bulletGO.GetComponent<Bullet> ();
            if ( bullet != null )
            {
                bullet.SeekTower (targetTower, damage);
            }
        }
        else
        {
            targetTower.CalcDamage (damage);
        }
        targetTower.UpdateTargetAfterHit (this);
    }

    private IEnumerator FireAfterAnimation( float damage )
    {
        yield return new WaitForSeconds (Constants.ANIM_ATTACK_TIME);
        Fire (damage);
    }

    private void SetBullet()
    {
        if(bulletName != "")
            bulletPref = GameAssets.instance.GetAssetByString (bulletName);
        impactPref = GameAssets.instance.GetAssetByString (Constants.BLOOD_IMPACT);
        deathPref = GameAssets.instance.GetAssetByString (Constants.CREEP_DEATH);
    }       
}
