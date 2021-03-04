using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer CellRenderer;
    public int CellNumber;
    public int LineNumber;
    public bool IsLoaded;
    public bool IsUsed;
    public bool IsEngaged;
    private CastManager castManager;
    public Sprite spellSprite;
    public Sprite cellSprite;

    private void Awake()
    {
        CellRenderer = GetComponent<SpriteRenderer> ();
        castManager = FindObjectOfType<CastManager> ();

    }

    // Start is called before the first frame update
    void Start()
    {
        spellSprite = ObjectsHolder.Instance.spellSprite;
        cellSprite = ObjectsHolder.Instance.cellSprite;
        GameEvents.current.OnCastOver += CountCell;
        GameEvents.current.OnCastReset += ReloadCell;

    }

    public void CountCell()
    {
        if ( IsLoaded ) CastManager.CellsCount += 1;
    }

    public void ReloadCell()
    {
        if ( CellRenderer )
        {
            CellRenderer.sprite = cellSprite;
            CellRenderer.sortingOrder = 0;
        }
        IsLoaded = false;
    }

    public void LoadCell()
    {
        if ( CellRenderer )
        {
            CellRenderer.sprite = spellSprite;
            CellRenderer.sortingOrder = 20;
        }
        IsLoaded = true;
        castManager.CastLine.Add (this);
    }


}
