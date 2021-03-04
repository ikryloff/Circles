using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    TimeManager timeManager;
    TouchController touchController;
    private bool magicPanelIsOn;
    Spells spells;
    Button quitButton;
    Button speedButton;
    Button spellsButton;
    Button closePanelButton;
    VisualElement magicPanel;
    VisualElement messageScreen;
    VisualElement manaBar;
    VisualElement healthBar;
    Label messageLabel;
    Label manaValue;
    Label healthValue;
    Label spellsNumber;
    Label xPoints;
    public UIDocument uIDocument;
    public StyleSheet unityStyleSheet;
    private UISpellsManager uISpellsManager;

    private void OnEnable()
    {
        uISpellsManager = GetComponent<UISpellsManager> ();
        uIDocument = FindObjectOfType<UIDocument> ();
        var root = uIDocument.rootVisualElement;
        uISpellsManager.SetRootAndInit (root);
        quitButton = root.Query<Button> ("quit");
        speedButton = root.Query<Button> ("speed");
        spellsButton = root.Query<Button> ("spells-button");
        closePanelButton = root.Query<Button> ("close-panel-button");
        magicPanel = root.Query<VisualElement> ("magic-panel");
        messageScreen = root.Query<VisualElement> ("message-screen");
        healthBar = root.Query<VisualElement> ("health-bar-line");
        manaBar = root.Query<VisualElement> ("mana-bar-line");
        messageLabel = root.Query<Label> ("message");
        manaValue = root.Query<Label> ("mana-bar-value");
        healthValue = root.Query<Label> ("health-bar-value");
        spellsNumber = root.Query<Label> ("spells-number");
        xPoints = root.Query<Label> ("exp-number");
        quitButton.RegisterCallback<ClickEvent> (ev => QuitGame ());
    }


    private void Start()
    {
        touchController = ObjectsHolder.Instance.touchController;
        spells = ObjectsHolder.Instance.spells;
        timeManager = FindObjectOfType<TimeManager> ();
        speedButton.RegisterCallback<ClickEvent> (ev => SpeedGame ());
        spellsButton.RegisterCallback<ClickEvent> (ev => ToggleSchoolList ());
        closePanelButton.RegisterCallback<ClickEvent> (ev => ToggleSchoolList ());
        magicPanelIsOn = true;
        ToggleSchoolList ();
        CleanMessage ();
        PrintSpellsQuantity ();
    }

    public void QuitGame()
    {
        Application.Quit ();
    }

    public void SpeedGame()
    {
        timeManager.TurnTime ();
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
            timeManager.PauseGameOff ();
            touchController.SetSkillBoardState (false);
            magicPanel.style.display = DisplayStyle.None;
            spellsButton.style.display = DisplayStyle.Flex;
            messageScreen.style.display = DisplayStyle.Flex;
            magicPanelIsOn = false;
        }
        else
        {
            timeManager.PauseGameOn();
            touchController.SetSkillBoardState (true);
            magicPanel.style.display = DisplayStyle.Flex;
            spellsButton.style.display = DisplayStyle.None;
            messageScreen.style.display = DisplayStyle.None;
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

    public void SetHealthValue( float value, float points )
    {
        healthBar.style.width = Length.Percent (value);
        healthValue.text = points.ToString ();
    }

    public void SetXPoints( int value )
    {
        xPoints.text = value.ToString ();
    }
}
