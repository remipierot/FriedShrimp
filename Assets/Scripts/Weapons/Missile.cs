using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    public float RotateSpeed = 3f;
    public float FollowDuration = 1f;

    private Transform _Player;
    private WaitForSeconds _PhysicsTimeStep;

    void Awake()
    {
        _Body = GetComponent<Rigidbody>();
        _PhysicsTimeStep = new WaitForSeconds(Time.fixedDeltaTime);
    }

	private void Start()
	{
        _Player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

	private void OnEnable()
	{
        StartCoroutine(StartFollow(FollowDuration));
	}

	IEnumerator StartFollow(float followDuration)
	{
        while(followDuration > 0f)
		{
            followDuration -= Time.fixedDeltaTime;

            if(_Player != null)
			{
                Vector3 dir = _Player.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), RotateSpeed * Time.fixedDeltaTime);
			}

            _Body.velocity = transform.forward * Speed;

            yield return _PhysicsTimeStep;
        }
	}
}
