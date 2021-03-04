using UnityEngine;
using UnityEngine.EventSystems;

public class CloseSkillBoardButton : MonoBehaviour, IPointerDownHandler
{
    private GameController gameController;

    private void Start()
    {
        gameController = ObjectsHolder.Instance.GameController;
    }

    public void OnPointerDown( PointerEventData eventData )
    {
    }
}
