using System.Collections;
using UnityEngine;

public class Creep : MonoBehaviour, IDamageable
{
    EnemyController ec;
    private CreepAnimation creepAnimation;
    [SerializeField]
    private string enemyType;

    private float xp;
    private float speed;
    private bool isRanger;
    private bool isDead;
    private bool isSlow;
    private bool isInFight;
    private float hitPoints;
    private float startHp;
    private float hp_norm;
    private float attackRange;
    private int linePosition;
    private float fireDelay;
    private float fireCountDown;
    private float damage;
    private string bulletName;



    public GameObject bulletPref;
    public GameObject impactPref;
    public GameObject deathPref;
    private ObjectsHolder oh;
    private SpriteRenderer sprite;

    public Transform creepTransform;
    public Tower targetTower;
    public Tower mainTargetTower;
    public HealthBar healthBar;
    private GameObject healthBarGO;
    private XPpoints xpPoints;
    [SerializeField]
    private ParticleSystem suppressionWind, defenceAffect;

    //private float time;

    private void Awake()
    {
        suppressionWind.Stop ();
        creepAnimation = GetComponent<CreepAnimation> ();
        healthBarGO = healthBar.gameObject;
        creepTransform = gameObject.transform;
        sprite = GetComponent<SpriteRenderer> ();
        SetCreepProperties ();
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
        DisplaceZPosition (); // to prevent flicking
        ec.AddCreepToEnemyList (this);
        GetMainTower ();
        GetClosestTower ();
        //time = Time.time;
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

    private void SetCreepProperties()
    {
        xp = EnemyProperties.GetXP (enemyType);
        speed = EnemyProperties.GetSpeed (enemyType);
        hitPoints = EnemyProperties.GetHP (enemyType);
        attackRange = EnemyProperties.GetAttackRange (enemyType);
        fireDelay = EnemyProperties.GetFireDelay (enemyType);
        damage = EnemyProperties.GetDamage (enemyType);
        isRanger = EnemyProperties.IsRanger (enemyType);
        if ( isRanger )
        {
            bulletName = EnemyProperties.GetBulletName (enemyType);
        }
    }

    public void SetSlowSpeed()
    {
        if ( !isSlow )
        {
            speed = speed / 2;
            isSlow = true;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void DisplaceZPosition()
    {
        float dp = EnemyController.GetDisplace ();
        sprite.sortingOrder = linePosition;
        creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y, creepTransform.position.z + dp);

    }

    public void PlayAffect()
    {
        suppressionWind.Play ();
        defenceAffect.Play ();
    }

    public void MoveUp()
    {
        ec.RemoveCreepFromEnemyList (this);
        linePosition -= 1;
        sprite.sortingOrder = linePosition;
        creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y + Constants.CELL_HEIGHT, creepTransform.position.z);
        ec.AddCreepToEnemyList (this);
        GetMainTower ();

    }

    public void MoveDown()
    {
        ec.RemoveCreepFromEnemyList (this);
        linePosition += 1;
        sprite.sortingOrder = linePosition;
        creepTransform.position = new Vector3 (creepTransform.position.x, creepTransform.position.y - Constants.CELL_HEIGHT, creepTransform.position.z);
        ec.AddCreepToEnemyList (this);
        GetMainTower ();
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

    public void CalcDamage( float damage )
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

    public void GetMainTower()
    {
        mainTargetTower = ec.GetMainTargetTower (this);
        targetTower = mainTargetTower;
        print ("Update Main Tower");
    }

    public void GetClosestTower()
    {
        targetTower = ec.GetTargetTower (this);
        print ("Update Tower");
    }

    public void GetClosestTowerAfterHit( Tower tower )
    {
        if ( tower == targetTower )
            return;
        targetTower = ec.GetTargetTower (this);
        print ("Update TowerAfterHit");
    }

    public void CheckCollision()
    {
        // stops fight anim
        if ( !isInFight && creepAnimation.IsFightAnimationIsOn () )
            creepAnimation.StopFightAnimation ();

        float dist = Vector2.Distance (targetTower.towerTransform.position, creepTransform.position);

        if ( targetTower != mainTargetTower &&
            targetTower.towerTransform.position.x > creepTransform.position.x &&
            dist > Constants.CELL_WIDTH * attackRange )
        {
            targetTower = mainTargetTower;
            print ("Change Tower");
        }

        if ( dist < Constants.CELL_WIDTH * attackRange )
        {
            // test
            // time = Time.time - time;
            // print ("time of " + enemyType + " " + time);
            // MakeDeath ();
            // test
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


    private void Attack( float damage )
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

    public void Fire( float damage )
    {
        if ( targetTower )
        {
            if ( isRanger )
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
    }

    private IEnumerator FireAfterAnimation( float damage )
    {
        yield return new WaitForSeconds (Constants.ANIM_ATTACK_TIME);
        Fire (damage);
    }

    private void SetBullet()
    {
        if ( bulletName != null )
            bulletPref = GameAssets.instance.GetAssetByString (bulletName);
        impactPref = GameAssets.instance.GetAssetByString (Constants.BLOOD_IMPACT);
        deathPref = GameAssets.instance.GetAssetByString (Constants.CREEP_DEATH);
    }
}
