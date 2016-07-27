using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(UiText))]
public class UiTextEditor : Editor
{
	UiText uiText;

	private void OnEnable()
	{
		uiText = (UiText)target;
	}

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GUILayout.BeginHorizontal();

		if (GUILayout.Button(new GUIContent("Only One Text"), EditorStyles.miniButton)) {
			CheckPlayMode();
			uiText.OnlyOneText();
		}

		if (GUILayout.Button(new GUIContent("One Per Line"), EditorStyles.miniButton)) {
			CheckPlayMode();
			uiText.OnePerLine();
		}

		GUILayout.EndHorizontal();

		EditorGUILayout.HelpBox("Try them both and compare Tris & Verts in the Stats :)", MessageType.Info);
	}

	private void CheckPlayMode()
	{
		if (!EditorApplication.isPlaying) {
			throw new System.InvalidOperationException("Play mode required...");
		}
	}
}
