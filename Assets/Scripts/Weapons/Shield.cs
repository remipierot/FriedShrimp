using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float ShieldDuration;
    public GameObject HitEffect;

    private WaitForSeconds _ShieldDuration;
    private Collider _Collider;

    void Start()
    {
        transform.localScale = Vector3.zero;
        _ShieldDuration = new WaitForSeconds(ShieldDuration);
        _Collider = GetComponent<Collider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !_Collider.enabled)
        {
            ShieldUp();
		}
    }

    public void ShieldUp()
	{
        StartCoroutine(EngageShield());
	}

    IEnumerator EngageShield()
	{
        _Collider.enabled = true;
        float inAnimDuration = .5f;
        float outAnimDuration = .5f;

        while(inAnimDuration > 0f)
		{
            inAnimDuration -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, .1f);
            yield return null;
		}

        yield return _ShieldDuration;

        while(outAnimDuration > 0f)
		{
            outAnimDuration -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, .1f);
            yield return null;
        }
        _Collider.enabled = false;

    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
		{
            var triggerPosition = other.ClosestPointOnBounds(transform.position);
            var direction = triggerPosition - transform.position;

            GameObject fx = PoolingManager.Instance.UseObject(HitEffect, triggerPosition, Quaternion.LookRotation(direction));
            PoolingManager.Instance.ReturnObject(fx, 1f);

            var hs = other.GetComponent<HealthSystem>();

            if (hs != null)
            {
                hs.TakeDamage(1000f);
            }
            else
                PoolingManager.Instance.ReturnObject(other.gameObject);
		}
	}
}
