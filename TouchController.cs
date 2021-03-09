using System.Collections;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private CastManager castManager;
    private ObjectsHolder objects;

    private bool isSkillBoardOpened;


    private void Awake()
    {
        castManager = FindObjectOfType<CastManager> ();
        objects = FindObjectOfType<ObjectsHolder> ();
    }
    

    private void Update()
    {
        if ( !IsSkillBoardOpened() )
        {
            if ( Input.touchCount > 0 )
            {
                if ( Input.GetTouch (0).phase == TouchPhase.Ended )
                {
                    GameEvents.current.CastOver ();
                    castManager.ReloadCast ();
                }
            }
        }
          

    }

    public bool IsSkillBoardOpened()
    {
        return isSkillBoardOpened;
    }

    public void SetPauseState( bool isOn )
    {
        isSkillBoardOpened = isOn;
    }




}
