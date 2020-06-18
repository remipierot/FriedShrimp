using System.Collections;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
	public ShootProfile Profile;
	public GameObject BulletPrefab;
	public Transform PopPoint;

	private float _TotalSpread;
	private WaitForSeconds _Rate;
	private WaitForSeconds _Interval;

	// Start is called before the first frame update
	void Start()
	{
		_Interval = new WaitForSeconds(Profile.Interval);
		_Rate = new WaitForSeconds(Profile.FireRate);

		if (PopPoint == null)
			PopPoint = transform;

		_TotalSpread = Profile.Spread * Profile.Amount;

		StartCoroutine(ShootingSequence());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	IEnumerator ShootingSequence()
	{
		while (true)
		{
			float angle = 0f;

			if (Profile.Amount > 1)
			{
				for (int i = 0; i < Profile.Amount; i++)
				{
					angle = _TotalSpread * (i / (float)Profile.Amount);
					angle -= (_TotalSpread / 2f) - Profile.Spread / Profile.Amount;

					_Shoot(angle);

					if (Profile.FireRate > 0f)
						yield return _Rate;
				}
			}

			yield return _Interval;
		}
	}

	private void _Shoot(float angle)
	{
		if(PoolingManager.Instance != null)
		{
			var tmp = PoolingManager.Instance.UseObject(BulletPrefab, PopPoint.position, Quaternion.identity);
			tmp.transform.Rotate(Vector3.up, angle);
			tmp.GetComponent<Bullet>().Damage = Profile.Damage;
		}
	}
}
