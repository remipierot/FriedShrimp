using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScreen : MonoBehaviour
{
	public float PopSpeed = .5f;
	public float FadeSpeed = .5f;

	private CanvasGroup _CanvasGroup;

	public event System.Action DoneChange = delegate { };

	private void Awake()
	{
		_CanvasGroup = GetComponent<CanvasGroup>();
	}

	public void Init(bool show)
	{
		_CanvasGroup.alpha = show ? 1f : 0f;
		Interactable(show);
	}

	public void Interactable(bool interactable)
	{
		_CanvasGroup.interactable = interactable;
		_CanvasGroup.blocksRaycasts = interactable;
	}

	public void Show()
	{
		_CanvasGroup.alpha = 0f;
		StartCoroutine(ModifyAlpha(1f, PopSpeed, () =>
		{
			Interactable(true);
		}));
	}

	public void Hide()
	{
		Interactable(false);
		StartCoroutine(ModifyAlpha(0f, PopSpeed));
	}

	IEnumerator ModifyAlpha(float alphaTarget, float speed, UnityAction callback = null)
	{
		while(!Mathf.Approximately(_CanvasGroup.alpha, alphaTarget))
		{
			_CanvasGroup.alpha = Mathf.Lerp(_CanvasGroup.alpha, alphaTarget, speed * Time.deltaTime);

			if (Mathf.Abs(_CanvasGroup.alpha - alphaTarget) < .1f)
				_CanvasGroup.alpha = alphaTarget;

			yield return null;
		}

		if (callback != null)
			callback.Invoke();

		DoneChange();
	}
}
