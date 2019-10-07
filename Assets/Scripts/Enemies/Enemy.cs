using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // When he dies
    public virtual void OnDeath()
    {
        // Remove it
        Destroy(gameObject);
    }
}
