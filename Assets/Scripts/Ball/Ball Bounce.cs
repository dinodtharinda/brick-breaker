using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxVelocity = 8f;
    public ObjectPool ballPool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Set the initial velocity with a random angle between -5 and 5
        float randomAngle = Random.Range(-5f, 5f);
        Vector2 initialVelocity = Quaternion.Euler(0, 0, randomAngle) * Vector2.up * 3f;
        rb.velocity = initialVelocity;
    }

    void Update()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
        else
        {
            float increaseFactor = 1.1f;
            rb.velocity *= increaseFactor;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ReflectWithRandomAngle(collision);
    }

    void ReflectWithRandomAngle(Collision2D collision)
    {
        // Get the collision normal
        Vector2 normal = collision.GetContact(0).normal;

        // Calculate the reflection angle
        float reflectionAngle = Vector2.SignedAngle(rb.velocity, normal) + Random.Range(-5f, 5f);

        // Set the new velocity with the reflection angle
        rb.velocity = Quaternion.Euler(0, 0, reflectionAngle) * rb.velocity;
    }
}
