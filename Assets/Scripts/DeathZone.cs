using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject player;  // Référence au joueur (Doodler)
    public float offsetHeight = 8f;  // Décalage vertical (distance entre la Death Zone et le joueur)
    private float lastPlayerYPosition;  // Dernière position Y du joueur

    void Start()
    {
        // Initialisation de la dernière position Y du joueur
        lastPlayerYPosition = player.transform.position.y - offsetHeight;
    }

    void FixedUpdate()
    {
        // Vérifier si le joueur monte (si sa position Y augmente)
        if (player.transform.position.y > lastPlayerYPosition)
        {
            // Si le joueur monte, la Death Zone suit la position Y avec le décalage
            Vector3 newPosition = transform.position;
            newPosition.y = player.transform.position.y - offsetHeight;
            transform.position = newPosition;
        }

        // Mettre à jour la dernière position Y du joueur
        lastPlayerYPosition = player.transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Appeler la fonction de GameManager pour gérer la mort
            Debug.Log("Player died");
            GameManager.Instance.PlayerDied();
        }
        else
        {
            if (other.gameObject != gameObject)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
