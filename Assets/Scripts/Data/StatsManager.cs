﻿using System.Collections;
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
    public Dictionary<string, bool> LevelCompleted = new Dictionary<string, bool>();
    public Dictionary<string, DateTime> StatsTimer = new Dictionary<string, DateTime>();

    [Header("Upgrade Timers")]
    public List<StatsUpgradeInfo> Stats = new List<StatsUpgradeInfo>();

	private void Start()
	{
        LoadProgress();
	}

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

        UpdateMoney.Instance.DisplayMoney(Money);
	}

    public T GetStatsValue<T>(string statName, List<T> statsList)
	{
		foreach (var s in Stats)
		{
            if(s.Name == statName)
            { 
                return statsList[s.Level];
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

    public void SaveProgress()
	{
        SaveData toSave = new SaveData();
        toSave.Lives = Lives;
        toSave.Money = Money;
        toSave.Achievements = Achievements;
        toSave.LevelCompleted = LevelCompleted;
        toSave.Stats = Stats;
        toSave.StatsTimer = StatsTimer;

        SaveSystem.Save(toSave);
	}

    public void LoadProgress()
	{
        SaveData toLoad = SaveSystem.Load<SaveData>();

        if (SaveSystem.LastResult == SaveSystem.Result.FileFound)
        {
            Lives = toLoad.Lives;
            Money = toLoad.Money;
            Achievements = toLoad.Achievements ?? Achievements;
            LevelCompleted = toLoad.LevelCompleted ?? LevelCompleted;
            Stats = toLoad.Stats ?? Stats;
            StatsTimer = toLoad.StatsTimer ?? StatsTimer;
        }

        UpdateItemDisplay();
	}

    public void UpdateItemDisplay()
	{
        UpgradeItem[] items = FindObjectsOfType<UpgradeItem>();
        LevelMenu[] levelMenus = FindObjectsOfType<LevelMenu>();

		foreach (var i in items)
		{
            i.UpdateItemDisplay();
		}

		foreach (var l in levelMenus)
		{
            l.UpdateMenu();
		}
	}

    public void AddMedals(string level, Medals medals)
	{
        if (Achievements.ContainsKey(level))
		{
            Achievements[level].Kill |= medals.Kill;
            Achievements[level].Rescue |= medals.Rescue;
            Achievements[level].Untouched |= medals.Untouched;
        }
        else
            Achievements.Add(level, medals);
	}

    public void AddLevelCompleted(string level)
    {
        if (Achievements.ContainsKey(level))
            LevelCompleted[level] = true;
        else
            LevelCompleted.Add(level, true);
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

[System.Serializable]
public class SaveData
{
    public int Lives;
    public int Money;
    public Dictionary<string, Medals> Achievements = new Dictionary<string, Medals>();
    public Dictionary<string, bool> LevelCompleted = new Dictionary<string, bool>();
    public Dictionary<string, DateTime> StatsTimer = new Dictionary<string, DateTime>();
    public List<StatsUpgradeInfo> Stats = new List<StatsUpgradeInfo>();
}
