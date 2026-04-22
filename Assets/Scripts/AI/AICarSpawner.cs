using System.Collections;
using UnityEngine;

public class AICarSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] carAIPrefabs;

    GameObject[] carAIPool = new GameObject[20];    

    Transform playerCarTransform;

    //Timing
    float timeLastCarSpawned = 0;
    WaitForSeconds wait = new WaitForSeconds(0.5f);

    // Overlapped check
    [SerializeField]
    LayerMask otherCarsLayerMask;
    Collider[] overlappedCheckCollider = new Collider[1];
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        for (int i = 0; i < carAIPool.Length; i++)
        {
            carAIPool[i] = Instantiate(carAIPrefabs[prefabIndex]);
            carAIPool[i].SetActive(false);

            prefabIndex++;

            // loop the prefab index if we run out of prefabs
            if (prefabIndex > carAIPrefabs.Length -1)
                prefabIndex = 0;
        }

        StartCoroutine(UpdateLessOftenCO());
    }



    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            CleanUpCarsBeyoundView();
            SpawnNewCars();

            yield return wait;
        }
    }

    void SpawnNewCars()
    {
        float spawnInterval = DifficultyManager.Instance != null ? DifficultyManager.Instance.SpawnInterval : 1.4f;
        if (Time.time - timeLastCarSpawned < spawnInterval)
            return;

        GameObject carToSpawn = null;

        foreach (GameObject aiCar in carAIPool)
        {
            // Skip active cars
            if (aiCar.activeInHierarchy)
                continue;

            carToSpawn = aiCar;
            break;
        }

        // No car available in the pool
        if (carToSpawn == null)
            return;

        float aheadDistance = DifficultyManager.Instance != null ? DifficultyManager.Instance.SpawnAheadDistance : 70f;
        Vector3 spawnPosition = new Vector3(0, 0.055f, playerCarTransform.position.z + aheadDistance);

        if (Physics.OverlapBoxNonAlloc(spawnPosition, Vector3.one * 2, overlappedCheckCollider, Quaternion.identity, otherCarsLayerMask) > 0)
            return;

        carToSpawn.transform.position = spawnPosition;
        carToSpawn.SetActive(true);

        timeLastCarSpawned = Time.time;
    }

    void CleanUpCarsBeyoundView()
    {
        foreach (GameObject aiCar in carAIPool)
        {   
            // skip inactive cars
            if (!aiCar.activeInHierarchy)
                continue;
            //check if ai car is too far ahead
            if (aiCar.transform.position.z - playerCarTransform.position.z > 200)
                aiCar.SetActive(false);

            //check if ai car is too far behind
            if (aiCar.transform.position.z - playerCarTransform.position.z < -50)
                aiCar.SetActive(false);

            
        }
    }
}
