using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [Range(0, 1)]
    public float ratio;

    void Awake()
    {
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        transform.position = new Vector3();

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition, Camera.MonoOrStereoscopicEye.Mono) * ratio;

        transform.position = new Vector3(
            transform.position.x + 3.5f * ratio,
            transform.position.y,
            -10
        );
    }
}
