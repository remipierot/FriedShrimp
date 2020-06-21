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

        ItemLevelBar.value = _Stat.Level;

        BuyButton.onClick.AddListener(BuyUpgrade);
    } 

    public void BuyUpgrade()
	{
        if(StatsManager.Instance.Money >= PricesLevel[_Stat.Level] && !_IsUpgrading)
		{
            DialogManager.Instance.ShowDialog("Do you really want to upgrade " + StatName + "?", () =>
            {
                StatsManager.Instance.AddMoney(-PricesLevel[_Stat.Level]);
                StatsManager.Instance.StatsTimer.Add(StatName, DateTime.Now.AddMinutes(StatsManager.Instance.GetUpgradeTime(StatName)[_Stat.Level]));
                StartCoroutine(DoUpgrade());
            });
        }
        else
		{
            DialogManager.Instance.ShowMessage("Not enough money");
		}
	}

    public void UpdateItemDisplay()
	{
        _Stat = StatsManager.Instance.GetStats(StatName);

        ItemLevelBar.value = _Stat.Level;

        if(_Stat.Level == PricesLevel.Length)
		{
            BuyText.text = "MAX";
            return;
		}

        BuyText.text = PricesLevel[_Stat.Level].ToString();

        CheckForUpgradeStatus();
	}

    public void CheckForUpgradeStatus()
	{
        if (StatsManager.Instance.StatsTimer.ContainsKey(StatName))
		{
            if (DateTime.Now < StatsManager.Instance.StatsTimer[StatName])
			{
                StartCoroutine(DoUpgrade());
            }
            else
			{
                IncreaseStat();
			}
		}
	}

    IEnumerator DoUpgrade()
	{
        _IsUpgrading = true;
        BuyButton.interactable = false;

        TimeSpan remaining = StatsManager.Instance.StatsTimer[StatName] - DateTime.Now;

        while(remaining.TotalSeconds > 0f)
		{
            remaining = StatsManager.Instance.StatsTimer[StatName] - DateTime.Now;
            BuyText.text = string.Format("{0:00}:{1:00}", remaining.Minutes, remaining.Seconds);
            yield return null;
        }

        BuyButton.interactable = true;
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

        DialogManager.Instance.ShowMessage("Upgrade completed: " + StatName);
        BuyText.text = PricesLevel[_Stat.Level].ToString();
        ItemLevelBar.value = _Stat.Level;
        StatsManager.Instance.StatsTimer.Remove(StatName);
    }
}
