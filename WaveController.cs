using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private EnemyController ec;
    private Wave [] waves;
    public WavesList wavesList;
    public Transform [] spawnPoints;

    private float timeBetweenWaves = 40f;
    private float timeBetweenCreeps = 10f;
    private float countdown = 2f;

    private int waveIndex = 0;

    private void Awake()
    {
        wavesList = GetComponent<WavesList>();
        waves = new Wave [3];
    }
    private void Start()
    {
       
        ec = ObjectsHolder.Instance.enemyController;
        spawnPoints = ec.spawns;
        waves = wavesList.GetWavesList (PlayerStats.GetPlayerLevel());
    }

    private void Update()
    {
        if ( waves.Length == 0 )
            return;
        if ( waveIndex == waves.Length )
            enabled = false;
        if ( countdown <= 0 )
        {
            print ("NextWave");
            StartCoroutine (SpawnWave ());
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves [waveIndex];
        print ("WI " + (waveIndex + 1));
        for ( int i = 0; i < wave.creeps.Length; i++ )
        {
            SpawnEnemy (wave.creeps [i]);
            countdown = timeBetweenWaves;
            yield return new WaitForSeconds (timeBetweenCreeps);
        }
        waveIndex++;

    }

    private void SpawnEnemy( GameObject enemy )
    {
        int pos = Random.Range (0, 7);
        GameObject enemyGO = Instantiate (enemy, spawnPoints [pos].position, Quaternion.identity) as GameObject;
        Creep newCreep = enemyGO.GetComponent<Creep> ();
        newCreep.SetLinePosition (pos + 1);
        ec.creeps.Add (newCreep);
        GameEvents.current.EnemyAppear ();
    }
}
