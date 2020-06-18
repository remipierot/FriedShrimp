using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject ObjToCreate;
    public int Count = 1;

    [Header("Auto-Destroy Properties")]
    public bool ShouldBeDestroyed;
    public float DestroyDelay;

    private Vector3 _Position;

    public void Create()
	{
        _Position = transform.position;
        _Position.y = 0f;

        for(int i = 0; i < Count; i++)
		{
            var tmp = PoolingManager.Instance.UseObject(ObjToCreate, _Position, Quaternion.identity);

            if (ShouldBeDestroyed)
                PoolingManager.Instance.ReturnObject(tmp, DestroyDelay);
		}
	}
}
