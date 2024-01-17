using UnityEngine;
using UnityEngine.UI;

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
        // rb.velocity = Vector2.up * 3f;
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

}
