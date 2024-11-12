using UnityEngine;

public class DeathZoneFollow : MonoBehaviour
{
    public Transform player; // Référence au personnage
    public float offsetY = -10f; // Décalage Y de la DeathZone par rapport au personnage

    void Update()
    {
        // Met à jour la position de la DeathZone
        if (player != null)
        {
            // Se place à la même position X et Z que le personnage, mais ajuste la position Y
            transform.position = new Vector3(transform.position.x, player.position.y + offsetY, transform.position.z);
        }
    }
}
