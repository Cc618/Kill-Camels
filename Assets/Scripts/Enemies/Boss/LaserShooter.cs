using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// First child is offset
public class LaserShooter : MonoBehaviour
{
    // Whether we shot the laser
    public bool laserActive;

    public GameObject lightFX;
    public GameObject particles;

    void Awake()
    {
        system = particles.GetComponentInChildren<ParticleSystem>();
        lightFXComponent = lightFX.GetComponentInChildren<Light>();

        child = transform.GetChild(0).gameObject;
        line = GetComponent<LineRenderer>();

        masks = LayerMask.GetMask("Ground", "Player");
    }

    // Update is called once per frame
    void Update()
    {
        var hit = Physics2D.Raycast(to2D(child.transform.position), new Vector2(1, 0), 100f, masks);

        if (!hit)
        {
            if (lightFXComponent.enabled)
            {
                lightFXComponent.enabled = false;
                system.Stop();
            }

            line.positionCount = 0;
        }
        else
        {
            if (!lightFXComponent.enabled)
            {
                lightFXComponent.enabled = true;

                system.Play();
            }

            line.positionCount = 2;

            // Update laser
            Vector3[] pos = new Vector3[2];

            pos[0] = child.transform.position;
            pos[1] = to3D(hit.point);

            line.SetPositions(pos);

            // Set FX position
            lightFX.transform.position = particles.transform.position = pos[1];

            // Check if it's the player and kill it
            PlayerMovements p; 
            if (p = hit.collider.gameObject.GetComponent<PlayerMovements>())
                p.Kill();
        }
    }

    void OnDisable()
    {
        lightFXComponent.enabled = false;
     
        system.Stop();
    }

    void OnEnable()
    {
        lightFXComponent.enabled = true;

        system.Play();
    }

    Vector2 to2D(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    Vector3 to3D(Vector2 v)
    {
        return new Vector3(v.x, v.y);
    }

    int masks;

    GameObject child;
    LineRenderer line;
    ParticleSystem system;
    Light lightFXComponent;
}
