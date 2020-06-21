using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateMoney : MonoBehaviour
{
    public static UpdateMoney Instance;

	public Text Money;

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
		Money.text = "$ " + value.ToString();
	}
}
