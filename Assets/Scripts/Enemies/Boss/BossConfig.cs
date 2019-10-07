using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossConfig : MonoBehaviour
{
    public bool toggleLaser;
    public bool attackManyMissiles;
    public bool attackSimpleMissiles;
    public bool setY;

    // 0 = No set, 1 = Down, 2 = Mid, 3 = Up
    public int setFocus;

    void FixedUpdate()
    {
        // We can attack
        if (Boss.instance.transform.position.x >= transform.position.x)
        {
            if (toggleLaser)
                Boss.instance.ToggleLaser();

            if (attackManyMissiles)
                Boss.instance.AttackManyMissiles();

            if (attackSimpleMissiles)
                Boss.instance.AttackSimpleMissiles();

            if (setY)
                Boss.instance.minY = transform.position.y;

            switch (setFocus)
            {
                case 1:
                    CameraFollow.instance.target = Boss.instance.focusDown;
                    break;

                case 2:
                    CameraFollow.instance.target = Boss.instance.focusMid;
                    break;

                case 3:
                    CameraFollow.instance.target = Boss.instance.focusUp;
                    break;
            }

            Destroy(gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position + new Vector3(0, 20), transform.position + new Vector3(0, -20));
        Gizmos.DrawSphere(transform.position, .1f);
    }
}
