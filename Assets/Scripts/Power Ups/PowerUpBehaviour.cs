using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        // Add this line of code if the power ups layer can hit
        // other things than the player
        //if (c.gameObject.name == "Player")

        OnPlayerCollect(c.gameObject);
    }

    // Call super to remove it
    protected virtual void OnPlayerCollect(GameObject player)
    {
        Score.instance.score += 150;

        // Remove it
        Destroy(gameObject);
    }
}
