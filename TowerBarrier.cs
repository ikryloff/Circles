using UnityEngine;

public class TowerBarrier : Tower
{

    private HealthBar healthBar;
    public float startHp;
    private SpriteRenderer sprite;
    private TowerAnimation towerAnimation;
    private GameObject healthBarGO;

    public string deathName;
    public string impactName;
    public GameObject impactPref;
    public GameObject deathPref;

    private new void Awake()
    {
        base.Awake ();
        healthBar = GetComponentInChildren<HealthBar> ();
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
        startHp = hitPoints;
        sprite.sortingOrder = LinePosition;
        healthBarGO.SetActive (false);
        SetPrefs ();

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

    public override void CalcDamage( float damage)
    {
        if ( !healthBarGO.activeSelf )
            healthBarGO.SetActive (true);
        hitPoints -= damage;
        hp_norm = hitPoints / startHp;
        healthBar.SetHBSize (hp_norm);
        towerAnimation.HitAnimation ();
        MakeImpact ();
        CheckTower ();
    }

    private void MakeImpact()
    {
        Instantiate (impactPref, towerTransform.position, Quaternion.identity);
    }

    private void MakeDeath()
    {
        Instantiate (deathPref, towerTransform.position, Quaternion.identity);
    }
    private void SetPrefs()
    {      
        if ( deathName != "" )
            deathPref = GameAssets.instance.GetAssetByString (deathName);
        if ( impactName != "" )
            impactPref = GameAssets.instance.GetAssetByString (impactName);

    }
}
