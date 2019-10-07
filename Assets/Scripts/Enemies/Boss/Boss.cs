using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss : MonoBehaviour
{
    public static Boss instance;

    public GameObject missile;

    [Range(0, 30)]
    public float flyForce;

    [Range(0, 10)]
    public float speed;

    [Range(.1f, 4)]
    public float manyMissilesDelay;

    [Range(1, 10)]
    public float simpleMissilesDelay;

    public GameObject focusUp, focusMid, focusDown;

    [HideInInspector]
    public float minY;

    [HideInInspector]
    public bool laserOn = false;
    public bool missileOn = false;

    void Awake()
    {
        instance = this;

        minY = transform.position.y;

        body = GetComponent<Rigidbody2D>();
        laserEntity = transform.GetChild(0).gameObject;
        laser = laserEntity.transform.GetChild(0).GetComponent<LaserShooter>();

        laserEntity.SetActive(false);
    }

    void FixedUpdate()
    {
        // Flap
        if (transform.position.y <= minY)
            Flap();
    }   

    void OnTriggerEnter2D()
    {
        PlayerMovements.instance.Kill();
    }

    IEnumerator LaunchMissiles(int number, float delay)
    {
        missileOn = true;

        // Animation time
        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < number; ++i)
        {
            Instantiate(missile, transform.GetChild(2).position, Quaternion.identity);

            yield return new WaitForSeconds(delay);
        }

        missileOn = false;
    }

    void Flap()
    {
        // Only normal flap
        if (body.velocity.y <= 0)
            AudioManager.Play("boss_flap");

        body.velocity = new Vector2(speed, flyForce);
    }

    Rigidbody2D body;
    LaserShooter laser;
    GameObject laserEntity;
}

// Attacks
public partial class Boss : MonoBehaviour
{
    public void ToggleLaser()
    {
        laserOn = !laserOn;

        laserEntity.SetActive(!laserEntity.activeSelf);
        laser.laserActive = !laser.laserActive;
    }

    // Launches multiple missiles quickly
    public void AttackManyMissiles()
    {
        StartCoroutine(
            LaunchMissiles(4, manyMissilesDelay)
        );
    }

    // Launches multiple missiles slowly
    public void AttackSimpleMissiles()
    {
        StartCoroutine(
            LaunchMissiles(4, simpleMissilesDelay)
        );
    }
}