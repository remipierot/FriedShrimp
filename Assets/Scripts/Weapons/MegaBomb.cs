using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaBomb : MonoBehaviour
{
    private float _Radius = 2f;
    private float _Damage = 5f;
    public ParticleSystem FX;

    void Start()
    {
        _Radius = StatsManager.Instance.GetStatsValue("MegaBomb", StatsManager.Instance.MegaBombUpgrades).Radius;
        _Damage = StatsManager.Instance.GetStatsValue("MegaBomb", StatsManager.Instance.MegaBombUpgrades).Damage;

        var fxMainParam = FX.main;
        fxMainParam.startSize = _Radius * fxMainParam.startSize.constant;
    }

    public void DropBomb()
	{
        FX.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, _Radius);

		foreach (var c in colliders)
		{
            var hs = c.GetComponent<HealthSystem>();

            if(hs != null && c.CompareTag("Enemy"))
			{
                hs.TakeDamage(_Damage);
			}
		}
	}
}
