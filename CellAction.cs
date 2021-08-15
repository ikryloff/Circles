using UnityEngine;
using UnityEngine.EventSystems;

public class CellAction : MonoBehaviour, IPointerEnterHandler
{
    private Cell cell;
    private CastManager castManager;
    private TouchController touchController;


    public void OnPointerEnter( PointerEventData eventData )
    {
        if ( !touchController.IsStopTouching () )
        {
            if ( !cell.IsLoaded )
            {
                cell.LoadCell ();
            }
        }
    }

    private void Awake()
    {
        cell = GetComponent<Cell> ();
        
    }

    private void Start()
    {
        touchController = ObjectsHolder.Instance.touchController;
    }
}
