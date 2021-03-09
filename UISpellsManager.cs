using UnityEngine;
using UnityEngine.UIElements;

public class UISpellsManager : MonoBehaviour
{
    private VisualElement root;
    private VisualElement spellsContainer;
    private VisualElement spellContainer;
    private VisualElement infoContainer;


    private int tabSelector;

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

    Label schoolLabel;

    private UIInfoPanel infoPanel;
    private Spells spells;


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
        infoPanel = GetComponent<UIInfoPanel> ();
        

        spellsContainer = root.Query<VisualElement> ("spells-container");
        spellContainer = root.Query<VisualElement> ("spell-container");
        infoContainer = root.Query<VisualElement> ("info-container");
        info = root.Query<Button> ("info-button");
        elem = root.Query<Button> ("elem-button");
        nature = root.Query<Button> ("nature-button");
        demonology = root.Query<Button> ("demon-button");
        necromancy = root.Query<Button> ("necro-button");
        defence = root.Query<Button> ("defence-button");

        tabs = new Button [] { info, elem, nature, demonology, necromancy, defence };

        schoolLabel = root.Query<Label> ("spells-pane-tab-name");

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

        
        info.clicked +=         delegate { TurnTab (0); };
        elem.clicked +=         delegate { TurnTab (1); };
        nature.clicked +=       delegate { TurnTab (2); };
        demonology.clicked +=   delegate { TurnTab (3); };
        necromancy.clicked +=   delegate { TurnTab (4); };
        defence.clicked +=      delegate { TurnTab (5); };
       

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
        int [] spList = new int [7];
        int [] caList = new int [7];
        switch ( tabSelector )
        {
            case 0:
                schoolLabel.text = Localization.GetString ("info");
                break;
            case 1:
                spList = spells.GetElementalListByIndex(0);
                caList = spells.GetElementalListByIndex (1);
                schoolLabel.text = Localization.GetString ("elemental");
                break;
            case 2:
                spList = spells.GetNatureListByIndex(0);
                caList = spells.GetNatureListByIndex (1);
                schoolLabel.text = Localization.GetString ("nature"); 
                break;
            case 3:
                spList = spells.GetDemonologyListByIndex (0);
                caList = spells.GetDemonologyListByIndex (1);
                schoolLabel.text = Localization.GetString ("demonology"); 
                break;
            case 4:
                spList = spells.GetNecromancyListByIndex (0);
                caList = spells.GetNecromancyListByIndex (1);
                schoolLabel.text = Localization.GetString ("necromancy");
                break;
            case 5:
                spList = spells.GetDefenciveListByIndex (0);
                caList = spells.GetDefenciveListByIndex (1);
                schoolLabel.text = Localization.GetString ("defencive");
                break;
            default:
                break;
        }

        UpdateSpellAndCallSchoolBoard (spList, caList);
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
}
