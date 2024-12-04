using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject platformVPrefab;
    public GameObject platformBPrefab;
    public GameObject platformMPrefab;
    public GameObject ressortPrefab; 
    public GameObject holePrefab;

    public GameObject monster1Prefab;
    public GameObject monster2Prefab;
    public GameObject monster3Prefab;

    public int numberOfPlatforms = 200;
    private float ressortSpawnChance = 0.2f; 
    private float holeSpawnChance = 0.05f;
    private float monsterSpawnChance = 0.05f;

    public Text scoreText; 
    private float maxHeight = 0f; // Hauteur maximale atteinte par le joueur
    public Transform player;

    public GameObject gameOverUI; // Panneau Game Over


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {   
        UpdateScoreText();

        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(1f, 2.5f);
            spawnPosition.x = Random.Range(-2.5f, 2.5f);

            int platformType = Random.Range(0, 6);
            GameObject platform = null;

            if (platformType == 0)
            { 
                Vector3 spawnPosition2 = new Vector3(spawnPosition.x, spawnPosition.y);
                spawnPosition2.y += Random.Range(0f, 1f);
                if (spawnPosition.x > -1.5f && spawnPosition.x < 1.5f)
                {
                    spawnPosition2.x += Random.Range(0.5f, 1.5f) * (Random.value < 0.5f ? 1 : -1);
                }
                else
                {
                    spawnPosition2.x += -spawnPosition.x + Random.Range(0, 1.5f) * Mathf.Sign(spawnPosition.x);
                }
                Instantiate(platformMPrefab, spawnPosition2, Quaternion.identity);

                int platformType2 = Random.Range(0, 2); 
                if (platformType2 == 0)
                    platform = Instantiate(platformBPrefab, spawnPosition, Quaternion.identity);
                else
                    platform = Instantiate(platformVPrefab, spawnPosition, Quaternion.identity);
            }
            else if (platformType == 1) 
            {
                platform = Instantiate(platformBPrefab, spawnPosition, Quaternion.identity);
            }
            else 
            {
                platform = Instantiate(platformVPrefab, spawnPosition, Quaternion.identity);
            }

            if (platform != null && (platformType == 1 || platformType >= 2))
            {
                if (Random.value < ressortSpawnChance)
                {
                    GameObject ressort = Instantiate(ressortPrefab, platform.transform);
                    float randomXPosition = Random.Range(-0.2f, 0.2f);
                    ressort.transform.localPosition = new Vector3(randomXPosition, 0.1f, 0f);
                }
            }

            if (Random.value < holeSpawnChance)
            {   
                float randomXPosition;
                if (spawnPosition.x > -1.5f && spawnPosition.x < 1.5f)
                {
                    randomXPosition = spawnPosition.x + 2f * (Random.value < 0.5f ? 1 : -1);
                }
                else
                {
                    randomXPosition = -spawnPosition.x;
                }
                float randomYPosition = spawnPosition.y + Random.Range(-0.5f, 0.5f);
                Vector3 holePosition = new Vector3(randomXPosition, randomYPosition, 0f);
                Instantiate(holePrefab, holePosition, Quaternion.identity);
            }
            if (Random.value < monsterSpawnChance)
            {
                spawnPosition.y += 0.5f;
                spawnPosition.x += Random.Range(0, 2f) * -Mathf.Sign(spawnPosition.x);
                int monsterType = Random.Range(0, 3);
                if (monsterType == 0)
                {
                    GameObject monster = Instantiate(monster1Prefab, spawnPosition, Quaternion.identity);
                }
                else if (monsterType == 1)
                {
                    GameObject monster = Instantiate(monster2Prefab, spawnPosition, Quaternion.identity);
                }
                else
                {
                    GameObject monster = Instantiate(monster3Prefab, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float playerHeight = player.position.y;
            if (playerHeight > maxHeight)
            {
                maxHeight = playerHeight;
                UpdateScoreText();
            }
        }
    }
    
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(maxHeight);
        }
    }

    public void PlayerDied()
    {
        Debug.Log("Game Over! Score: " + Mathf.FloorToInt(maxHeight));

        // Afficher le panneau Game Over
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        

        // Mettre le jeu en pause
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Réactiver le temps
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recharger la scène actuelle
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Réactiver le temps
        SceneManager.LoadScene("MenuScene"); // Remplacer "Menu" par le nom exact de la scène du menu
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); 
        
        // Pour confirmer le fonctionnement dans l'éditeur Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }





    

}


