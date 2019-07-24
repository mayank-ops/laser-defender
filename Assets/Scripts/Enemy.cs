using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Enemy stats")]
    [SerializeField] float health = 100f;
    [SerializeField] int enemyPoints = 50;
    [SerializeField] GameObject EnemyLaser;
    [SerializeField] float LaserSpeed = 20f;
    [SerializeField] GameObject enemyVFX;
    [SerializeField] AudioClip EnemyLaserAudio;
    [SerializeField] AudioClip EnemyDestroyAudio;

    [Header("Enemy laser stats")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;

    GameSession obj;
    private float gap = 0.8f;

    // Use this for initialization
    void Start ()
    {
        obj = FindObjectOfType<GameSession>();
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyLaser = Instantiate(EnemyLaser, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(EnemyLaserAudio, enemyLaser.transform.position, 0.5f);
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LaserSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(damageDealer==null)
        {
            return;
        }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.getDamage();
        damageDealer.hit();
        if (health <= 0)
        {
            var VFX = Instantiate(enemyVFX, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(EnemyDestroyAudio, Camera.main.transform.position, 0.2f);
            Destroy(VFX, 1f);
            obj.updateScore(enemyPoints);
            Destroy(gameObject);
        }
    }
}
