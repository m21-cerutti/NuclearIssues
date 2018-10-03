using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRocket : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>().newRocket();
			Destroy(gameObject);
		}
	}
}
