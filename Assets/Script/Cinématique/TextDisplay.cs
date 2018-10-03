using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextDisplay : SingletonBehaviour<TextDisplay>
{
	public Text dialogueText;

	void Start ()
	{
		dialogueText.text = "";
		dialogueText.color = Color.green;
	}

	void Update()
	{
		if (Input.anyKeyDown)
			EndDialogue();
	}

	public void startDialogue(Dialogue dialogue)
	{
		dialogueText.text = "";
		dialogueText.color = Color.green;
		StartCoroutine(DisplaySentences(dialogue));
	}

	IEnumerator DisplaySentences(Dialogue dialogue)
	{
		foreach(string sentence in dialogue.sentences)
		{
			foreach (char c in sentence)
			{
				dialogueText.text += c;
				float waitc = Random.Range(0.02f, 0.06f);
				MusicManager.Instance.playNoise(1);
				yield return new WaitForSeconds(waitc);
			}
			float waits = Random.Range(1f, 2f);
			yield return new WaitForSeconds(waits);
			dialogueText.text += '\n';
		}
	}

	void EndDialogue()
	{
		StopAllCoroutines();
		dialogueText.text = "";
		return;
	}
}
