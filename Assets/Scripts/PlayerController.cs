using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public GameObject projectilePrefab;
    private float moveX;
    private bool IsShooting;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetTrigger("Shoot");
            Vector2 position = transform.position;
            position.y += 0.5f;
            GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
            Destroy(projectile, 1);
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
        // if (IsShooting)
        // {
        //     animator.SetTrigger("Shoot");
        //     GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        //     projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        // }
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
        if (transform.position.x < -3.5f)
        {
            transform.position = new Vector3(3.5f, transform.position.y, 0);
        }
        else if (transform.position.x > 3.5f)
        {
            transform.position = new Vector3(-3.5f, transform.position.y, 0);
        }
    }
}
