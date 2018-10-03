using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour {
	[SerializeField]
	public static int number_Enemies;
	public bool cheat = false;
	GameObject[] enemies;

	private void Start()
	{
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		number_Enemies = enemies.Length;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player" && (number_Enemies <= 0)|| cheat)
		{
			HUDManager.Instance.Win();
		}
	}

}
