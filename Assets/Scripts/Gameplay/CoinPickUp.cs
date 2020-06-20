using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPickUp : MonoBehaviour
{
	public UnityEvent OnPickUp;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Coin"))
		{
			PoolingManager.Instance.ReturnObject(other.gameObject);
			OnPickUp.Invoke();
		}
	}
}
