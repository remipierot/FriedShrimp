using UnityEngine;

public class PoolingManager : MonoBehaviour
{
	public static PoolingManager Instance;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}

	public GameObject UseObject(GameObject obj, Vector3 pos, Quaternion rot)
	{
		var tmp = Instantiate(obj, pos, rot);
		tmp.SetActive(true);
		return tmp;
	}

	public void ReturnObject(GameObject obj, float delay = 0f)
	{
		Destroy(obj, delay);
	}
}
