using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed { get; set; }
    public float Damage { get; set; }

    protected Rigidbody _Body;

    void Start()
    {
        _Body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _Body.velocity = transform.forward * Speed;
    }
}
