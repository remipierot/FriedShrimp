using UnityEngine;
using UnityEngine.Events;

public class DeathSystem : MonoBehaviour
{
	public bool ShouldBeDestroyed = true;
	public bool BackToPool = true;
	public float DestroyDelay;
	public CreateObject[] ObjectsToSpawn;
	public UnityEvent OnDeathEvent;

	private Collider[] _Colliders;

	private void Start()
	{
		_Colliders = GetComponents<Collider>();
	}

	public void Die()
	{
		foreach(var o in ObjectsToSpawn)
		{
			o.Create();
		}

		foreach (var c in _Colliders)
		{
			c.enabled = false;
		}

		OnDeathEvent.Invoke();

		if (ShouldBeDestroyed)
		{
			if (BackToPool)
				PoolingManager.Instance.ReturnObject(gameObject, DestroyDelay);
			else
				Destroy(gameObject, DestroyDelay);
		}
	}
}
