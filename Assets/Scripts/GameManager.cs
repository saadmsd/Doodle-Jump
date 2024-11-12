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
    public GameObject ressortPrefab; // Référence au prefab de ressort

    public int numberOfPlatforms = 200;
    private float ressortSpawnChance = 0.2f; // Probabilité d'apparition du ressort (20%)

    
    public Text scoreText; // Référence au composant UI Text pour afficher le score
    private int score = 0;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assigne cette instance à la propriété statique Instance
            DontDestroyOnLoad(gameObject); // Conserve l'objet entre les scènes
        }
        else
        {
            Destroy(gameObject); // Détruit les objets en double
        }
    }
    void Start()
    {   
        // Initialiser le score à 0
        UpdateScoreText();

        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(1f, 3f);
            spawnPosition.x = Random.Range(-2.5f, 2.5f);

            int platformType = Random.Range(0, 6);
            GameObject platform = null; // Référence pour la plateforme instanciée

            if (platformType == 0)
            { // Plateforme marron + plateforme bleue ou verte
                Vector3 spawnPosition2 = new Vector3(spawnPosition.x, spawnPosition.y);
                spawnPosition2.y += Random.Range(0f, 3f);
                spawnPosition2.x = Random.Range(-2.5f, 2.5f);
                Instantiate(platformMPrefab, spawnPosition2, Quaternion.identity);

                int platformType2 = Random.Range(0, 2); // 0 = plateforme bleue, 1 = plateforme verte
                if (platformType2 == 0)
                    platform = Instantiate(platformBPrefab, spawnPosition, Quaternion.identity);
                else
                    platform = Instantiate(platformVPrefab, spawnPosition, Quaternion.identity);
            }
            else if (platformType == 1) // Plateforme bleue
            {
                platform = Instantiate(platformBPrefab, spawnPosition, Quaternion.identity);
            }
            else // Plateforme verte
            {
                platform = Instantiate(platformVPrefab, spawnPosition, Quaternion.identity);
            }

            // Ajouter un ressort avec une probabilité de 0,2 sur les plateformes B et V
            if (platform != null && (platformType == 1 || platformType >= 2))
            {
                if (Random.value < ressortSpawnChance)
                {
                    // Instancier le ressort comme enfant de la plateforme
                    GameObject ressort = Instantiate(ressortPrefab, platform.transform);

                    // Calculer une position aléatoire en X dans les limites de la largeur de la plateforme
                    float randomXPosition = Random.Range(-0.2f, 0.2f);

                    // Positionner le ressort à cette position aléatoire
                    ressort.transform.localPosition = new Vector3(randomXPosition, 0.1f, 0f);
                }
            }
        }
    }

    public void PlayerDied()
    {
        // Affiche le score actuel dans la console
        Debug.Log("Game Over! Score: " + score);

        // Retourne au menu principal (assume que la scène 0 est le menu)
        SceneManager.LoadScene(0);
    }

    // Met à jour le texte du score
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Met à jour le texte du score dans l'UI
        }
    }

    public void IncreaseScore()
    {
        score++;  // Augmente le score
        UpdateScoreText();  // Met à jour l'affichage du score
    }
}