using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour {

    // Use this for initialization

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    [Header("Player")]
    [SerializeField] private float speed = 15f;
    [SerializeField] private float padding = 1f;
    [SerializeField] private int health = 100;
    [SerializeField] AudioClip PlayerLaserSound;
    [SerializeField] AudioClip PlayerDeathSound;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float LaserSpeed = 20f;
    [SerializeField] float ProjectileFiringPeriod = 0.1f;

    Coroutine mycoroutine;
    IEnumerator ch;

    void Start ()
    {
        SetUpMoveBoundaries();
	}

    // Update is called once per frame
    void Update ()
    {
        Move();
        Fire();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
        if (damageDealer==null)
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
            Die();
        }
    }

    public int getHealth()
    {
        return Mathf.Max(health, 0);
    }

    private void Die()
    {
        FindObjectOfType<Level>().GameOverScreen();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(PlayerDeathSound, Camera.main.transform.position, 1f);
    }
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            ch = FireContinuously();
            mycoroutine = StartCoroutine(ch);
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(mycoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(PlayerLaserSound, laser.transform.position, 0.5f);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, LaserSpeed);
            yield return new WaitForSeconds(ProjectileFiringPeriod);
        }
    }

    private void Move()
    {
        var myVarX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        var myVarY = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        var newXpos = Mathf.Clamp(transform.position.x + myVarX, minX, maxX);
        var newYpos = Mathf.Clamp(transform.position.y + myVarY, minY, maxY);

        transform.position = new Vector2(newXpos, newYpos);
        
        //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, Input.GetAxis("Vertical") * Time.deltaTime * speed, 0);
    }

    private void SetUpMoveBoundaries()
    {
        Camera myCamera = Camera.main;

        minX = myCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = myCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        minY = myCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = myCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
