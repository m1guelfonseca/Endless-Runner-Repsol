using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    [SerializeField] Transform gameModel;

    //max values 
    float maxSteerVelocity = 2;
    float maxForwardVelocity = 30;

    float speedBaseSteerLimit = 0;
    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 5;

    Vector2 input = Vector2.zero;

    void Update()
    {
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);
    }


    private void FixedUpdate()
    {
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
        } else
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
}