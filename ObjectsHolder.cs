using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectsHolder : MonoBehaviour
{
    public Sprite spellSprite;
    public Sprite cellSprite;
    public static ObjectsHolder Instance { get; private set; }
    public Physics2DRaycaster Raycaster;
    public Camera mainCamera;
    public Spells spells;
    public Field field;
    public BuildingManager buildingManager;
    public EnemyController enemyController;
    public AttackManager attackManager;
    public Wizard wizard;
    public XPpoints xpPoints;
    public GameController GameController;
    public UIManager uIManager;
    public TouchController touchController;



    private void Awake()
    {
        if ( !Instance )
        {
            Instance = this;
            DontDestroyOnLoad (this);
        }
        else
            Destroy (this);

        mainCamera = Camera.main;
        Raycaster = mainCamera.GetComponent<Physics2DRaycaster> ();
        spells = FindObjectOfType<Spells> ();
        field = FindObjectOfType<Field> ();
        buildingManager = FindObjectOfType<BuildingManager> ();
        attackManager = FindObjectOfType<AttackManager> ();
        enemyController = FindObjectOfType<EnemyController> ();
        touchController = FindObjectOfType<TouchController>();
        wizard = FindObjectOfType<Wizard> ();
        xpPoints = wizard.GetComponent<XPpoints> ();
        GameController = FindObjectOfType<GameController> ();
        uIManager = FindObjectOfType<UIManager> ();
    }



}
