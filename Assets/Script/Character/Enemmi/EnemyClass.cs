using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : CharacterObj{

	public GameObject player = null;
	public bool follow_player;
	public GameObject barlife;

	public override void die()
	{
		EndScript.number_Enemies--;
		destroy();
	}

	void Update()
	{
		barlife.transform.localScale = new Vector3(ratio(), barlife.transform.localScale.y, barlife.transform.localScale.z);
		if (follow_player && player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		else if (follow_player)
		{
			player.SendMessage("takeDamage", damage);
		}
	}
}
