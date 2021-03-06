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
    Button [] spells;
    Button [] calls;

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


    private int [] natureSchoolSpellList = { 7, 8, 9, 10, 11, 12, 13 };
    private int [] natureSchoolCallllList = { 42, 43, 44, 45, 46, 47, 48 };
    private int [] elementalSchoolSpellList = { 0, 1, 2, 3, 4, 5, 6 };
    private int [] elementalSchoolCallList = { 35, 36, 37, 38, 39, 40, 41 };
    private int [] demonologySchoolSpellList = { 14, 15, 16, 17, 18, 19, 20 };
    private int [] demonologySchoolCallList = { 49, 50, 51, 52, 53, 54, 55 };
    private int [] necromancySchoolSpellList = { 21, 22, 23, 24, 25, 26, 27 };
    private int [] necromancySchoolCallList = { 56, 57, 58, 59, 60, 61, 62 };
    private int [] defenciveSchoolSpellList = { 28, 29, 30, 31, 32, 33, 34 };
    private int [] defenciveSchoolCallList = { 63, 64, 65, 66, 67, 68, 69 };


    public void SetRootAndInit( VisualElement _root )
    {
        root = _root;
        Init ();
    }

    private void Init()
    {
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
        spells = new Button [] { spell1, spell2, spell3, spell4, spell5, spell6, spell7 };

        call1 = root.Query<Button> ("call-level1-but");
        call2 = root.Query<Button> ("call-level2-but");
        call3 = root.Query<Button> ("call-level3-but");
        call4 = root.Query<Button> ("call-level4-but");
        call5 = root.Query<Button> ("call-level5-but");
        call6 = root.Query<Button> ("call-level6-but");
        call7 = root.Query<Button> ("call-level7-but");
        calls = new Button [] { call1, call2, call3, call4, call5, call6, call7 };

        
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
                spells [i].style.backgroundColor = Color.green;
            }
            else
            {
                spells [i].style.backgroundColor = Color.black;
            }
            if ( PlayerStats.IsSpellInPlayerSpellsIDList (ca) )
            {
                calls [i].style.backgroundColor = Color.green;
            }
            else
            {
                calls [i].style.backgroundColor = Color.black;
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
                spList = elementalSchoolSpellList;
                caList = elementalSchoolCallList;
                schoolLabel.text = Constants.ELEMENTAL;
                break;
            case 2:
                spList = natureSchoolSpellList;
                caList = natureSchoolCallllList;
                schoolLabel.text = Constants.NATURE;
                break;
            case 3:
                spList = demonologySchoolSpellList;
                caList = demonologySchoolCallList;
                schoolLabel.text = Constants.DEMONOLOGY;
                break;
            case 4:
                spList = necromancySchoolSpellList;
                caList = necromancySchoolCallList;
                schoolLabel.text = Constants.NECROMANCY;
                break;
            case 5:
                spList = defenciveSchoolSpellList;
                caList = defenciveSchoolCallList;
                schoolLabel.text = Constants.DEFENSIVE;
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
