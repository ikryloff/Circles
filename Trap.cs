using UnityEngine;

public class Trap : Tower
{
    private SpriteRenderer sprite;
    private TowerAnimation towerAnimation;

    private string trapCode;

    public string deathName;
    public string impactName;
    public GameObject impactPref;
    public GameObject deathPref;

    private new void Awake()
    {
        base.Awake ();
        sprite = GetComponent<SpriteRenderer> ();
        towerAnimation = GetComponent<TowerAnimation> ();
    }

    public override void Start()
    {
        base.Start ();
        isAI = true;
        AddTower (this);
        sprite.sortingOrder = LinePosition - 1;
        SetPrefs ();

    }

    public void Update()
    {
        CheckCollision ();
    }


    private void CheckCollision()
    {
        if ( creepsInLine.Count == 0 )
            return;
        for ( int i = 0; i < creepsInLine.Count; i++ )
        {
            Creep creep = creepsInLine [i];
            Transform crTransform = creep.creepTransform;
            float dist = Mathf.Abs (transform.position.x - crTransform.position.x);
            if ( dist <= Constants.TRAP_DIST )
            {
                TrapTrigger (creep);
            }
        }
    }

    public void SetTrapCode( string code )
    {
        trapCode = code;
    }

    private void TrapTrigger( Creep creep )
    {
        if ( trapCode.Equals (Constants.TOWER_CODE_GO_AWAY) )
            creep.MoveUp ();
        else if ( trapCode.Equals (Constants.TOWER_CODE_COME_HERE) )
            creep.MoveDown ();
        else
            creep.CalcDamage (damage);
        TowerDeath ();
    }

    public override void TowerDeath()
    {
        MakeDeath ();
        RemoveTower (this);
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
