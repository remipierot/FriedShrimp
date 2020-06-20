using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float Speed = 3f;

    private Rigidbody _Body;
    private Vector3 _Movement;
    private Vector3 _Target;

    private Magnet _Magnet;

	private void Start()
	{
        _Magnet = FindObjectOfType<Magnet>();
	}

	void Awake()
    {
        _Body = GetComponent<Rigidbody>();
    }

	private void OnEnable()
	{
        _Movement = transform.position;
        _Movement += Random.insideUnitSphere * Speed;
        _Movement.y = 0f;
	}

	void FixedUpdate()
    {
        _Target = Vector3.Lerp(transform.position, _Movement, 1f * Time.deltaTime);

        if((_Magnet.transform.position - transform.position).sqrMagnitude <= Mathf.Pow(_Magnet.MagnetRange, 2f))
		{
            _Target = Vector3.Lerp(transform.position, _Magnet.transform.position, _Magnet.MagnetPower * Time.deltaTime);
		}

        _Body.MovePosition(_Target);
    }
}
