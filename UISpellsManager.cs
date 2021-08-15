using System;
using UnityEngine;
using UnityEngine.UIElements;

public class UISpellsManager : MonoBehaviour
{
    

    private UIManager uIManager;

    private VisualElement root;
    private VisualElement spellsContainer;
    private VisualElement spellContainer;
    private VisualElement infoContainer;
    private VisualElement magicPanel;

    private int tabSelector;

    Button closePanelButton;

    Button info;
    Button nature;
    Button elem;
    Button demonology;
    Button necromancy;
    Button defence;

    Button [] tabs;
    Button [] spellsButtons;
    Button [] callsButtons;

    Button spell1;
    Button spell2;
    Button spell3;
    Button spell4;
    Button spell5;
    Button spell6;
    Button spell7;

    Button call1;
    Button call2;
    Button call3;
    Button call4;
    Button call5;
    Button call6;
    Button call7;

    Label itemName;
    Label spellDescription;

    IMGUIContainer schemaImg;

    private UIInfoPanel infoPanel;
    private Spells spells;

    private int [] spellsIDList = new int [7];
    private int [] callsIDList = new int [7];

    private bool isSpellPanelIsOpened;

    private void Start()
    {
        spells = ObjectsHolder.Instance.spells;
    }

    public void SetRootAndInit( VisualElement _root )
    {
        root = _root;
        Init ();
    }

    private void Init()
    {
        uIManager = GetComponent<UIManager> ();
        infoPanel = GetComponent<UIInfoPanel> ();


        spellsContainer = root.Query<VisualElement> ("spells-container");
        spellContainer = root.Query<VisualElement> ("spell-container");
        infoContainer = root.Query<VisualElement> ("info-container");
        magicPanel = root.Query<VisualElement> ("magic-panel");

        info = root.Query<Button> ("info-button");
        elem = root.Query<Button> ("elem-button");
        nature = root.Query<Button> ("nature-button");
        demonology = root.Query<Button> ("demon-button");
        necromancy = root.Query<Button> ("necro-button");
        defence = root.Query<Button> ("defence-button");
        closePanelButton = root.Query<Button> ("close-panel-button");

        tabs = new Button [] { info, elem, nature, demonology, necromancy, defence };

        itemName = root.Query<Label> ("spells-panel-tab-name");
        spellDescription = root.Query<Label> ("spell-desc-value");
        FontUtilities.SetFontSize42 (spellDescription);


        spell1 = root.Query<Button> ("spell-level1-but");
        spell2 = root.Query<Button> ("spell-level2-but");
        spell3 = root.Query<Button> ("spell-level3-but");
        spell4 = root.Query<Button> ("spell-level4-but");
        spell5 = root.Query<Button> ("spell-level5-but");
        spell6 = root.Query<Button> ("spell-level6-but");
        spell7 = root.Query<Button> ("spell-level7-but");
        spellsButtons = new Button [] { spell1, spell2, spell3, spell4, spell5, spell6, spell7 };

        call1 = root.Query<Button> ("call-level1-but");
        call2 = root.Query<Button> ("call-level2-but");
        call3 = root.Query<Button> ("call-level3-but");
        call4 = root.Query<Button> ("call-level4-but");
        call5 = root.Query<Button> ("call-level5-but");
        call6 = root.Query<Button> ("call-level6-but");
        call7 = root.Query<Button> ("call-level7-but");
        callsButtons = new Button [] { call1, call2, call3, call4, call5, call6, call7 };

        schemaImg = root.Query<IMGUIContainer> ("spell-schema-img");

        InitSpellButtons ();

        closePanelButton.clicked += CloseOrReturn;

        info.clicked += delegate { TurnTab (0); };
        elem.clicked += delegate { TurnTab (1); };
        nature.clicked += delegate { TurnTab (2); };
        demonology.clicked += delegate { TurnTab (3); };
        necromancy.clicked += delegate { TurnTab (4); };
        defence.clicked += delegate { TurnTab (5); };
    }

    private void InitSpellButtons()
    {
        spellsButtons [0].clicked += delegate { OpenSpellPanel (true, 0); };
        spellsButtons [1].clicked += delegate { OpenSpellPanel (true, 1); };
        spellsButtons [2].clicked += delegate { OpenSpellPanel (true, 2); };
        spellsButtons [3].clicked += delegate { OpenSpellPanel (true, 3); };
        spellsButtons [4].clicked += delegate { OpenSpellPanel (true, 4); };
        spellsButtons [5].clicked += delegate { OpenSpellPanel (true, 5); };
        spellsButtons [6].clicked += delegate { OpenSpellPanel (true, 6); };

        callsButtons [0].clicked += delegate { OpenSpellPanel (false, 0); };
        callsButtons [1].clicked += delegate { OpenSpellPanel (false, 1); };
        callsButtons [2].clicked += delegate { OpenSpellPanel (false, 2); };
        callsButtons [3].clicked += delegate { OpenSpellPanel (false, 3); };
        callsButtons [4].clicked += delegate { OpenSpellPanel (false, 4); };
        callsButtons [5].clicked += delegate { OpenSpellPanel (false, 5); };
        callsButtons [6].clicked += delegate { OpenSpellPanel (false, 6); };


    }

