using UnityEngine;
using Unity.Cinemachine;

public class PlayerCarSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject[] carPrefabs;

    [Header("Camera")]
    [SerializeField]
    CinemachineCamera cinemachineCamera;

    [Header("Menu")]
    [SerializeField]
    bool isMainMenu = false;

    //instanciated car
    GameObject instanciatedPlayerCar = null;

    //which car is selected
    int carIndex = 0;

    //selected car from menu
    static GameObject selectedCarPrefab = null;
    
    Quaternion carRotation = Quaternion.identity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isMainMenu)
        {
            instanciatedPlayerCar = Instantiate(carPrefabs[carIndex].GetComponent<CarHandler>().CarMeshRenderer.gameObject);
            selectedCarPrefab = carPrefabs[carIndex];
        }
        else
        {
            if (selectedCarPrefab != null)
            {
                instanciatedPlayerCar = Instantiate(selectedCarPrefab);
            }
            else
            {
                instanciatedPlayerCar = Instantiate(carPrefabs[0]);
            }
        }

        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow = instanciatedPlayerCar.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMainMenu)
        {
            instanciatedPlayerCar.transform.Rotate(new Vector3(0, 20, 0) * Time.deltaTime);

            carRotation = instanciatedPlayerCar.transform.rotation;
        }
    }

    void ChangeCar()
    {
        Destroy(instanciatedPlayerCar);

        instanciatedPlayerCar = Instantiate(carPrefabs[carIndex].GetComponent<CarHandler>().CarMeshRenderer.gameObject);

        selectedCarPrefab = carPrefabs[carIndex];

        instanciatedPlayerCar.transform.rotation = carRotation;
    }

    public void OnNextCarClicked()
    {
        carIndex++;

        if (carIndex > carPrefabs.Length - 1)
        {
            carIndex = 0;
        }

        ChangeCar();
    }

    public void OnPreviousCarClicked()
    {
        carIndex--;

        if (carIndex < 0)
        {
            carIndex = carPrefabs.Length - 1;
        }

        ChangeCar();
    }
}