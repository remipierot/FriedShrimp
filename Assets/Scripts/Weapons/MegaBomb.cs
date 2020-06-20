using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBomb : MonoBehaviour
{
    public float Radius = 2f;
    public float Damage = 5f;
    public ParticleSystem FX;

    void Start()
    {
        var fxMainParam = FX.main;
        fxMainParam.startSize = Radius * fxMainParam.startSize.constant;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
		{
            DropBomb();
		}
    }

    public void DropBomb()
	{
        FX.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, Radius);

		foreach (var c in colliders)
		{
            var hs = c.GetComponent<HealthSystem>();

            if(hs != null && c.CompareTag("Enemy"))
			{
                hs.TakeDamage(Damage);
			}
		}
	}
}
