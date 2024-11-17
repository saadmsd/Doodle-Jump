using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject platformVPrefab;
    public GameObject platformBPrefab;
    public GameObject platformMPrefab;
    public GameObject ressortPrefab; 

    public int numberOfPlatforms = 200;
    private float ressortSpawnChance = 0.2f; 

    public Text scoreText; 
    private float maxHeight = 0f; // Hauteur maximale atteinte par le joueur
    public Transform player; // Référence au joueur (à assigner dans l'inspecteur)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            spawnPosition.y += Random.Range(1f, 3f);
            spawnPosition.x = Random.Range(-2.5f, 2.5f);

            int platformType = Random.Range(0, 6);
            GameObject platform = null;

            if (platformType == 0)
            { 
                Vector3 spawnPosition2 = new Vector3(spawnPosition.x, spawnPosition.y);
                spawnPosition2.y += Random.Range(0f, 3f);
                spawnPosition2.x = Random.Range(-2.5f, 2.5f);
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
        }
    }

    void Update()
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
        SceneManager.LoadScene(0);
    }
}