    private void OpenSpellPanel( bool isSpell, int spellNum )
    {
        SpellPanelOn ();
        Spell spell;
        if ( isSpell )
            spell = spells.GetSpellByID (spellsIDList [spellNum]);
        else
            spell = spells.GetSpellByID (callsIDList [spellNum]);
       
        itemName.text = Localization.GetString(spell.spellTag);
        schemaImg.style.backgroundImage = GameAssets.instance.GetSchemaByID (spell.spellID);
    }

  

    private void SpellPanelOn()
    {
        spellContainer.style.display = DisplayStyle.Flex;
        spellsContainer.style.display = DisplayStyle.None;
        infoContainer.style.display = DisplayStyle.None;
        isSpellPanelIsOpened = true;
        
    }

    private void SetButtonImages()
    {
        for ( int i = 0; i < spellsButtons.Length; i++ )
        {
            spellsButtons [i].style.backgroundImage = GameAssets.instance.GetActiveIconByID (spellsIDList [i]);
            callsButtons [i].style.backgroundImage = GameAssets.instance.GetActiveIconByID (callsIDList [i]);
        }
    }

    private void InfoPanelOn()
    {
        spellContainer.style.display = DisplayStyle.None;
        spellsContainer.style.display = DisplayStyle.None;
        infoContainer.style.display = DisplayStyle.Flex;
        infoPanel.SetInfoPanelText ();
    }

    private void SpellsPanelOn()
    {
        spellContainer.style.display = DisplayStyle.None;
        infoContainer.style.display = DisplayStyle.None;
        spellsContainer.style.display = DisplayStyle.Flex;
        isSpellPanelIsOpened = false;
        UpdateSpellBoard ();
        SetButtonImages ();
    }

    private void CloseOrReturn()
    {
        if ( isSpellPanelIsOpened )
        {
            SpellsPanelOn ();
        }
        else
        {
            uIManager.ToggleSchoolList ();
        }
    }

    public void UpdateSpellAndCallSchoolBoard( int [] spellList, int [] callList )
    {
        for ( int i = 0; i < spellList.Length; i++ )
        {
            int sp = spellList [i];
            int ca = callList [i];
            if ( PlayerStats.IsSpellInPlayerSpellsIDList (sp) )
            {
                spellsButtons [i].style.backgroundColor = Color.green;
            }
            else
            {
                spellsButtons [i].style.backgroundColor = Color.black;
            }
            if ( PlayerStats.IsSpellInPlayerSpellsIDList (ca) )
            {
                callsButtons [i].style.backgroundColor = Color.green;
            }
            else
            {
                callsButtons [i].style.backgroundColor = Color.black;
            }
        }

        
    }

    public void UpdateSpellBoard()
    {
        switch ( tabSelector )
        {
            case 0:
                itemName.text = Localization.GetString ("info");
                break;
            case 1:
                spellsIDList = spells.GetElementalListByIndex (0);
                callsIDList = spells.GetElementalListByIndex (1);
                itemName.text = Localization.GetString ("elemental");
                break;
            case 2:
                spellsIDList = spells.GetNatureListByIndex (0);
                callsIDList = spells.GetNatureListByIndex (1);
                itemName.text = Localization.GetString ("nature");
                break;
            case 3:
                spellsIDList = spells.GetDemonologyListByIndex (0);
                callsIDList = spells.GetDemonologyListByIndex (1);
                itemName.text = Localization.GetString ("demonology");
                break;
            case 4:
                spellsIDList = spells.GetNecromancyListByIndex (0);
                callsIDList = spells.GetNecromancyListByIndex (1);
                itemName.text = Localization.GetString ("necromancy");
                break;
            case 5:
                spellsIDList = spells.GetDefenciveListByIndex (0);
                callsIDList = spells.GetDefenciveListByIndex (1);
                itemName.text = Localization.GetString ("defencive");
                break;
            default:
                break;
        }

        SetButtonImages ();
    }



    public void TurnTab( int selector )
    {
        if ( selector == 0 )
            InfoPanelOn ();
        else
            SpellsPanelOn ();
        tabs [tabSelector].style.backgroundColor = Color.grey;
        tabSelector = selector;
        tabs [tabSelector].style.backgroundColor = Color.green;
        UpdateSpellBoard ();
    }

    public void HideMagicPanel()
    {
        magicPanel.style.display = DisplayStyle.None;
    }

    public void ShowMagicPanel()
    {
        magicPanel.style.display = DisplayStyle.Flex;
    }
}
