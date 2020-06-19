using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
	public static PoolingManager Instance;

	public PoolItem[] PoolItems;

	private Dictionary<int, Queue<GameObject>> _PoolQueue = new Dictionary<int, Queue<GameObject>>();
	private Dictionary<int, bool> _GrowableBool = new Dictionary<int, bool>();
	private Dictionary<int, Transform> _Parents = new Dictionary<int, Transform>();

	private void Awake()
	{
		if (Instance == null)
			Instance = this;

		PoolInit();
	}

	void PoolInit()
	{
		GameObject poolGroup = new GameObject("Pool Groups");

		for(int i = 0; i < PoolItems.Length; i++)
		{
			GameObject uniquePool = new GameObject(PoolItems[i].PoolObject.name + " Group");
			uniquePool.transform.SetParent(poolGroup.transform);

			int objID = PoolItems[i].PoolObject.GetInstanceID();
			PoolItems[i].PoolObject.SetActive(false);

			_PoolQueue.Add(objID, new Queue<GameObject>());
			_GrowableBool.Add(objID, PoolItems[i].Growable);
			_Parents.Add(objID, uniquePool.transform);

			for(int j = 0; j < PoolItems[i].PoolAmount; j++)
			{
				var tmp = Instantiate(PoolItems[i].PoolObject, uniquePool.transform);
				_PoolQueue[objID].Enqueue(tmp);
			}
		}
	}

	public GameObject UseObject(GameObject obj, Vector3 pos, Quaternion rot)
	{
		int objID = obj.GetInstanceID();

		var tmp = _PoolQueue[objID].Dequeue();

		if(tmp.activeInHierarchy)
		{
			if(_GrowableBool[objID])
			{
				_PoolQueue[objID].Enqueue(tmp);
				tmp = Instantiate(obj, _Parents[objID]);
				tmp.transform.position = pos;
				tmp.transform.rotation = rot;
			}
			else
			{
				tmp = null;
			}
		}
		else
		{
			tmp.transform.position = pos;
			tmp.transform.rotation = rot;
		}

		tmp?.SetActive(true);
		_PoolQueue[objID].Enqueue(tmp);
		return tmp;
	}

	public void ReturnObject(GameObject obj, float delay = 0f)
	{
		if(delay == 0f)
		{
			obj.SetActive(false);
		}
		else
		{
			StartCoroutine(DelayReturn(obj, delay));
		}
	}

	IEnumerator DelayReturn(GameObject obj, float delay = 0f)
	{
		while (delay > 0f)
		{
			delay -= Time.deltaTime;
			yield return null;
		}

		obj.SetActive(false);
	}
}

[System.Serializable]
public class PoolItem
{
	public GameObject PoolObject;
	public int PoolAmount;
	public bool Growable;
}
