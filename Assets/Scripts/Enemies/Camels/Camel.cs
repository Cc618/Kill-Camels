using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A camel has 2 children :
// - Death : If the player touches its trigger he dies
// - Score : If the player touches its trigger he kills the camel
public class Camel : Enemy
{
    public Sprite oneHumpSprite;

    // How many hits to kill him ?
    public int hitsToKill;

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        sizeX = transform.localScale.x;
        sizeY = transform.localScale.y;
    }

    void Update()
    {
        // Look towards the player
        transform.localScale = new Vector3(
            PlayerMovements.instance.transform.position.x > transform.position.x ? -sizeX: sizeX,
            sizeY
        );
    }

    // When the player hits the camel and the camel is still alive
    public virtual void OnHit()
    {
        Score.instance.score += 50;

        AudioManager.Play("camel_hit");

        // To one hump
        if (hitsToKill == 1)
            GetComponent<SpriteRenderer>().sprite = oneHumpSprite;

        CameraFollow.instance.Shake(.25f);
    }

    public override void OnDeath()
    {
        Score.instance.score += 100;
        
        AudioManager.Play("camel_death");

        CameraFollow.instance.Shake(.75f);

        base.OnDeath();
    }

    public void Hurt()
    {
        // Decrease life
        --hitsToKill;

        // Kill him
        if (hitsToKill == 0)
            OnDeath();
        else
            OnHit();
    }

    protected SpriteRenderer sprite;

    float sizeX, sizeY;
}
