using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour
{
    public static UpdateMoney Instance;

	public Text Money;
	public Text Score;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		DisplayMoney(StatsManager.Instance.Money);
	}

	public void DisplayMoney(int value)
	{
		if (Money)
			Money.text = "$ " + value.ToString();
	}

	public void DisplayScore(int value)
	{
		if (Score)
			Score.text = value.ToString("00000000");
	}
}
