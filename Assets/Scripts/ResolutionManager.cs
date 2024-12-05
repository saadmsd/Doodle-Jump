using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Start()
    {
        // Définir la résolution au démarrage 10:16
        Screen.SetResolution(800, 1280, true);
        
    }
}