using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public float Damage;

    private Rigidbody _Body;

    void Start()
    {
        _Body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _Body.velocity = transform.forward * Speed;
    }
}
