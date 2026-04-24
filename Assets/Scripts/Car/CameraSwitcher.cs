using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cinemachine Cameras")]
    [SerializeField] CinemachineCamera thirdPersonCamera;
    [SerializeField] CinemachineCamera firstPersonCamera;

    [Header("Priorities")]
    [SerializeField] int activePriority = 10;
    [SerializeField] int inactivePriority = 0;

    [Header("Rearview Mirror")]
    [SerializeField] Camera rearviewCamera;

    [Header("Car Point Names")]
    [SerializeField] string fpvPointName = "FPVPoint";
    [SerializeField] string rearviewPointName = "RearviewPoint";

    bool isFirstPerson = false;
    Transform rearviewTarget;

    void Start()
    {
        SetThirdPerson();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.vKey.wasPressedThisFrame)
            ToggleCamera();
    }

    void LateUpdate()
    {
        if (rearviewTarget == null || rearviewCamera == null)
            return;

        rearviewCamera.transform.position = rearviewTarget.position;
        rearviewCamera.transform.rotation = rearviewTarget.rotation;
    }

    public void SetCarTarget(GameObject car)
    {
        // third-person camera
        thirdPersonCamera.Follow = car.transform;

        // first-person camera
        Transform fpvPoint = car.transform.Find(fpvPointName);
        if (fpvPoint == null)
            return; 
        

        firstPersonCamera.Follow = fpvPoint;
        firstPersonCamera.LookAt = fpvPoint;
        

        // rearview mirror, save the transformation and follow it in LateUpdate
        Transform rearviewPoint = car.transform.Find(rearviewPointName);
        if (rearviewPoint != null)
        {
            rearviewTarget = rearviewPoint;
        }
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
