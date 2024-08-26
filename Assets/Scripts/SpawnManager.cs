using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefab; 
    public float initialDelay = 1f;  
    public float spawnInterval = 20f;  
    public float speedIncreaseRate = 0.9f;  

    private float currentInterval;

    void Start()
    {
        currentInterval = spawnInterval;
        StartCoroutine(SpawnPrefab());
    }

    IEnumerator SpawnPrefab()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(currentInterval);

            currentInterval *= speedIncreaseRate; 
        }
    }
}
