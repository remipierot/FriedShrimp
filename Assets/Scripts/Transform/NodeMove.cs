using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMove : MonoBehaviour
{
	public float Speed = 2f;
	public float RotateSpeed = 2f;
	public float BankingValue = 5f;

	public bool ShouldRotate;
	public bool LoopMove;

	public int LoopToNode;

	public List<Vector3> Nodes = new List<Vector3>();

	private const int CURVE_SEGMENT = 20;
	private int _RealLoopNode;
	private Transform _Parent;
	private float _NextAngleGrab;

	private Quaternion _Rotation;

	private void OnEnable()
	{
		_Parent = transform.parent != null ? transform.parent : transform;
		StartCoroutine(_StartMove());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator _StartMove()
	{
		float oldAngle = 0f;
		int posID = 0;
		var path = GetCurveNodes();

		while (LoopMove || posID < path.Count - 1)
		{
			if((path[posID] - transform.localPosition).sqrMagnitude < .01f)
			{
				if (posID < path.Count - 1)
				{
					posID++;
				}
				else if (LoopMove)
				{
					posID = _RealLoopNode;
				}
			}

			transform.localPosition = Vector3.MoveTowards(transform.localPosition, path[posID], Speed * Time.deltaTime);

			if (ShouldRotate)
			{
				if (Time.time > _NextAngleGrab)
				{
					_NextAngleGrab = Time.time + .5f;

					var direction = path[posID] - transform.localPosition;

					if (direction.sqrMagnitude > .01f)
					{
						_Rotation = Quaternion.LookRotation(direction, Vector3.up);
					}

					float zBank = Mathf.Clamp(_Rotation.eulerAngles.y - oldAngle, -10f, 10f);
					var banking = Quaternion.Euler(Vector3.forward * Mathf.Ceil(zBank) * -BankingValue);

					_Rotation *= banking;

					oldAngle = _Rotation.eulerAngles.y;
				}

				transform.rotation = Quaternion.Slerp(transform.rotation, _Rotation, RotateSpeed * Time.deltaTime);
			}

			yield return null;
		}
	}

	public List<Vector3> GetCurveNodes()
	{
		_Parent = transform.parent != null ? transform.parent : transform;

		var curveNodes = new List<Vector3>();

		for(int i = 0; i < Nodes.Count - 3; i+=3)
		{
			var p0 = _Parent.InverseTransformPoint(Nodes[i]);
			var p1 = _Parent.InverseTransformPoint(Nodes[i + 1]);
			var p2 = _Parent.InverseTransformPoint(Nodes[i + 2]);
			var p3 = _Parent.InverseTransformPoint(Nodes[i + 3]);

			if(i == 0)
			{
				p0 = _Parent.InverseTransformPoint(transform.position);
			}

			for(int j = 0; j <= CURVE_SEGMENT; j++)
			{
				curveNodes.Add(_NextBezierPoint(p0, p1, p2, p3, j / (float)CURVE_SEGMENT));
			}
		}

		_RealLoopNode = (int)(curveNodes.Count * (Mathf.Clamp(LoopToNode, 0, Nodes.Count) / (float)Nodes.Count));
		_RealLoopNode = Mathf.Clamp(_RealLoopNode, 0, curveNodes.Count - 1);

		return curveNodes;
	}

	private Vector3 _NextBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float minT = 1f - t;
		var point = minT * minT * minT * p0 +
					3f * minT * minT * t * p1 +
					3f * minT * t * t * p2 +
					t * t * t * p3;

		return point;
	}

	private void OnDrawGizmosSelected()
	{
		var curveNodes = GetCurveNodes();

		for (int i = 0; i < curveNodes.Count-1; i++)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawLine(_Parent.TransformPoint(curveNodes[i]), _Parent.TransformPoint(curveNodes[i + 1]));
		}

		var c = Color.red;
		c.a = .5f;
		Gizmos.color = c;

		if(Nodes.Count > 0)
			Gizmos.DrawLine(transform.position, Nodes[0]);

		for (int i = 0; i < Nodes.Count-1; i++)
		{
			Gizmos.DrawLine(Nodes[i], Nodes[i + 1]);
		}
	}
}
