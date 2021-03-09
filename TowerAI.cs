using System.Collections;
using UnityEngine;

public class TowerAI : Tower
{
    public HealthBar healthBar;
    public float startHp;
    private SpriteRenderer sprite;
    private TowerAnimation towerAnimation;
    public bool IsRanger;
    public bool IsMultiple;
    private GameObject healthBarGO;
    public string bulletName;
    public string deathName;
    public string impactName;
    public GameObject bulletPref;
    public GameObject impactPref;
    public GameObject deathPref;
    public Creep targetCreep;
    public string attackType; // 0 - random, 1 - nearest, 2 - all

    private new void Awake()
    {
        base.Awake ();
        healthBarGO = healthBar.gameObject;
        sprite = GetComponent<SpriteRenderer> ();
        towerAnimation = GetComponent<TowerAnimation> ();
    }
    public override void Start()
    {
        GameEvents.current.OnEnemyAppear += UpdateTarget;
        base.Start ();
        isAI = true;
        AddTower ();
        hp_norm = 1f;
        fireCountDown = 1;
        startHp = hitPoints;
        sprite.sortingOrder = LinePosition;
        healthBarGO.SetActive (false);
        SetBullet ();
        UpdateTarget ();
    }

    public void Update()
    {
        CheckCollision ();
    }

    private void CheckCollision()
    {
        if ( hitPoints > 0 && targetCreep )
        {
            float dist = Vector2.Distance (transform.position, targetCreep.creepTransform.position);
            if ( dist <= range * Constants.CELL_WIDTH )
            {
                AttackSerias ();
            }
        }
    }

    private void CheckTower()
    {
        if ( hitPoints <= 0 )
        {
            TowerDeath ();
        }
    }

    public override void TowerDeath()
    {
        GameEvents.current.OnEnemyAppear -= UpdateTarget;
        MakeDeath ();
        RemoveTower ();
    }

    public override void UpdateTarget()
    {
        if ( creepsInLine.Count == 0 )
            return;
        GetClosestCreep ();

        if ( targetCreep )
        {
            towerAnimation.FaceToAnimation (targetCreep.creepTransform);
        }
        else
            towerAnimation.DefaultAnimation ();

    }

    private void MakeImpact()
    {
        Instantiate (impactPref, towerTransform.position, Quaternion.identity);
    }

    private void MakeDeath()
    {
        Instantiate (deathPref, towerTransform.position, Quaternion.identity);
    }

    public void GetClosestCreep()
    {
        targetCreep = ec.GetClosestCreep (this);
        print ("Update enemy");
    }

    private void AttackSerias()
    {
        if ( fireCountDown <= 0 )
        {
            Attack (damage, "", "", null);
            fireCountDown = fireRate; // 1 per second
        }
        fireCountDown -= Time.deltaTime;
    }

    public override void Attack( float damage, string school, string spellTarget, GameObject bullet )
    {
        if ( creepsInLine.Count <= 0 )
            return;
        if ( IsRanger )
        {
            Creep enemy = null;
            int rand = Random.Range (0, creepsInLine.Count);
            if ( attackType.Equals (Constants.SPELL_TARGET_RANDOM_IN_LINE) )
            {
                enemy = creepsInLine [rand];
            }
            else if ( attackType.Equals (Constants.SPELL_TARGET_NEAREST_IN_LINE) )
            {
                enemy = targetCreep;
            }

            if ( !enemy.isDead )
            {
                towerAnimation.AttackAnimation (enemy);
                StartCoroutine (FireAfterAnimation (enemy, damage));
            }
        }
        else
        {
            Creep creep = targetCreep;
            if ( !creep.isDead )
            {
                towerAnimation.AttackAnimation (creep);
                StartCoroutine (FireAfterAnimation (creep, damage));
            }

        }

    }

    public void Fire( Creep creep, float damage )
    {
        if ( IsRanger )
        {
            GameObject bulletGO = Instantiate (bulletPref, firePoint.position, Quaternion.identity) as GameObject;
            Bullet bullet = bulletGO.GetComponent<Bullet> ();
            if ( bullet != null )
            {
                bullet.SeekEnemy (creep, damage);
            }
        }
        else
        {
            creep.CalcDamage (damage);
        }
        creep.GetClosestTowerAfterHit (this);
    }

    public override void CalcDamage( float damage )
    {
        if ( !healthBarGO.activeSelf )
            healthBarGO.SetActive (true);
        hitPoints -= damage;
        hp_norm = hitPoints / startHp;
        healthBar.SetHBSize (hp_norm);
        towerAnimation.HitAnimation (targetCreep.creepTransform);
        MakeImpact ();
        CheckTower ();
    }

    private IEnumerator FireAfterAnimation( Creep enemy, float damage )
    {
        yield return new WaitForSeconds (Constants.ANIM_ATTACK_TIME);
        Fire (enemy, damage);
    }

    private void SetBullet()
    {
        if ( IsRanger )
            bulletPref = GameAssets.instance.GetAssetByString (bulletName);
        if ( deathName != "" )
            deathPref = GameAssets.instance.GetAssetByString (deathName);
        if ( impactName != "" )
            impactPref = GameAssets.instance.GetAssetByString (impactName);

    }

    public override void UpdateTargetAfterHit(Creep creep )
    {
        if ( creep == targetCreep )
            return;
        UpdateTarget ();
    }

}

