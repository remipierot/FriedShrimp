using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Missile : Bullet
{
    public float RotateSpeed = 3f;
    public float FollowDuration = 1f;
    public bool FiredByPlayer = false;

    private Transform _Target;
    private WaitForSeconds _PhysicsTimeStep;

    void Awake()
    {
        _Body = GetComponent<Rigidbody>();
        _PhysicsTimeStep = new WaitForSeconds(Time.fixedDeltaTime);
    }

	private void Start()
	{
        if(!FiredByPlayer)
		{
            _Target = GameObject.FindGameObjectWithTag("Player")?.transform;
		}
    }

	private void OnEnable()
	{
        if (FiredByPlayer)
        {
            _Target = FindEnemy();
        }

        StartCoroutine(StartFollow(FollowDuration));
	}

	IEnumerator StartFollow(float followDuration)
	{
        while(followDuration > 0f)
		{
            followDuration -= Time.fixedDeltaTime;

            if(_Target != null)
			{
                Vector3 dir = _Target.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), RotateSpeed * Time.fixedDeltaTime);
			}

            _Body.velocity = transform.forward * Speed;

            yield return _PhysicsTimeStep;
        }
	}

    Transform FindEnemy()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length > 1)
            Array.Sort(enemies, delegate (GameObject a, GameObject b)
            {
                return Vector3.Distance(transform.position, a.transform.position)
                .CompareTo(Vector3.Distance(transform.position, b.transform.position));
            });

        if (enemies.Length > 0 && enemies[0].GetComponent<HealthSystem>().enabled)
            return enemies[0].transform;
        else
            return null;
	}
}
