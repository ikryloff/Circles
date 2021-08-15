using UnityEngine;

public class GameController : MonoBehaviour
{

    private void Awake()
    {
        // for test
        PlayerStats.SetPlayerLevel (1);

    }
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}
