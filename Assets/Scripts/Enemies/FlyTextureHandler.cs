using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// One texture when we go up and another when we go down
public class FlyTextureHandler : MonoBehaviour
{
    public Sprite up;
    public Sprite down;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        sprite.sprite = body.velocity.y < 0 ? up : down;
    }

    Rigidbody2D body;
    SpriteRenderer sprite;
}
