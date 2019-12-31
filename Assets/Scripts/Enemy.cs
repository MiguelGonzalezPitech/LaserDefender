using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float health = 100;


    [SerializeField]
    float shotCounter;

    [SerializeField]
    float minTimeBetweenShots = 0.2f;


    [SerializeField]
    float maxTimeBetweenShots = 3.0f;


    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject deathVFX;

    [SerializeField]
    float durationOfExplosion;


    [SerializeField]
    float laserSpeed = 10f;


    [SerializeField]
    AudioClip deathSFX;


    [SerializeField]
    AudioClip fireSFX;


    [SerializeField]
    int scoreValue = 50;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);

        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(
                        laserPrefab,
                        transform.position,
                        Quaternion.identity
                        ) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        AudioSource.PlayClipAtPoint(fireSFX, Camera.main.gameObject.transform.position);
    }



    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        DamageDealer damageDealer = collider2d.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessDeath(damageDealer);
    }
    private void ProcessDeath(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        FindObjectOfType<GameSession>().AddScore(scoreValue);
        Destroy(gameObject);
        GameObject explosion = Instantiate(
            deathVFX,
            transform.position,
            transform.rotation);
        Destroy(explosion, durationOfExplosion);

        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.gameObject.transform.position);
    }
}
