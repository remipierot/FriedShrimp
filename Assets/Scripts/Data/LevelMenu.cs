using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public string Scene;
    public string RequiredLevel;
    public Button Play;
    public Text Kills;
    public Text Rescue;
    public Text Untouched;

    public Color Disabled;
    public Color Enabled;

    private Medals _LevelMedals;

    void Start()
    {
        Play.onClick.AddListener(GoToLevel);
        Play.GetComponentInChildren<Text>().text = (transform.GetSiblingIndex() + 1).ToString("00");
        UpdateMenu();
    }

    public void UpdateMenu()
	{
        if (StatsManager.Instance.Achievements.ContainsKey(Scene))
            _LevelMedals = StatsManager.Instance.Achievements[Scene];

        Kills.color = Disabled;
        Rescue.color = Disabled;
        Untouched.color = Disabled;

        if (_LevelMedals != null)
		{
            Kills.color = _LevelMedals.Kill ? Enabled : Disabled;
            Rescue.color = _LevelMedals.Rescue ? Enabled : Disabled;
            Untouched.color = _LevelMedals.Untouched ? Enabled : Disabled;
        }

        CheckIfUnlocked();
	}

    void CheckIfUnlocked()
	{
        bool unlocked = StatsManager.Instance.LevelCompleted.ContainsKey(RequiredLevel) &&
                        StatsManager.Instance.LevelCompleted[RequiredLevel];

        Play.interactable = unlocked || string.IsNullOrEmpty(RequiredLevel);
	}

    void GoToLevel()
	{
        SceneLoader.Instance.ChangeScene(Scene);
	}
}
