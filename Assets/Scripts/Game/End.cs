using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public static End instance;

    public string nextLevel;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CameraFollow.instance.maxX = transform.position.x -
            CameraFollow.instance.GetComponent<Camera>().orthographicSize *
            CameraFollow.instance.GetComponent<Camera>().aspect;

        // Avoid kills by limits
        if (CameraFollow.instance.isBoss)
            CameraFollow.instance.maxX += PlayerMovements.instance.transform.localScale.x * 2;
    }

    void FixedUpdate()
    {
        if (Input.GetKey("n") ||
            PlayerMovements.instance.transform.position.x >= transform.position.x)
            SceneManager.LoadScene(nextLevel);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(
            transform.position + new Vector3(0, 100),
            transform.position + new Vector3(0, -100)
        );
    }
}
