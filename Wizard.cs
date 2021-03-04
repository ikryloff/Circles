using UnityEngine;

public class Wizard : MonoBehaviour
{
    private UIManager uI;
    public float startHp;
    public float startMana;
    private float hp_norm;
    private float mana_norm;
    private float defencePoints;
    private float manaPoints;
    private float manaCicle;
    private float manaCicleRate;
    private float manaRecoverPoints;
    public float temp;

    void Start()
    {
        uI = ObjectsHolder.Instance.uIManager;
        temp = Time.time;
        defencePoints = 1000;
        manaPoints = PlayerStats.GetPlayerMP ();
        startHp = defencePoints;
        startMana = manaPoints;
        hp_norm = 1f;
        mana_norm = 1f;
        manaRecoverPoints = 2f;
        manaCicle = 5;
        manaCicleRate = 5;
        uI.SetHealthValue (hp_norm * 100, defencePoints);
        CalcMana ();
        PrintSpellsIDList ();
    }
    private void Update()
    {
        ManaRecoveryCicle ();
    }

    private void ManaRecoveryCicle()
    {
        if ( manaCicle <= 0 )
        {
            ManaRecover (manaRecoverPoints);
            manaCicle = manaCicleRate;
        }
        manaCicle -= Time.deltaTime;
    }

    public void ManaRecover( float mPoints )
    {
        manaPoints += mPoints * PlayerStats.GetPlayerManaBonus ();
        if ( manaPoints > startMana )
        {
            manaPoints = startMana;
        }
        CalcMana ();
    }

    public void CalcDamage( float damage )
    {
        defencePoints -= damage;
        hp_norm = defencePoints / startHp;
        uI.SetHealthValue (hp_norm * 100, defencePoints);
        CheckDefence ();
    }

    private void CheckDefence()
    {
        if ( defencePoints <= 0 )
        {
            defencePoints = 0;
            print ("GameOver");

            temp = Time.time - temp;
            Time.timeScale = 0;
            print (temp);
        }
    }

    public void ManaWaste( float mPoints )
    {
        manaPoints -= mPoints;
        CalcMana ();
    }

    public void CalcMana()
    {
        mana_norm = manaPoints / startMana;
        uI.SetManaValue (mana_norm * 100, manaPoints);

    }

    public float GetManapoints()
    {
        return manaPoints;
    }



    //For test
    public void PrintSpellsIDList()
    {
        for ( int i = 0; i < PlayerStats.GetPlayerSpellsIDList().Length; i++ )
        {
            print (PlayerStats.GetPlayerSpellsIDList () [i] + ", ");
        }
        print ("Quantity: " + PlayerStats.GetPlayerSpellsQuantity ());
    }
}
