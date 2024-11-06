using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateform_grise : MonoBehaviour
{
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        // la plateforme tombe si le joueur est dessus
        if (collision.relativeVelocity.y <= 0f)
        {

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
            Destroy(gameObject, 2f);
            
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, -Mathf.Abs(playerRb.velocity.y)); // on inverse la vitesse du joueur en y
            }
        }
        

    }
}
