using UnityEngine;

public class WavesList : MonoBehaviour
{
    [SerializeField]
    private GameObject peasant, archer, spearman, horesman, swordsman, vigilante;

    private Wave [] waves;



    public Wave [] GetWavesList( int level )
    {
        waves = new Wave [3];
        for ( int i = 0; i < waves.Length; i++ )
        {
            waves [i] = new Wave ();
        }

        switch ( level )
        {
            case 1:
                waves [0].creeps = new GameObject [] { peasant, horesman, swordsman };
                waves [1].creeps = new GameObject [] { spearman, vigilante, archer };
                waves [2].creeps = new GameObject [] { peasant, spearman, archer };
                break;
            case 2:
                waves [0].creeps = new GameObject [] { peasant, peasant, peasant };
                waves [1].creeps = new GameObject [] { peasant, peasant, peasant };
                waves [2].creeps = new GameObject [] { peasant, spearman, peasant };
                break;
            default:
                waves = null;
                break;
        }


        return waves;
    }
}
