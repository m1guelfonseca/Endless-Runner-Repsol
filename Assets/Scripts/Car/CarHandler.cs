using UnityEngine;
using System.Collections;


public class CarHandler : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] Transform gameModel;

    [SerializeField] MeshRenderer carMeshRenderer;

    [SerializeField] ExplodeHandler explodeHandler;

    //max values 
    float maxSteerVelocity = 2;
    float maxForwardVelocity = 30;

    float speedBaseSteerLimit = 0;
    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 5;

    bool isExploded = false;
    bool isPlayer = false;

    Vector2 input = Vector2.zero;

    void Start()
    {
        isPlayer = CompareTag("Player");
    }   

    void Update()
    {
        if (isExploded)
        {
            return;
        }

        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);
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
        if(!isPlayer)
        {
            if(collision.transform.root.CompareTag("Untagged"))
                return;

            if(collision.transform.root.CompareTag("CarAI"))
                return;
        }

        Vector3 velocity = rb.linearVelocity;
        explodeHandler.Explode(velocity * 45);
        isExploded = true;

        StartCoroutine(SlowDownTimeCO()); ;
    }
}