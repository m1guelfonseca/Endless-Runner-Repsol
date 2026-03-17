using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    float accelerationMultiplier = 3;
    float brakeMultiplier = 15;
    float steeringMultiplier = 5;

    Vector2 input = Vector2.zero;

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
    }

    void accelerate()
    {
        rb.linearDamping = 0;
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
            rb.AddForce(transform.right * steeringMultiplier * input.x);
        }
    }

    public void setInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        input = inputVector;
    }
}