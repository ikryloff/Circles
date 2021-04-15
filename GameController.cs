using UnityEngine;

public class GameController : MonoBehaviour
{

    private void Awake()
    {
        // for test
        PlayerStats.SetPlayerLevel (2);

    }
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}
