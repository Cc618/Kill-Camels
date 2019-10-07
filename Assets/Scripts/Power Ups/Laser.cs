using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Range(0, 200)]
    public float speed;

    [Range(0, 20)]
    public float maxDistanceToPlayer;

    public GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        UpdateVelocity();
    }

    void FixedUpdate()
    {
        // Check limits
        float delta = PlayerMovements.instance.transform.position.x - transform.position.x;
        if (delta >= maxDistanceToPlayer || delta <= -maxDistanceToPlayer)
        {
            Kill();
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            c.gameObject.GetComponent<Camel>().Hurt();
        else if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerMovements.instance.Kill();
            Kill();

            return;
        }

        // Test hits
        --remainingHits;
        if (remainingHits == 0)
            Kill();

        // Change direction
        speed = -speed;
        UpdateVelocity();
    }

    void UpdateVelocity()
    {
        body.velocity = new Vector2(
            speed,
            0
        );
    }

    void Kill()
    {
        CameraFollow.instance.Shake(.4f);

        AudioManager.Play("bomb");

        Instantiate(particles, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    int remainingHits = 4;

    Rigidbody2D body;
}
