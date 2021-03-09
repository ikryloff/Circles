using UnityEngine;
using UnityEngine.UIElements;

public class UIInfoPanel : MonoBehaviour
{
    UIManager uIManager;

    private VisualElement root;

    private Label defence;
    private Label mana;
    private Label exp;
    private Label manaPPS;
    private Label manaPR;
    private Label possibleSpells;

    private Label defenceValue;
    private Label manaValue;
    private Label expValue;
    private Label manaPPSValue;
    private Label manaPRValue;
    private Label possibleSpellsValue;

    private Label known;
    private Label elem;
    private Label nature;
    private Label demon;
    private Label necro;
    private Label defencive;

    private Label knownValue;
    private Label elemValue;
    private Label natureValue;
    private Label demonValue;
    private Label necroValue;
    private Label defenciveValue;

    private Spells spells;

   
    private void Start()
    {
        spells = ObjectsHolder.Instance.spells;
        SetInfoPanelText ();
    }

    public void SetRootAndInit( VisualElement _root )
    {
        root = _root;
        Init ();
    }

    private void Init()
    {
        uIManager = GetComponent<UIManager> ();

        defence = root.Query<Label> ("player-defence");
        mana = root.Query<Label> ("player-mana");
        exp = root.Query<Label> ("player-exp");
        manaPPS = root.Query<Label> ("player-mana-pps");
        manaPR = root.Query<Label> ("player-mana-return");
        possibleSpells = root.Query<Label> ("player-possible");

        defenceValue = root.Query<Label> ("player-defence-value");
        manaValue = root.Query<Label> ("player-mana-value");
        expValue = root.Query<Label> ("player-exp-value");
        manaPPSValue = root.Query<Label> ("player-mana-pps-value");
        manaPRValue = root.Query<Label> ("player-mana-return-value");
        possibleSpellsValue = root.Query<Label> ("player-possible-value");

        known = root.Query<Label> ("player-spells");
        elem = root.Query<Label> ("player-spells-elem");
        nature = root.Query<Label> ("player-spells-nature");
        demon = root.Query<Label> ("player-spells-demon");
        necro = root.Query<Label> ("player-spells-necro");
        defencive = root.Query<Label> ("player-spells-def");

        knownValue = root.Query<Label> ("player-spells-value");
        elemValue = root.Query<Label> ("player-spells-elem-value");
        natureValue = root.Query<Label> ("player-spells-nature-value");
        demonValue = root.Query<Label> ("player-spells-demon-value");
        necroValue = root.Query<Label> ("player-spells-necro-value");
        defenciveValue = root.Query<Label> ("player-spells-def-value");        

    }

    public void SetInfoPanelText()
    {
        defence.text = Localization.GetString ("defence");
        mana.text = Localization.GetString ("mana");
        exp.text = Localization.GetString ("exp");
        manaPPS.text = Localization.GetString ("manaPPS");
        manaPR.text = Localization.GetString ("manaPR");
        possibleSpells.text = Localization.GetString ("possibleSpells");

        defenceValue.text = PlayerStats.GetPlayerDP ().ToString ();
        manaValue.text = PlayerStats.GetPlayerMP ().ToString ();
        expValue.text = PlayerStats.GetPlayerXP ().ToString ();
        manaPPSValue.text = PlayerStats.GetPlayerMPPS ().ToString ();
        manaPRValue.text = (PlayerStats.GetPlayerManaReturn () * 100).ToString ();

        known.text = Localization.GetString ("known");
        elem.text = Localization.GetString ("elemental_stat");
        nature.text = Localization.GetString ("natural_stat");
        demon.text = Localization.GetString ("demon_stat");
        necro.text = Localization.GetString ("necro_stat");
        defencive.text = Localization.GetString ("defencive_stat");

        knownValue.text = PlayerStats.GetPlayerSpellsQuantity ().ToString ();
        elemValue.text = spells.GetSchoolLearnedSpells(spells.GetElementalListByIndex(0), spells.GetElementalListByIndex (1)).ToString();
        natureValue.text = spells.GetSchoolLearnedSpells (spells.GetNatureListByIndex (0), spells.GetNatureListByIndex (1)).ToString ();
        demonValue.text = spells.GetSchoolLearnedSpells (spells.GetDemonologyListByIndex (0), spells.GetDemonologyListByIndex (1)).ToString ();
        necroValue.text = spells.GetSchoolLearnedSpells (spells.GetNecromancyListByIndex (0), spells.GetNecromancyListByIndex (1)).ToString (); 
        defenciveValue.text = spells.GetSchoolLearnedSpells (spells.GetDefenciveListByIndex (0), spells.GetDefenciveListByIndex (1)).ToString ();
    }
}
