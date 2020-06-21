using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStartData : MonoBehaviour
{
    public AutoShoot AutoShoot;
    public string WeaponStatName;

    void Start()
    {
        if(WeaponStatName == "Blaster")
		{
            AutoShoot.SwitchProfile(
            StatsManager.Instance.GetStatsValue(WeaponStatName, StatsManager.Instance.BlasterUpgrades)
            );
        }
        else if (WeaponStatName == "Missile")
        {
            AutoShoot.SwitchProfile(
            StatsManager.Instance.GetStatsValue(WeaponStatName, StatsManager.Instance.MissileUpgrades)
            );
        }

    }
}
