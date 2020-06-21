using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public string Scene;
    public Button Play;
    public Text Kills;
    public Text Rescue;
    public Text Untouched;

    public Color Disabled;
    public Color Enabled;

    private Medals _LevelMedals;

    void Start()
    {
        if(StatsManager.Instance.Achievements.ContainsKey(Scene))
            _LevelMedals = StatsManager.Instance.Achievements[Scene];

        Play.onClick.AddListener(GoToLevel);
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
	}

    void GoToLevel()
	{
        SceneLoader.Instance.ChangeScene(Scene);
	}
}
