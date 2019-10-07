using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        ratio = transform.position.z * .1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(
            cam.transform.position.x * ratio,
            cam.transform.position.y * ratio,
            transform.position.z
        );
    }

    float ratio;
}
