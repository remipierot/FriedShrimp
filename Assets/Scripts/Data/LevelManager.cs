using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Medals Medals = new Medals();
    public int TotalEnemy;
    public int EnemyKilled;
    public int TotalRescue;
    public int HumansRescued;
    public int Score;

    public UnityEvent OnEndGame;

    private string _Level;

	private void Awake()
	{
        if (Instance == null)
            Instance = this;

        Medals.Untouched = true;
        _Level = SceneManager.GetActiveScene().name;
    }

    public void RegisterEnemy()
	{
        TotalEnemy++;
	}

    public void RegisterRescue()
	{
        TotalRescue++;
	}

    public void AddEnemyKill(int score)
	{
        EnemyKilled++;
        Score += score;
        UpdateMoney.Instance.DisplayScore(Score);
    }

    public void AddRescue(int score)
	{
        HumansRescued++;
        Score += score;
        UpdateMoney.Instance.DisplayScore(Score);
    }

    public void PlayerHit()
	{
        Medals.Untouched = false;
	}

    public void EndGame(bool playerAlive)
	{
        StartCoroutine(CountDelay(playerAlive));
	}

    IEnumerator CountDelay(bool playerAlive)
    {
        yield return new WaitForSeconds(.25f);

        if (playerAlive)
		{
            if (EnemyKilled == TotalEnemy)
                Medals.Kill = true;
            if (HumansRescued == TotalRescue)
                Medals.Rescue = true;

            StatsManager.Instance.AddMedals(_Level, Medals);
            StatsManager.Instance.AddLevelCompleted(_Level);
            StatsManager.Instance.SaveProgress();
        }

        OnEndGame.Invoke();
    }
}

[System.Serializable]
public class Medals
{
    public bool Rescue;
    public bool Kill;
    public bool Untouched;
}
