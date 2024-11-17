using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme_bleu : MonoBehaviour
{
    public float jumpForce = 10f;
    public float speed = 2f; // Vitesse de déplacement de la plateforme
    public float moveDistance = 3f; // Distance maximale de déplacement depuis la position initiale

    private Vector3 startPosition;
    private float leftLimit = -2.5f;
    private float rightLimit = 2.5f;
    private bool movingRight = true;
 
    

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        // Vérifier la direction et déplacer la plateforme
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            // Inverser la direction si la limite droite est atteinte
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            // Inverser la direction si la limite gauche est atteinte
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;

               
            }
        }
    }
    
}
