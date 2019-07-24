using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    private WaveConfig waveConfig;
    private int waypointIndex = 0;
    private List<Transform> waypoints;

    // Use this for initialization
    void Start ()
    {
        //Debug.Log("inside start: " + Time.time);
        waypoints = waveConfig.getWayPoints();
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveEnemy();
    }

    public void setScriptableObjectOfEnemy(WaveConfig waveConfigRecieved) // waveConfigRecieved is our scriptable object for the enemy
    {
        //Debug.Log("inside setter: " + Time.time);
        this.waveConfig = waveConfigRecieved;
    }

    private void MoveEnemy()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            //Debug.Log("index: " + waypointIndex);
            var targetPos = waypoints[waypointIndex].transform.position;
            var movementThisframe = waveConfig.getmoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, movementThisframe);

            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
