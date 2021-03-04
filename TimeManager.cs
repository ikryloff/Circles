using UnityEngine;
using UnityEngine.UIElements;

public class TimeManager : MonoBehaviour
{
  
    public bool isFast;   

    public void TurnTime()
    {
        if ( isFast )
        {
            MinSpeed ();
            isFast = false;
        }
        else
        {
            MaxSpeed ();
            isFast = true;
        }
            
    }

    public void PauseGameOn()
    {
        Time.timeScale = 0;
    }

    public void PauseGameOff()
    {
        Time.timeScale = 1;
    }

    public void MaxSpeed()
    {
        Time.timeScale = 20;
    }

    public void MinSpeed()
    {
        Time.timeScale = 1;
    }

    

}
