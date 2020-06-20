using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LockTransform))]
public class LockTransformEditor : Editor
{
	LockTransform Source;

	public override void OnInspectorGUI()
	{
		Source = (LockTransform)target;

		base.OnInspectorGUI();
		int lockLabelWidth = 110;
		int spaceOptionWidth = 100;
		int localPosition = Source.LockLocalPosition ? 1 : 0;
		int localRotation = Source.LockLocalRotation ? 1 : 0;
		string[] space = new string[] { "World", "Local" };

		GUILayout.BeginHorizontal();
		GUILayout.Label("Lock Position", GUILayout.Width(lockLabelWidth));
		Source.LockLocalPosition = EditorGUILayout.Popup("", localPosition, space, GUILayout.Width(spaceOptionWidth)) == 1;
		Source.LockPosition[0] = GUILayout.Toggle(Source.LockPosition[0], "X");
		Source.LockPosition[1] = GUILayout.Toggle(Source.LockPosition[1], "Y");
		Source.LockPosition[2] = GUILayout.Toggle(Source.LockPosition[2], "Z");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Lock Rotation", GUILayout.Width(lockLabelWidth));
		Source.LockLocalRotation = EditorGUILayout.Popup("", localRotation, space, GUILayout.Width(spaceOptionWidth)) == 1;
		Source.LockRotation[0] = GUILayout.Toggle(Source.LockRotation[0], "X");
		Source.LockRotation[1] = GUILayout.Toggle(Source.LockRotation[1], "Y");
		Source.LockRotation[2] = GUILayout.Toggle(Source.LockRotation[2], "Z");
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label("Lock Scale", GUILayout.Width(lockLabelWidth));
		GUILayout.Label(space[1], GUILayout.Width(spaceOptionWidth));
		Source.LockScale[0] = GUILayout.Toggle(Source.LockScale[0], "X");
		Source.LockScale[1] = GUILayout.Toggle(Source.LockScale[1], "Y");
		Source.LockScale[2] = GUILayout.Toggle(Source.LockScale[2], "Z");
		GUILayout.EndHorizontal();
	}
}