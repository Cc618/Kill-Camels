using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamelDeath : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        PlayerMovements.instance.Kill();
    }
}
