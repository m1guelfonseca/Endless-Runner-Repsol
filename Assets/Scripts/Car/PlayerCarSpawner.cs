using UnityEngine;
using Unity.Cinemachine;

public class PlayerCarSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] carPrefabs;

    [Header("Camera")]
    [SerializeField]
    CinemachineCamera cinemachineCamera;

    //instanciated car
    GameObject instanciatedPlayerCar = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instanciatedPlayerCar = Instantiate(carPrefabs[0]);

        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = instanciatedPlayerCar.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
