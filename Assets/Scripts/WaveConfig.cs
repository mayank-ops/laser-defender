using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy wave config")]
public class WaveConfig : ScriptableObject {

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenspawns = 0.5f;
    [SerializeField] float spawnRandomfactor = 0.3f;
    [SerializeField] int numberofEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject getenemyPrefab() { return enemyPrefab; }

    public List<Transform> getWayPoints()
    {
        var waveWavepoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWavepoints.Add(child);
        }
        return waveWavepoints;
    }

    public float gettimeBetweenspawns() { return timeBetweenspawns; }
    public float getspawnRandomfactor() { return spawnRandomfactor; }
    public int getnumberofEnemies() { return numberofEnemies; }
    public float getmoveSpeed() { return moveSpeed; }

}
