using System.Collections;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private CastManager castManager;
    private ObjectsHolder objects;

    private bool isStopTouching;


    private void Awake()
    {
        castManager = FindObjectOfType<CastManager> ();
        objects = FindObjectOfType<ObjectsHolder> ();
    }

    
    private void Update()
    {
        if ( !IsStopTouching())
        {
            if ( Input.touchCount > 0 )
            {
                if ( Input.GetTouch (0).phase == TouchPhase.Ended )
                {
                    GameEvents.current.CastOver ();
                    castManager.MakeCast ();
                }
            }
        }
          

    }

    public bool IsStopTouching()
    {
        return isStopTouching;
    }

    public void SetStopTouching( bool isOn )
    {
        isStopTouching = isOn;
    }

   

}
