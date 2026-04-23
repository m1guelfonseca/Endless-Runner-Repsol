using UnityEngine;

public class FuelTankHandler : MonoBehaviour
{
    [SerializeField] float gasolineAmount = 30f;

    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        CarHandler carHandler = other.GetComponentInParent<CarHandler>();

        if (carHandler == null)
            return;

        // only the player can collect fuel
        if (!carHandler.CompareTag("Player"))
            return;

        carHandler.AddGasoline(gasolineAmount);
        gameObject.SetActive(false);
    }
}