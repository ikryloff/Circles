using System.Collections;
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

    public static float fontKoef;

    IMGUIContainer prepareIcon;

    VisualElement messageScreen;
    VisualElement manaBar;
    VisualElement defenceBar;
    VisualElement prepareBar;
    VisualElement gamePanel;
    Label messageLabel;
    Label manaValue;
    Label defenceValue;
    Label spellsText;
    Label spellsNumber;
    Label xPText;
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

        prepareIcon = rootGameUI.Query<IMGUIContainer> ("prepare-bar-icon");

        messageScreen = rootGameUI.Query<VisualElement> ("message-screen");
        defenceBar = rootGameUI.Query<VisualElement> ("defence-bar-line");
        manaBar = rootGameUI.Query<VisualElement> ("mana-bar-line");
        prepareBar = rootGameUI.Query<VisualElement> ("prepare-bar-line");
        gamePanel = rootGameUI.Query<VisualElement> ("game-panel");

        //Labels
        messageLabel = rootGameUI.Query<Label> ("message");
        manaValue = rootGameUI.Query<Label> ("mana-bar-value");
        defenceValue = rootGameUI.Query<Label> ("defence-bar-value");
        spellsText = rootGameUI.Query<Label> ("spells-txt");
        FontUtilities.SetFontSize42 (spellsText);
        spellsNumber = rootGameUI.Query<Label> ("spells-number");
        FontUtilities.SetFontSize42 (spellsNumber);
        xPText = rootGameUI.Query<Label> ("exp-text");
        FontUtilities.SetFontSize42 (xPText);
        xPoints = rootGameUI.Query<Label> ("exp-number");
        FontUtilities.SetFontSize42 (xPoints);
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
        SetPrepareValue (100);
        SetDefaultPrepareIcon ();
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


    public void SetMessage( string mess )
    {
        StopCoroutine (Message (mess));
        StartCoroutine (Message (mess));
    }

    IEnumerator Message( string mess )
    {
        float messTime = 2.5f;
        while ( messTime > 0 )
        {
            if ( !messageLabel.text.Equals (mess) )
                messageLabel.text = mess;
            messTime -= Time.deltaTime;
            yield return null;

        }
        CleanMessage ();
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
            gamePanel.style.display = DisplayStyle.Flex;
            magicPanelIsOn = false;
        }
        else
        {
            StopSpelling ();
            uISpellsManager.ShowMagicPanel ();
            spellsButton.style.display = DisplayStyle.None;
            messageScreen.style.display = DisplayStyle.None;
            gamePanel.style.display = DisplayStyle.None;
            magicPanelIsOn = true;
            uISpellsManager.TurnTab (0);
            uISpellsManager.UpdateSpellBoard ();
        }
    }

    public void SetPrepareValue( float value )
    {
        if ( value >= 100 ) value = 100;
        prepareBar.style.height = Length.Percent (value);
    }

    public void SetManaValue( float value, float points )
    {
        manaBar.style.height = Length.Percent (value);
        manaValue.text = Mathf.FloorToInt (points).ToString ();
    }

    public void SetDefenceValue( float value, float points )
    {
        defenceBar.style.height = Length.Percent (value);
        defenceValue.text = points.ToString ();
    }

    public void SetXPoints( int value )
    {
        xPoints.text = value.ToString ();
    }

    public void StopSpelling()
    {
        touchController.SetStopTouching (true);
        timeManager.PauseGameOn ();
    }
    public void StartSpelling()
    {
        timeManager.PauseGameOff ();
        touchController.SetStopTouching (false);
    }


    public void SetPrepareIcon( int ID )
    {
        prepareIcon.style.backgroundImage = GameAssets.instance.GetActiveIconByID (ID);
    }

    public void SetDefaultPrepareIcon()
    {
        prepareIcon.style.backgroundImage = GameAssets.instance.GetEmptyIcon ();
    }

}
