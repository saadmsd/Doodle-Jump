using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Vérifier si la collision est avec une plateforme
        if (collision.relativeVelocity.y > 0f )
        {
            Debug.Log("Collision with platform");
            if (collision.collider.CompareTag("platformV") || collision.collider.CompareTag("platformB"))
            {
                animator.SetBool("IsBouncing", true);
                // Désactiver l'animation après un court moment
                Invoke("StopBouncing", 0.3f);
            }
            else if (collision.collider.CompareTag("ressort"))
            {
                animator.SetBool("IsBouncing", true);
                // Désactiver l'animation après un court moment
                Invoke("StopBouncing", 1f);
            }

        }
    }

    private void StopBouncing()
    {
        animator.SetBool("IsBouncing", false);
    }
}