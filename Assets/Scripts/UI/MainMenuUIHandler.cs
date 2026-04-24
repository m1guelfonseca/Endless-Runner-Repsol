using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIHandler : MonoBehaviour
{
    [SerializeField] private Transform backgroundPlane;

    void Start()
    {
        FitPlaneToCamera();
    }    

    void FitPlaneToCamera()
    {
        Camera cam = Camera.main;
        float distance = Vector3.Distance(cam.transform.position, backgroundPlane.position);
        float height = 2f * distance * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad) * 1.1f;
        float width = height * cam.aspect;

        backgroundPlane.localScale = new Vector3(width / 10f, 1f, height / 10f);

        Vector3 newPos = cam.transform.position + cam.transform.forward * distance;
        backgroundPlane.position = new Vector3(newPos.x, newPos.y - 0.02f, backgroundPlane.position.z);
    }

    public void OnStartGameClicked()
    {
        SceneManager.LoadScene("Stage");
    }
}