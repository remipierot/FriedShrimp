using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCleared : MonoBehaviour
{
    public Text Kills;
    public Text Rescue;
    public Text Untouched;

	public Color Disabled;
	public Color Enabled;

	public Button ExitButton;

    private WaitForSeconds _Interval = new WaitForSeconds(.5f);

	private void OnEnable()
	{
		ExitButton.onClick.AddListener(BackToMenu);
		ExitButton.interactable = false;

		StartCoroutine(ShowAchievement());
	}

	IEnumerator ShowAchievement()
	{
        yield return _Interval;

		Kills.color = LevelManager.Instance.Medals.Kill ? Enabled : Disabled;

		yield return _Interval;

		Rescue.color = LevelManager.Instance.Medals.Rescue ? Enabled : Disabled;

		yield return _Interval;

		Untouched.color = LevelManager.Instance.Medals.Untouched ? Enabled : Disabled;

		yield return _Interval;

		ExitButton.interactable = true;
	}

	void BackToMenu()
	{
        SceneLoader.Instance.ChangeScene("Menu");
    }

}
