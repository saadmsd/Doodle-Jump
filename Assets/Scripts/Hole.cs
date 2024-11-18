using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifier si l'objet qui entre est le joueur
        if (other.CompareTag("Player"))
        {
            // Appeler la fonction Game Over dans le GameManager
            GameManager.Instance.PlayerDied();
        }
    }
}
