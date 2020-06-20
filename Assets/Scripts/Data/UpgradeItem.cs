using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpgradeItem : MonoBehaviour
{
    [Header("Upgrade Menu Objects")]
    public string StatName;
    public string ItemName;
    public Text ItemNameText;
    public Text BuyText;
    public Slider ItemLevelBar;
    public Button BuyButton;

    [Header("Item Prices Setup")]
    public int[] PricesLevel;

    private StatsUpgradeInfo _Stat;
    private bool _IsUpgrading;
    
    void Start()
    {
        _Stat = StatsManager.Instance.GetStats(StatName);

        ItemNameText.text = ItemName;

        BuyText.text = PricesLevel[_Stat.Level].ToString();

        BuyButton.onClick.AddListener(BuyUpgrade);
    }

    public void BuyUpgrade()
	{
        if(StatsManager.Instance.Money >= PricesLevel[_Stat.Level])
		{
            StatsManager.Instance.AddMoney(-PricesLevel[_Stat.Level]);
            StatsManager.Instance.StatsTimer.Add(StatName, DateTime.Now.AddMinutes(StatsManager.Instance.GetUpgradeTime(StatName)[_Stat.Level]));
            StartCoroutine(DoUpgrade());
        }
        else
		{

		}
	}

    public void CheckForUpgradeStatus()
	{

	}

    IEnumerator DoUpgrade()
	{
        _IsUpgrading = true;

        TimeSpan remaining = StatsManager.Instance.StatsTimer[StatName] - DateTime.Now;

        while(remaining.TotalSeconds > 0f)
		{
            remaining = StatsManager.Instance.StatsTimer[StatName] - DateTime.Now;
            BuyText.text = string.Format("{0:00}:{1:00}", remaining.Minutes, remaining.Seconds);
            yield return null;
        }

        _IsUpgrading = false;

        IncreaseStat();
	}

    void IncreaseStat()
	{
        _Stat.Level++;

        if(_IsUpgrading)
		{
            StopAllCoroutines();
            _IsUpgrading = false;
		}
	}
}
