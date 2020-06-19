using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeMove))]
public class NodeMoveEditor : Editor
{
	NodeMove Source;

	public override void OnInspectorGUI()
	{
		Source = (NodeMove)target;

		base.OnInspectorGUI();

		if (GUILayout.Button("Add Node"))
		{
			Source.Nodes.Add(Source.transform.position);
		}

		if (GUILayout.Button("Remove Node") && Source.Nodes.Count > 0)
		{
			Source.Nodes.RemoveAt(Source.Nodes.Count - 1);
		}
	}

	private void OnSceneGUI()
	{
		Source = (NodeMove)target;

		for(int i = 0; i < Source.Nodes.Count; i++)
		{
			Source.Nodes[i] = Handles.PositionHandle(Source.Nodes[i], Quaternion.identity);
			Handles.Label(Source.Nodes[i], "Node " + (i + 1));
		}
	}
}
