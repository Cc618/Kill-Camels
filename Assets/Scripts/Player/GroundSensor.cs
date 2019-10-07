using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isOnGround
    {
        get
        {
            return groundContacts > 0;
        }
    }

    void Awake()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == groundLayer)
            ++groundContacts;
    }

    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.layer == groundLayer)
            --groundContacts;
    }

    // Ground contacts count
    public int groundContacts;
    int groundLayer;
}
