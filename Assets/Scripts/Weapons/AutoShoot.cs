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
		if (PopPoint == null)
			PopPoint = transform;

		Init();

		StartCoroutine(ShootingSequence());
	}

	public void Init()
	{
		_Interval = new WaitForSeconds(Profile.Interval);
		_Rate = new WaitForSeconds(Profile.FireRate);
		_TotalSpread = Profile.Spread * Profile.Amount;
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	public void SwitchProfile(ShootProfile newProfile)
	{
		Profile = newProfile;
		Init();
	}

	IEnumerator ShootingSequence()
	{
		while (true)
		{
			float angle = 0f;

			for (int i = 0; i < Profile.Amount; i++)
			{
				angle = _TotalSpread * (i / (float)Profile.Amount);
				angle -= (_TotalSpread / 2f) - Profile.Spread / Profile.Amount;

				_Shoot(angle);

				if (Profile.FireRate > 0f)
					yield return _Rate;
			}

			yield return _Interval;
		}
	}

	private void _Shoot(float angle)
	{
		if (!PoolingManager.Instance)
			return;

		var tmp = PoolingManager.Instance.UseObject(BulletPrefab, PopPoint.position, Quaternion.identity);
		tmp.transform.Rotate(Vector3.up, PopPoint.transform.rotation.eulerAngles.y + angle);
		var bullet = tmp.GetComponent<Bullet>();

		if (!bullet)
			return;

		bullet.Damage = Profile.Damage;
		bullet.Speed = Profile.Speed;
		PoolingManager.Instance.ReturnObject(tmp, Profile.DestroyRate);
	}
}
