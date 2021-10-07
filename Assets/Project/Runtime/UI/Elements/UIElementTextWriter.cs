using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


namespace Project.Runtime.UI.Elements
{
	public class UIElementTextWriter : MonoBehaviour
	{

		TextMeshProUGUI txt;
		public float speed;
		string story;

		void Awake()
		{
			txt = GetComponent<TextMeshProUGUI>();
			story = txt.text;
			txt.text = "";

			// TODO: add optional delay when to start
			StartCoroutine("PlayText");
		}

		IEnumerator PlayText()
		{
			foreach (char c in story)
			{
				txt.text += c;
				yield return new WaitForSeconds(speed);

			}
		}

	}

}