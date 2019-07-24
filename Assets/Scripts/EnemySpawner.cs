using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingwave = 0;
    [SerializeField] bool looping = false;

	// Use this for initialization
	IEnumerator Start ()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingwave ; waveIndex < waveConfigs.Count ; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int enemyCount=0;enemyCount<waveConfig.getnumberofEnemies();enemyCount++)
        {
            //Debug.Log("inside for: " + Time.time);
            GameObject enemy=Instantiate(waveConfig.getenemyPrefab(), waveConfig.getWayPoints()[0].position, Quaternion.identity);
            enemy.GetComponent<EnemyPathing>().setScriptableObjectOfEnemy(waveConfig);
            yield return new WaitForSeconds(waveConfig.gettimeBetweenspawns());
        }
    }
}
