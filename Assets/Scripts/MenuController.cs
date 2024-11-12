using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        // Charge la scène de jeu
        SceneManager.LoadScene("PlayScene");  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
