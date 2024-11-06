using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject platformVPrefab;
    public GameObject platformBPrefab;
    public GameObject platformGPrefab;


    public int numberOfPlatforms = 200;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(1f, 3f);
            spawnPosition.x = Random.Range(-2.5f, 2.5f);
            int platformType = Random.Range(0, 6);
            if (platformType == 0){ // platform grise + platform bleu ou verte
                Vector3 spawnPosition2 = new Vector3(spawnPosition.x, spawnPosition.y);
                spawnPosition2.y += Random.Range(0f, 3f);
                spawnPosition2.x = Random.Range(-2.5f, 2.5f);
                Instantiate(platformGPrefab, spawnPosition2, Quaternion.identity);
                int platformType2 = Random.Range(0, 2); // 0 = platform bleu, 1 = platform verte
                if (platformType2 == 0)
                    Instantiate(platformBPrefab, spawnPosition, Quaternion.identity);
                else
                    Instantiate(platformVPrefab, spawnPosition, Quaternion.identity);
            }
            else if (platformType == 1) // platform bleu
                Instantiate(platformBPrefab, spawnPosition, Quaternion.identity);
            else // platform verte
                Instantiate(platformVPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
