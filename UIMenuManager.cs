using UnityEngine;
using UnityEngine.UIElements;

public class UIMenuManager : MonoBehaviour
{
    private bool isOptionsOrHelpOpened;

    private UIManager uIManager;
    private VisualElement root;
    private VisualElement options;
    private VisualElement menuPanel;
    private VisualElement helpPanel;

    private Button quit;
    private Button back;
    private Button optionsButton;
    private Button helpButton;
    private Button optionsEngButton;
    private Button optionsRusButton;


    public void SetRootAndInit( VisualElement _root )
    {
        root = _root;
        Init ();
    }

    private void Init()
    {
        uIManager = GetComponent<UIManager> ();

        options = root.Query<VisualElement> ("options-panel");
        helpPanel = root.Query<VisualElement> ("help-panel");
        menuPanel = root.Query<VisualElement> ("menu-panel");

        quit = root.Query<Button> ("quit");
        back = root.Query<Button> ("back");
        optionsButton = root.Query<Button> ("options");
        helpButton = root.Query<Button> ("help");
        optionsEngButton = root.Query<Button> ("lang-eng");
        optionsRusButton = root.Query<Button> ("lang-rus");

        back.clicked += ReturnMenu;
        optionsButton.clicked += OpenOptions;
        helpButton.clicked += OpenHelp;
        quit.clicked += QuitGame;
        optionsEngButton.clicked += SetEnglishLang;
        optionsRusButton.clicked += SetRussianLang;
    }

    private void Start()
    {
        DontDisplayMenu ();
    }

    public void QuitGame()
    {
        Application.Quit ();
        print ("Quit");
    }

    public void ReturnMenu()
    {
        if ( isOptionsOrHelpOpened )
        {
            CloseOptions ();
        }
        else
        {
            root.style.display = DisplayStyle.None;
            uIManager.StartSpelling ();
        }
    }

    public void OpenMenu()
    {
        root.style.display = DisplayStyle.Flex;
        menuPanel.style.display = DisplayStyle.Flex;
        options.style.display = DisplayStyle.None;
        CloseHelp ();
    }

    public void DontDisplayMenu()
    {
        root.style.display = DisplayStyle.None;
    }

    public void OpenOptions()
    {
        options.style.display = DisplayStyle.Flex;
        menuPanel.style.display = DisplayStyle.None;
        helpPanel.style.display = DisplayStyle.None;
        isOptionsOrHelpOpened = true;
    }

    public void CloseOptions()
    {
        options.style.display = DisplayStyle.None;
        helpPanel.style.display = DisplayStyle.None;
        menuPanel.style.display = DisplayStyle.Flex;
        isOptionsOrHelpOpened = false;
    }

    public void OpenHelp()
    {
        helpPanel.style.display = DisplayStyle.Flex;
        menuPanel.style.display = DisplayStyle.None;
        options.style.display = DisplayStyle.None;
        isOptionsOrHelpOpened = true;
    }

    public void CloseHelp()
    {
        helpPanel.style.display = DisplayStyle.None;
        options.style.display = DisplayStyle.None;
        menuPanel.style.display = DisplayStyle.Flex;
        isOptionsOrHelpOpened = false;
    }

    private void SetEnglishLang()
    {
        PlayerStats.SetPlayerLanguage (0);
        optionsEngButton.style.backgroundColor = Color.green;
        optionsRusButton.style.backgroundColor = Color.grey;
    }

    private void SetRussianLang()
    {
        PlayerStats.SetPlayerLanguage (1);
        optionsRusButton.style.backgroundColor = Color.green;
        optionsEngButton.style.backgroundColor = Color.grey;
    }

}
