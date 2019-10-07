using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamelScore : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        PlayerMovements.instance.AirJump();
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Camel camel = GetComponentInParent<Camel>();

        camel.Hurt();
    }
}
