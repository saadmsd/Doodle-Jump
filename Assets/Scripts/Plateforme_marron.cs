using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme_marron : MonoBehaviour
{
    private Animator animator;
    private void OnTriggerEnter2D(Collider2D other)
    {   
        
        // la plateforme tombe si le joueur est dessus
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (playerRb.velocity.y <= 0)
            {
                animator = GetComponent<Animator>();
                animator.SetBool("crash", true);
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.gravityScale = 1;
                Destroy(gameObject, 2f);
            }
        }
        

    }
}
