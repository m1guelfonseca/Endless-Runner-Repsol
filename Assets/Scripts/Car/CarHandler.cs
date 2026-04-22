using UnityEngine;
using System.Collections;
using System;


public class CarHandler : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] Transform gameModel;

    [SerializeField] MeshRenderer carMeshRenderer;
    public MeshRenderer CarMeshRenderer => carMeshRenderer;

    [SerializeField] ExplodeHandler explodeHandler;

    [Header("SFX")]
    [SerializeField] AudioSource carEngineAS;

    [SerializeField] AnimationCurve carPitchAnimationCurve;

    [SerializeField] AudioSource carSkidAS;

    [SerializeField] AudioSource CarCrashAS;

    //max values 
    float maxSteerVelocity = 2;
    float maxForwardVelocity = 30;

    float speedBaseSteerLimit = 0;
    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 5;

    float CarMaxSpeedPercentage = 0;

    bool isExploded = false;
    bool isPlayer = false;

    Vector2 input = Vector2.zero;


    //Stats

    float carStartPositionZ;
    float distanceTraveled = 0;
    public float DistanceTraveled => distanceTraveled;

    //Events
    public event Action<CarHandler> OnPlayerCrashed;

    void Start()
    {
        isPlayer = CompareTag("Player");

        if (isPlayer)
        {
            carEngineAS.Play();
        }
        carStartPositionZ = transform.position.z;
    }

    void Update()
    {
        if (isExploded)
        {
            FadeOutCarAudio();
            return;
        }

        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);

        UpdateCarAudio();

        //Update distance traveled
        distanceTraveled = transform.position.z - carStartPositionZ;
    }


    private void FixedUpdate()
    {
        if (isExploded)
        {
            rb.linearDamping = rb.linearVelocity.z * 0.1f;
            rb.linearDamping = Mathf.Clamp(rb.linearDamping, 1.5f, 10);

            rb.MovePosition(Vector3.Lerp(transform.position, new Vector3(0, 0, transform.position.z), Time.deltaTime * 0.5f));
            return;
        }

        if (input.y > 0)
        {
            accelerate();
        }
        else
        {
            rb.linearDamping = 0.5f;
        }

        if (input.y < 0)
        {
            brake();
        }

        steer();

        //force the car not to go backwards
        if (rb.linearVelocity.z <= 0)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    void accelerate()
    {
        rb.linearDamping = 0;

        //stay within the speed limit
        if (rb.linearVelocity.z >= maxForwardVelocity)
            return;

        rb.AddForce(transform.forward * accelerationMultiplier * input.y);
    }

    void brake()
    {
        if (rb.linearVelocity.z <= 0)
        {
            rb.AddForce(transform.forward * brakeMultiplier * input.y);
        }
    }

    void steer()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            //move the car sideways 
            float speedBaseSteering = rb.linearVelocity.z / 5.0f;
            speedBaseSteerLimit = Mathf.Clamp01(speedBaseSteering);

            rb.AddForce(transform.right * steeringMultiplier * input.x * speedBaseSteering);

            // normalize the x velocity
            float normalizedX = rb.linearVelocity.x / maxSteerVelocity;

            //ensure that we dont allow it to get bigger than 1 in magnitude
            normalizedX = Mathf.Clamp(normalizedX, -1.0f, 1.0f);

            // make sure we stay within the turn speed limit
            rb.linearVelocity = new Vector3(normalizedX * maxSteerVelocity, 0, rb.linearVelocity.z);
        }
        else
        {
            // auto center car
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0, 0, rb.linearVelocity.z), Time.fixedDeltaTime * 3);
        }
    }

    void UpdateCarAudio()
    {
        if (!isPlayer)
        {
            return;
        }

        CarMaxSpeedPercentage = rb.linearVelocity.z / maxForwardVelocity;

        carEngineAS.pitch = carPitchAnimationCurve.Evaluate(CarMaxSpeedPercentage);

        if (input.y < 0 && CarMaxSpeedPercentage > 0.2f)
        {
            if (!carSkidAS.isPlaying)
            {
                carSkidAS.Play();

            }
            carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 1.0f, Time.deltaTime * 10);
        }
        else
        {
            carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 0, Time.deltaTime * 30);
        }

    }

    void FadeOutCarAudio()
    {
        if (!isPlayer)
        {
            return;
        }

        carEngineAS.volume = Mathf.Lerp(carEngineAS.volume, 0, Time.deltaTime * 10);
        carSkidAS.volume = Mathf.Lerp(carSkidAS.volume, 0, Time.deltaTime * 10);
    }

    public void setInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        input = inputVector;
    }

    public void SetMaxSpeed(float newMaxSpeed)
    {
        maxForwardVelocity = newMaxSpeed;
    }

    IEnumerator SlowDownTimeCO()
    {
        while (Time.timeScale > 0.2f)
        {
            Time.timeScale -= Time.deltaTime * 2;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        while (Time.timeScale <= 1f)
        {
            Time.timeScale += Time.deltaTime;
            yield return null;
        }

        Time.timeScale = 1.0f;
    }

    //Events
    private void OnCollisionEnter(Collision collision)
    {
        if (!isPlayer)
        {
            if (collision.transform.root.CompareTag("Untagged"))
                return;

            if (collision.transform.root.CompareTag("CarAI"))
                return;
        }

        Vector3 velocity = rb.linearVelocity;
        explodeHandler.Explode(velocity * 45);
        isExploded = true;

        CarCrashAS.volume = CarMaxSpeedPercentage;
        carEngineAS.volume = Mathf.Clamp(CarCrashAS.volume, 0.25f, 1.0f);

        CarCrashAS.pitch = CarMaxSpeedPercentage;
        CarCrashAS.pitch = Mathf.Clamp(CarCrashAS.pitch, 0.3f, 1.0f);
        CarCrashAS.Play();

        //trigger event
        OnPlayerCrashed?.Invoke(this);

        StartCoroutine(SlowDownTimeCO()); ;
    }
}