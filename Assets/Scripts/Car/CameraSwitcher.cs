using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] CinemachineCamera thirdPersonCamera;
    [SerializeField] CinemachineCamera firstPersonCamera;

    [Header("Priorities")]
    [SerializeField] int activePriority = 10;
    [SerializeField] int inactivePriority = 0;

    [Header("FPV Config")]
    [SerializeField] string fpvPointName = "FPVPoint";

    bool isFirstPerson = false;

    void Start()
    {
        SetThirdPerson();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.vKey.wasPressedThisFrame)
        {
            ToggleCamera();
        }
    }

    // assigns targets for both cameras
    public void SetCarTarget(GameObject car)
    {
        thirdPersonCamera.Follow = car.transform;

        Transform fpvPoint = car.transform.Find(fpvPointName);

        if (fpvPoint == null)
            return;


        firstPersonCamera.Follow = fpvPoint;
        firstPersonCamera.LookAt = fpvPoint;
    }

    void ToggleCamera()
    {
        isFirstPerson = !isFirstPerson;

        if (isFirstPerson)
            SetFirstPerson();
        else
            SetThirdPerson();
    }

    void SetThirdPerson()
    {
        thirdPersonCamera.Priority = activePriority;
        firstPersonCamera.Priority = inactivePriority;
    }

    void SetFirstPerson()
    {
        firstPersonCamera.Priority = activePriority;
        thirdPersonCamera.Priority = inactivePriority;
    }
}
