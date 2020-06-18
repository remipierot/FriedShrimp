using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMove : MonoBehaviour
{
    public float Speed = 2f;
    public float RotateSpeed = 2f;
    public float BankingValue = 5f;

    public bool RotateObject;
    public bool LoopMovement;

    public int LoopToNode;

    public List<Vector3> Nodes = new List<Vector3>();

    private const int CURVE_SEGMENT = 20;
    private int _RealLoopNode;

	public List<Vector3> GetCurveNodes()
	{
        var curveNodes = new List<Vector3>();
        curveNodes.Add(transform.position);

        for(int i = 0; i < Nodes.Count - 3; i+=3)
		{
            var p0 = Nodes[i];
            var p1 = Nodes[i + 1];
            var p2 = Nodes[i + 2];
            var p3 = Nodes[i + 3];

            for(int j = 0; j < CURVE_SEGMENT; j++)
			{
                curveNodes.Add(_NextBezierPoint(p0, p1, p2, p3, j / (float)CURVE_SEGMENT));
			}
		}

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
        List<Vector3> curveNodes = new List<Vector3>();
        curveNodes = GetCurveNodes();

        for (int i = 0; i < curveNodes.Count-1; i++)
		{
            Gizmos.color = Color.green;
            Gizmos.DrawLine(curveNodes[i], curveNodes[i + 1]);
		}

        for (int i = 0; i < Nodes.Count-1; i++)
        {
            var c = Color.red;
            c.a = .5f;
            Gizmos.color = c;
            Gizmos.DrawLine(Nodes[i], Nodes[i + 1]);
        }
    }
}
