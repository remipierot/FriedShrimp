using UnityEngine;

public class LinkToTransform : MonoBehaviour
{
	public Transform LinkedTo;
	public bool LinkPosition;
	public bool LinkRotation;

	private Vector3 _InitialPositionOffset;
	private Quaternion _InitialRotationOffset;

	private void Awake()
	{
		_InitialPositionOffset = transform.position - LinkedTo.position;
		_InitialRotationOffset = Quaternion.Inverse(LinkedTo.rotation) * transform.rotation;
	}

	void Update()
	{
		if(LinkedTo.hasChanged)
		{
			if (LinkPosition)
				transform.position = LinkedTo.position + _InitialPositionOffset;

			if (LinkRotation)
				transform.rotation = LinkedTo.rotation * _InitialRotationOffset;
		}
	}
}
