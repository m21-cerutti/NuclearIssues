using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ATHScript : MonoBehaviour
{
	public GameObject player;
	public RectTransform FillBoss;
	public Text nbRockets;
	float previous_life = 1f;

	void Start()
	{
		nbRockets.text = "";
	}

	void FixedUpdate()
	{
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		else
		{
			previous_life = player.GetComponent<PlayerMotor>().ratio();
			//Initialisation Life
			FillBoss.localScale = new Vector3(previous_life, FillBoss.localScale.y, FillBoss.localScale.z);
			//Update Life
			float life_boss = player.GetComponent<PlayerMotor>().ratio();
			life_boss = Mathf.Clamp01(life_boss);
			if (previous_life != life_boss)

			{
				FillBoss.localScale = new Vector3(life_boss, FillBoss.localScale.y, FillBoss.localScale.z);
				previous_life = life_boss;
			}
			nbRockets.text = player.GetComponent<PlayerShoot>().getNbRockets().ToString();
		}
	}
}
