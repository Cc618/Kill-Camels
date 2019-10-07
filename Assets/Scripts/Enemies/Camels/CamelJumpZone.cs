using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamelJumpZone : MonoBehaviour
{
    [Range(0, 100)]
    public float jumpForce;

    void OnTriggerEnter2D(Collider2D c)
    {
        Jump();
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Jump();
    }

    void Jump()
    {
        var body = GetComponentInParent<Rigidbody2D>();

        // Only if grounded
        if (body.velocity.y == 0)
            body.velocity = new Vector2(0, jumpForce);
    }
}
