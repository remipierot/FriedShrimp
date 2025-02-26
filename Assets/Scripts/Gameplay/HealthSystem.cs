﻿using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private float _MaxHealth = 10f;
    public bool IsEnemy = true;
    public GameObject HitEffect;
    public GameObject HealthBar;
    public int MinScore = 25;
    public int MaxScore = 50;

    private string _TagName = "Bullet";
    private float _CurrentHealth;
    private DeathSystem _Death;

    void OnEnable()
    {
        if (IsEnemy)
		{
            LevelManager.Instance.RegisterEnemy();
            gameObject.tag = "Enemy";
            _TagName = "Bullet";
		}
        else
		{
            _TagName = "EnemyBullet";
            _MaxHealth = StatsManager.Instance.GetStatsValue("Health", StatsManager.Instance.HealthUpgrades);
		}

        _CurrentHealth = _MaxHealth;
    }

	private void Start()
	{
        _Death = GetComponent<DeathSystem>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(_TagName) && this.enabled)
		{
            if (!IsEnemy)
			{
                LevelManager.Instance.PlayerHit();
			}

            var triggerPosition = other.ClosestPointOnBounds(transform.position);
            var direction = triggerPosition - transform.position;

            GameObject fx = PoolingManager.Instance.UseObject(HitEffect, triggerPosition, Quaternion.LookRotation(direction));
            PoolingManager.Instance.ReturnObject(fx, 1f);

            TakeDamage(other.GetComponent<Bullet>().Damage);
            PoolingManager.Instance.ReturnObject(other.gameObject);
		}
	}

    public void TakeDamage(float damage)
	{
        _CurrentHealth -= damage;
        _CheckHealth();
        _UpdateUI();
    }

    private void _CheckHealth()
	{
        if (_CurrentHealth <= 0f)
		{
            if(HealthBar != null)
                HealthBar.transform.parent.gameObject.SetActive(false);

            if (_Death != null)
                _Death.Die();

            //Prevent listing by FindGameObjectsWithTag when looking for closest enemy
            if(IsEnemy)
			{
                gameObject.tag = "Untagged";
                LevelManager.Instance.AddEnemyKill(Random.Range(MinScore, MaxScore));
			}
		}
	}

    private void _UpdateUI()
	{
        if(HealthBar != null)
		{
            Vector3 scale = Vector3.one;
            float value = _CurrentHealth / _MaxHealth;
            scale.x = value;
            HealthBar.transform.localScale = scale;
		}
	}
}
