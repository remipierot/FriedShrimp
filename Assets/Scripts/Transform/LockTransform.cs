using UnityEngine;

public class LockTransform : MonoBehaviour
{
	[HideInInspector]
	public bool[] LockPosition = new bool[3];
	[HideInInspector]
	public bool LockLocalPosition = false;
	[HideInInspector]
	public bool[] LockRotation = new bool[3];
	[HideInInspector]
	public bool LockLocalRotation = false;
	[HideInInspector]
	public bool[] LockScale = new bool[3];

	private Vector3 _InitialPosition;
	private Vector3 _InitialLocalPosition;
	private Quaternion _InitialRotation;
	private Quaternion _InitialLocalRotation;
	private Vector3 _InitialLocalScale;

	private void Awake()
	{
		_InitialPosition = transform.position;
		_InitialLocalPosition = transform.localPosition;
		_InitialRotation = transform.rotation;
		_InitialLocalRotation = transform.localRotation;
		_InitialLocalScale = transform.localScale;
	}

	private Vector3 _GetLockedVector3(Quaternion current, Quaternion initial, bool[] isLocked)
	{
		return _GetLockedVector3(current.eulerAngles, initial.eulerAngles, isLocked);
	}

	private Vector3 _GetLockedVector3(Vector3 current, Vector3 initial, bool[] isLocked)
	{
		Vector3 back = current;

		if (isLocked[0] && !Mathf.Approximately(current.x, initial.x))
			back.x = initial.x;
		if (isLocked[1] && !Mathf.Approximately(current.y, initial.y))
			back.y = initial.y;
		if (isLocked[2] && !Mathf.Approximately(current.z, initial.z))
			back.z = initial.z;

		return back;
	}

	void Update()
    {
		if (transform.hasChanged)
		{
			if (LockLocalPosition)
				transform.localPosition = _GetLockedVector3(transform.localPosition, _InitialLocalPosition, LockPosition);
			else
				transform.position = _GetLockedVector3(transform.position, _InitialPosition, LockPosition);

			if (LockLocalRotation)
				transform.localRotation = Quaternion.Euler(_GetLockedVector3(transform.localRotation, _InitialLocalRotation, LockRotation));
			else
				transform.rotation = Quaternion.Euler(_GetLockedVector3(transform.rotation, _InitialRotation, LockRotation));

			transform.localScale = _GetLockedVector3(transform.localScale, _InitialLocalScale, LockScale);
		}
    }
}
