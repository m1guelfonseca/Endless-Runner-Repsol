using UnityEngine;

public class ExplodeHandler : MonoBehaviour
{
    [SerializeField] GameObject originalObject;
    [SerializeField] GameObject model;
    Rigidbody[] rigidBodies;

    private void Awake()
    {
        rigidBodies = model.GetComponentsInChildren<Rigidbody>(true);
    }

    void Start()
    {
        //Explode(Vector3.forward);
    }

    public void Explode(Vector3 externalForce)
    {
        originalObject.SetActive(false);
        foreach (Rigidbody rb in rigidBodies)
        {
            rb.transform.parent = null;
            rb.GetComponent<MeshCollider>().enabled = true;
            rb.gameObject.SetActive(true);
            rb.isKinematic = false;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.AddForce(Vector3.up * 200 + externalForce, ForceMode.Force);
            rb.AddTorque(Random.insideUnitSphere * 0.5f, ForceMode.Impulse);
        }
    }
}