using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Medals Medals = new Medals();
    public int TotalEnemy;
    public int EnemyKilled;
    public int TotalRescue;
    public int HumansRescued;

    public UnityEvent OnEndGame;

	private void Awake()
	{
        if (Instance == null)
            Instance = this;

        Medals.Untouched = true;
    }

    public void RegisterEnemy()
	{
        TotalEnemy++;
	}

    public void RegisterRescue()
	{
        TotalRescue++;
	}

    public void AddEnemyKill()
	{
        EnemyKilled++;
	}

    public void AddRescue()
	{
        HumansRescued++;
	}

    public void PlayerHit()
	{
        Medals.Untouched = false;
	}

    public void EndGame()
	{
        StartCoroutine(CountDelay());
	}

    IEnumerator CountDelay()
    {
        yield return new WaitForSeconds(.25f);

        if (EnemyKilled == TotalEnemy)
            Medals.Kill = true;
        if (HumansRescued == TotalRescue)
            Medals.Rescue = true;

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
