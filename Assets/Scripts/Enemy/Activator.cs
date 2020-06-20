using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Activator : MonoBehaviour
{
	public UnityEvent OnEnterScreen;
	public UnityEvent OnExitScreen;

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Activator"))
		{
			OnEnterScreen.Invoke();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Deactivator"))
		{
			OnExitScreen.Invoke();
		}
	}
}
