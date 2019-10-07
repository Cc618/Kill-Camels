using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Must have the x limit as 3rd component
public class FlyingCamel : Camel
{
    public GameObject doubleCamel;
    public GameObject singleCamel;

    [Range(0, 25)]
    public float flyForce;

    [Range(0, 25)]
    public float speed;

    protected override void Awake()
    {
        body = GetComponent<Rigidbody2D>();

        // Update limits
        minY = transform.position.y;

        float limitX = transform.GetChild(2).transform.position.x;

        if (limitX < transform.position.x)
        {
            speed = -speed;
            minX = limitX;
            maxX = transform.position.x;
        }
        else
        {
            maxX = limitX;
            minX = transform.position.x;
        }

        base.Awake();
    }

    // Don't update texture like this
    void Update()
    {}

    void FixedUpdate()
    {
        if (transform.position.y <= minY)
            Flap();

        if ((speed > 0 && transform.position.x >= maxX) || (speed < 0 && transform.position.x <= minX))
            speed = -speed;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            transform.position,
            transform.GetChild(2).position
        );
    }

    // Replace it by a normal camel
    public override void OnHit()
    {
        AudioManager.Play("camel_hit");

        if (hitsToKill == 1)
        {
            Instantiate(singleCamel, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (hitsToKill == 2)
        {
            Instantiate(doubleCamel, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }

    void Flap()
    {
        AudioManager.Play("flap", transform.position.x);

        // Look towards
        transform.localScale = new Vector3(
            speed > 0 ? -1 : 1,
            1
        );

        // Fly
        body.velocity = new Vector2(
            speed,
            evenFlapNumber ? flyForce : flyForce * .75f
        );

        evenFlapNumber = !evenFlapNumber;
    }

    // If below, flyies
    float minY;

    // X limits
    float minX, maxX;

    bool evenFlapNumber = true;

    Rigidbody2D body;
}
