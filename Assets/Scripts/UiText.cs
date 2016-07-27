using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// http://forum.unity3d.com/threads/ui-text-character-limit.359729/
/// http://forum.unity3d.com/threads/dynamic-scrolling-text-support.360026/
/// </summary>
public class UiText : MonoBehaviour
{
	public GameObject TextPrefab;
	public GameObject ContentParent;
	public int LinesCount = 200;

	private void Start()
	{
		OnePerLine();
	}

	public void OnePerLine()
	{
		DestroyChildren(ContentParent.transform);

		for (int i = 0; i <= LinesCount; i++) {
			GameObject newLine = InstantiateTextPrefab();
			newLine.GetComponent<Text>().text = GetLine(i);
			newLine.name = i.ToString();
		}
	}

	public void OnlyOneText()
	{
		DestroyChildren(ContentParent.transform);

		GameObject textObj = InstantiateTextPrefab();
		Destroy(textObj.GetComponent<LayoutElement>());
		Text textComponent = textObj.GetComponent<Text>();

		textComponent.text = "";
		for (int i = 0; i < LinesCount; i++) {
			textComponent.text += GetLine(i, true);
		}
		textComponent.text += GetLine(LinesCount);
	}

	private GameObject InstantiateTextPrefab()
	{
		GameObject gameObj = Instantiate(TextPrefab);
		gameObj.transform.SetParent(ContentParent.transform);
		gameObj.transform.localRotation = Quaternion.identity;
		gameObj.transform.localPosition = Vector3.zero;
		return gameObj;
	}

	private static readonly string[] loremIpsum = {
		"lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod",
		"tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua"
	};

	private string GetLine(int number, bool withEOL = false)
	{
		string result = number.ToString();
		for (int i = 0; i < loremIpsum.Length / 2; i++) {
			result += " " + loremIpsum[Random.Range(0, loremIpsum.Length)];
		}
		if (withEOL) result += "\n";
		return result;
	}

	/// <summary>
	/// Calls GameObject.Destroy on all children of transform. and immediately detaches the children
	/// from transform so after this call tranform.childCount is zero.
	/// </summary>
	public static void DestroyChildren(Transform transform)
	{
		for (int i = transform.childCount - 1; i >= 0; --i) {
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}
		transform.DetachChildren();
	}
}
