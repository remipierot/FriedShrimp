using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    public int Lives = 5;
    public int Money;

    [Header("Upgrades")]
    public List<ShootProfile> BlasterUpgrades = new List<ShootProfile>();
    public List<ShootProfile> MissileUpgrades = new List<ShootProfile>();
    public List<float> HealthUpgrades = new List<float>();
    public List<MegaBombData> MegaBombUpgrades = new List<MegaBombData>();
    public List<ShieldData> ShieldUpgrades = new List<ShieldData>();
    public List<LaserData> LaserUpgrades = new List<LaserData>();

    public Dictionary<string, Medals> Achievements = new Dictionary<string, Medals>();
    public Dictionary<string, DateTime> StatsTimer = new Dictionary<string, DateTime>();

    [Header("Upgrade Timers")]
    public List<StatsUpgradeInfo> Stats = new List<StatsUpgradeInfo>();

    private void Awake()
	{
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
		{
            Destroy(gameObject);
		}
	}

    public void AddMoney(int value)
	{
        Money += value;
	}

    public T GetStatsValue<T>(string statName, List<T> statsList)
	{
		foreach (var s in Stats)
		{
            if(s.Name == statName)
            { 
                return statsList[s.Level - 1];
            }
		}

        return default(T);
	}

    public float[] GetUpgradeTime(string statName)
	{
        foreach (var s in Stats)
        {
            if (s.Name == statName)
            {
                return s.UpgradeTime;
            }
        }

        return null;
    }

    public StatsUpgradeInfo GetStats(string statName)
	{
        foreach (var s in Stats)
        {
            if (s.Name == statName)
            {
                return s;
            }
        }

        return null;
    }
}

[System.Serializable]
public class StatsUpgradeInfo
{
    public string Name;
    public int Level;
    public float[] UpgradeTime;
}

[System.Serializable]
public class MegaBombData
{
    public float Radius;
    public float Damage;
}

[System.Serializable]
public class ShieldData
{
    public float ShieldDuration;
}

[System.Serializable]
public class LaserData
{
    public float LaserDuration;
}
