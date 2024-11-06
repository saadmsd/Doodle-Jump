using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;

    private float moveX;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;

        // inverser le sprite du joueur si on va à gauche
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }        

        // Si le joueur sort de l'écran, on le replace de l'autre côté
        if (transform.position.x < -3f)
        {
            transform.position = new Vector3(3f, transform.position.y, 0);
        }
        else if (transform.position.x > 3f)
        {
            transform.position = new Vector3(-3f, transform.position.y, 0);
        }
    }
}
