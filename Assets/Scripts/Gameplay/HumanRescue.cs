using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HumanRescue : MonoBehaviour
{
	public float RescueTime = 5f;
	public Image TimerUI;
	public UnityEvent OnRescued;

	private GameObject _Player;

	private void Start()
	{
		LevelManager.Instance.RegisterRescue();
		_Player = GameObject.FindGameObjectWithTag("Player");
	}

	IEnumerator Rescuing(float countdown)
	{
		while (countdown > 0f)
		{
			countdown -= Time.deltaTime;
			UpdateUI(countdown);

			if (_Player == null)
			{
				StopAllCoroutines();
				TimerUI.fillAmount = 0;
			}

			yield return null;
		}

		LevelManager.Instance.AddRescue();
		OnRescued.Invoke();
		Destroy(gameObject, 1.5f);
	}
	
	void UpdateUI(float countdown)
	{
		if (TimerUI != null)
		{
			float value = 1f - (countdown / RescueTime);
			TimerUI.fillAmount = value;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			StartCoroutine(Rescuing(RescueTime));
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			StopAllCoroutines();
			TimerUI.fillAmount = 0f;
		}
	}
}
