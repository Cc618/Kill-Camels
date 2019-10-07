using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHump : MonoBehaviour
{
    public bool laser;

    public AnimationClip open;
    public AnimationClip close;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        bool animOpen = animator.GetBool("open");

        bool isOpen = laser ? Boss.instance.laserOn : Boss.instance.missileOn;

        if (animOpen != isOpen)
        {
            animator.SetBool("open", isOpen);

            AudioManager.Play(isOpen ? "hump_open" : "hump_close");
        }
    }

    Animator animator;
}
