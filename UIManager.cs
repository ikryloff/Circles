using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    TimeManager timeManager;
    TouchController touchController;
    private bool magicPanelIsOn;
    Spells spells;
    Button menuButton;
    Button speedButton;
    Button spellsButton;
    
    VisualElement messageScreen;
    VisualElement manaBar;
    VisualElement defenceBar;
    VisualElement header;
    Label messageLabel;
    Label manaValue;
    Label defenceValue;
    Label spellsNumber;
    Label xPoints;
    [SerializeField]
    private UIDocument uiGame;
    [SerializeField]
    private UIDocument uiMenu;
    [SerializeField]
    private UIDocument uiSpellsPanel;

    public StyleSheet unityStyleSheet;
    private UISpellsManager uISpellsManager;
    private UIMenuManager uIMenuManager;
    private UIInfoPanel uIInfoPanel;

    private int defencePoints;
    private int manaPoints;
    private int xpPoints;

    private void OnEnable()
    {
        uISpellsManager = GetComponent<UISpellsManager> ();
        uIMenuManager = GetComponent<UIMenuManager> ();
        uIInfoPanel = GetComponent<UIInfoPanel> ();

        var rootGameUI = uiGame.rootVisualElement;

        var rootSpellsUI = uiSpellsPanel.rootVisualElement;
        uISpellsManager.SetRootAndInit (rootSpellsUI);
        uIInfoPanel.SetRootAndInit (rootSpellsUI);

        var rootMenuUI = uiMenu.rootVisualElement;
        uIMenuManager.SetRootAndInit (rootMenuUI);

        menuButton = rootGameUI.Query<Button> ("menu");
        speedButton = rootGameUI.Query<Button> ("speed");
        spellsButton = rootGameUI.Query<Button> ("spells-button");
        

        messageScreen = rootGameUI.Query<VisualElement> ("message-screen");
        defenceBar = rootGameUI.Query<VisualElement> ("defence-bar-line");
        manaBar = rootGameUI.Query<VisualElement> ("mana-bar-line");
        header = rootGameUI.Query<VisualElement> ("header");

        messageLabel = rootGameUI.Query<Label> ("message");
        manaValue = rootGameUI.Query<Label> ("mana-bar-value");
        defenceValue = rootGameUI.Query<Label> ("defence-bar-value");
        spellsNumber = rootGameUI.Query<Label> ("spells-number");
        xPoints = rootGameUI.Query<Label> ("exp-number");
    }


    private void Start()
    {
        touchController = ObjectsHolder.Instance.touchController;
        spells = ObjectsHolder.Instance.spells;
        timeManager = FindObjectOfType<TimeManager> ();
        speedButton.clicked += SpeedGame;
        spellsButton.clicked += ToggleSchoolList;
        
        menuButton.clicked += OpenMenu;
        magicPanelIsOn = true;
        ToggleSchoolList ();
        CleanMessage ();
        PrintSpellsQuantity ();
    }

    public void OpenMenu()
    {
        StopSpelling ();
        uIMenuManager.OpenMenu ();
    }   

    public void SpeedGame()
    {
        timeManager.TurnTime ();
    }

    public void Test()
    {
        SetMessage ("Test");
    }

    public void SetMessage( string mess )
    {
        messageLabel.text = mess;
    }

    public void CleanMessage()
    {
        messageLabel.text = "";
    }

    public void PrintSpellsQuantity()
    {
        spellsNumber.text = PlayerStats.GetPlayerSpellsQuantity ().ToString ();
    }

    public void ToggleSchoolList()
    {
        if ( magicPanelIsOn ) // if we need to close
        {
            StartSpelling ();
            uISpellsManager.HideMagicPanel ();
            spellsButton.style.display = DisplayStyle.Flex;
            messageScreen.style.display = DisplayStyle.Flex;
            header.style.display = DisplayStyle.Flex;
            magicPanelIsOn = false;
        }
        else
        {
            StopSpelling ();
            uISpellsManager.ShowMagicPanel ();
            spellsButton.style.display = DisplayStyle.None;
            messageScreen.style.display = DisplayStyle.None;
            header.style.display = DisplayStyle.None;
            magicPanelIsOn = true;
            uISpellsManager.TurnTab (0);
            uISpellsManager.UpdateSpellBoard ();
        }
    }

    public void SetManaValue( float value, float points )
    {
        manaBar.style.width = Length.Percent (value);
        manaValue.text = Mathf.FloorToInt(points).ToString ();
    }

    public void SetDefenceValue( float value, float points )
    {
        defenceBar.style.width = Length.Percent (value);
        defenceValue.text = points.ToString ();        
    }

    public void SetXPoints( int value )
    {
        xPoints.text = value.ToString ();
    }

    private void StopSpelling()
    {
        touchController.SetPauseState (true);
        timeManager.PauseGameOn ();
    }
    public void StartSpelling()
    {
        touchController.SetPauseState (false);
        timeManager.PauseGameOff ();
    }

   

}
