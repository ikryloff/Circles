using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action OnCastOver;
    public event Action OnCastReset;
    public event Action OnEnemyAppear;
    public event Action OnTowerAppear;
    public void CastOver()
    {
        if ( OnCastOver != null )
        {
            OnCastOver ();
        }
    }

    public void CastReset()
    {
        if ( OnCastReset != null )
        {
            OnCastReset ();
        }
    }

    public void EnemyAppear()
    {
        if ( OnEnemyAppear != null )
        {
            OnEnemyAppear ();
        }
    }

    public void TowerAppear()
    {
        if ( OnTowerAppear != null )
        {
            OnTowerAppear ();
        }
    }
}
