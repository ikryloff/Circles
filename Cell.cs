using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer CellRenderer;
    public int CellNumber;
    public int LineNumber;
    public bool IsLoaded;
    public bool IsUsed;
    public bool IsEngaged;
    [SerializeField]
    private int cellType; // 0 - common, 1 - untouchable 
    private CastManager castManager;
    public Sprite spellSprite;
    public Sprite cellSprite;
    public Sprite colorSprite;
    public Sprite untouchableSprite;

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
        colorSprite = ObjectsHolder.Instance.colorSprite;
        untouchableSprite = ObjectsHolder.Instance.untouchableSprite;
        GameEvents.current.OnCastOver += CountCell;
        GameEvents.current.OnCastReset += ReloadCell;

        Wizard.StopCasting = true;
        StartCoroutine (CastDelay ());

    }

    public void CountCell()
    {
        if ( IsLoaded ) CastManager.CellsCount += 1;
    }

    public void ReloadCell()
    {
        if ( CellRenderer)
        {
            if(cellType == 1)
                CellRenderer.sprite = untouchableSprite;
            else
                CellRenderer.sprite = cellSprite;

            CellRenderer.sortingOrder = 0;
        }
        IsLoaded = false;
    }

    public void LoadCell()
    {
        if ( Wizard.StopCasting )
            return;
        if ( CellRenderer )
        {
            CellRenderer.sprite = spellSprite;
            CellRenderer.sortingOrder = 20;
        }
        IsLoaded = true;
        castManager.CastLine.Add (this);
    }

    public void ColorCell()
    {
        if ( CellRenderer )
        {
            CellRenderer.sprite = colorSprite;
            CellRenderer.sortingOrder = 0;
        }
    }

    IEnumerator CastDelay()
    {
        yield return new WaitForSeconds (0.5f);
        Wizard.StopCasting = false;
    }

}
