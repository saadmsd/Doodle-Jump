using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float jumpForce = 10f;
    public float speed; // Vitesse de déplacement de la plateforme

    public Rigidbody2D rb;
    private Vector3 startPosition;
    private float limitx = 0.1f;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        speed = Random.Range(1f, 3f);
    }

    void FixedUpdate()
    {
        // boomerang effect
        if (transform.position.x > startPosition.x + limitx)
        {
            speed = -Mathf.Abs(speed);
        }
        else if (transform.position.x < startPosition.x - limitx)
        {
            speed = Mathf.Abs(speed);
        }
        rb.velocity = new Vector2(speed, rb.velocity.y);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rbP = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rbP != null)
            {
                
                Vector2 velocity = rbP.velocity;
                velocity.y = jumpForce;
                rbP.velocity = velocity;

                rb.velocity = new Vector2(0,-jumpForce);
                
            }
        }
        else if (collision.relativeVelocity.y > 0f)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.Instance.PlayerDied();
            }
            else if (collision.gameObject.CompareTag("projectile"))
            {
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
    
}
