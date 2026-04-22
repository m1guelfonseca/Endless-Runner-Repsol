using System.Collections;
using UnityEngine;

public class FuelTankSpawner : MonoBehaviour
{
    [SerializeField] GameObject fuelTankPrefab;

    [SerializeField] float spawnInterval = 8f;

    // Overlap check
    [SerializeField] LayerMask otherCarsLayerMask;
    Collider[] overlappedCheckCollider = new Collider[1];

    GameObject[] fuelTankPool = new GameObject[5];

    Transform playerCarTransform;

    float timeLastFuelTankSpawned = 0;
    WaitForSeconds wait = new WaitForSeconds(0.5f);

    void Start()
    {
        playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < fuelTankPool.Length; i++)
        {
            fuelTankPool[i] = Instantiate(fuelTankPrefab);
            fuelTankPool[i].SetActive(false);
        }

        StartCoroutine(UpdateLessOftenCO());
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            CleanUpFuelTanksBeyondView();
            SpawnFuelTank();
            yield return wait;
        }
    }

    void SpawnFuelTank()
    {
        if (Time.time - timeLastFuelTankSpawned < spawnInterval)
            return;

        GameObject tankToSpawn = null;

        foreach (GameObject tank in fuelTankPool)
        {
            if (tank.activeInHierarchy)
                continue;

            tankToSpawn = tank;
            break;
        }

        if (tankToSpawn == null)
            return;

        // pick a random lane, same as AIHandler
        int randomLane = Random.Range(0, Utils.CarLanes.Length);
        Vector3 spawnPosition = new Vector3(Utils.CarLanes[randomLane], 0.055f, playerCarTransform.position.z + 80);

        if (Physics.OverlapBoxNonAlloc(spawnPosition, Vector3.one * 2, overlappedCheckCollider, Quaternion.identity, otherCarsLayerMask) > 0)
            return;

        tankToSpawn.transform.position = spawnPosition;
        tankToSpawn.SetActive(true);

        timeLastFuelTankSpawned = Time.time;
    }

    void CleanUpFuelTanksBeyondView()
    {
        foreach (GameObject tank in fuelTankPool)
        {
            if (!tank.activeInHierarchy)
                continue;

            if (tank.transform.position.z - playerCarTransform.position.z < -50)
                tank.SetActive(false);
        }
    }
}