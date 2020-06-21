using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystemDebug : MonoBehaviour
{
    public void Save()
	{
		StatsManager.Instance.SaveProgress();
	}

	public void Load()
	{
		StatsManager.Instance.LoadProgress();
	}
}
