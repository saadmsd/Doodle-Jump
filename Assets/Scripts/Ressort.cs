using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressort : MonoBehaviour
{
    public float jumpForce = 15f;
    private Animator animator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {   
                animator = GetComponent<Animator>();
                animator.SetTrigger("Bounce");
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce; 
                rb.velocity = velocity;
            }
        }
    }
}
