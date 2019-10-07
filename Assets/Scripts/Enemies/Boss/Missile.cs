using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [Range(0, 20)]
    public float speed;

    [Range(0, 5)]
    public float angularSpeed;

    public bool noExplosion;

    public GameObject particles;

    void Start()
    {
        player = PlayerMovements.instance.gameObject;
        body = GetComponent<Rigidbody2D>();

        if (!noExplosion)
            StartCoroutine(ExplodeTimer());
    }

    void FixedUpdate()
    {
        Vector2 playerPos = to2D(player.transform.position),
            missilePos = to2D(transform.position),
            forward = to2D(transform.right),
            normal = new Vector2(forward.y, -forward.x);

        float delta = Vector2.Dot(playerPos - missilePos, normal);

        if (delta < 0)
            angle += angularSpeed * Time.fixedDeltaTime;
        else
            angle -= angularSpeed * Time.fixedDeltaTime;

        body.velocity = new Vector2(
            Mathf.Cos(angle) * speed,
            Mathf.Sin(angle) * speed
        );

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * angle);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerMovements.instance.Kill();

            Explode(false);
        }
        else
            Explode();
    }

    void Explode(bool audio = true)
    {
        if (audio)
            AudioManager.Play("bomb");

        Score.instance.score += 15;

        Instantiate(particles, new Vector3(
                transform.position.x,
                transform.position.y,
                -.5f
            ), Quaternion.identity);

        Destroy(gameObject);
    }

    // Explode in 10 seconds
    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(10);

        Explode();
    }

    Vector2 to2D(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    float angle = 0;

    GameObject player;
    Rigidbody2D body;
}
