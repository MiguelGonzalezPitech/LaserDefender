using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]
    float movSpeed = 10f;

    [SerializeField]
    float padding = 1f;

    [SerializeField]
    float health = 200;

    [SerializeField]
    AudioClip deathSFX;

    [SerializeField]
    [Range(0, 1)] float deathVolume;

    [Header("Laser")]
    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    float laserSpeed = 10f;

    [SerializeField]
    float timeBetweenShots = 0.10f;

    [SerializeField]
    AudioClip laserSFX;

    [SerializeField]
    [Range(0, 1)] float laserVolume;



    Coroutine fireCoroutine;


    float xMin;
    float xMax;
    float yMin;
    float yMax;
    // Start is called before the first frame update
    void Start()
    {
        SetupBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireCoroutine());

        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireCoroutine);
        }

    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                laserPrefab,
                transform.position,
                Quaternion.identity
                ) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.gameObject.transform.position, laserVolume);

            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }


    void SetupBoundaries()
    {
        Camera cam = Camera.main;
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    public float GetPlayerHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        DamageDealer damageDealer = collider2d.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathVolume);
        FindObjectOfType<Level>().LoadGameOver();
    }
}
