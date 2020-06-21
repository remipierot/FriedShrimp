using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour
{
	public static DialogManager Instance;

	public GameObject Panel;

	public Text Message;
	public Text Yes;
	public Text No;

	public Button YButton;
	public Button NButton;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		Panel.SetActive(false);
	}

	public void ShowDialog(string message, UnityAction yAction, UnityAction nAction = null, string yText = "Yes", string nText = "No")
	{
		NButton.gameObject.SetActive(true);

		Message.text = message;
		Yes.text = yText;
		No.text = nText;

		YButton.onClick.RemoveAllListeners();
		NButton.onClick.RemoveAllListeners();

		if(nAction != null)
		{
			NButton.onClick.AddListener(nAction);
		}

		NButton.onClick.AddListener(DisablePanel);

		YButton.onClick.AddListener(yAction);
		YButton.onClick.AddListener(DisablePanel);

		Panel.SetActive(true);
	}

	public void ShowMessage(string message)
	{
		NButton.gameObject.SetActive(false);

		Message.text = message;
		Yes.text = "OK";

		YButton.onClick.RemoveAllListeners();

		YButton.onClick.AddListener(DisablePanel);

		Panel.SetActive(true);
	}

	void DisablePanel()
	{
		Panel.SetActive(false);
	}
}
