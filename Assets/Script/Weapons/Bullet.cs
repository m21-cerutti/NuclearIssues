using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

	public int damage;
	public bool isRocket;
	public float radius;
	public GameObject Explosion;
	private object yield;
	Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		if (Explosion != null)
		{
			Explosion.transform.localScale = Explosion.transform.localScale * radius;
		}
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		foreach (ContactPoint2D con in other.contacts)
		{
			if (isRocket)
			{
				explosiveDamage(other.collider, damage);
				StartCoroutine(Explose(3f));
				break;
			}
			else {
				if (other.collider.tag == "Enemy")
				{
					if (other.collider.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
					{
						other.collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					}
					bulletDamage(other.collider, damage);
				}
				Destroy(gameObject);
			}
		}
		

	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	void bulletDamage(Collider2D other, int damage)
	{
		if(other.tag == "Enemy")
			other.SendMessage("takeDamage", damage);
	}

	void explosiveDamage(Collider2D other, int damage)
	{
		Collider2D[] colliders = new Collider2D[20];
		LayerMask layerMask = 1 << 8 | 1 << 10;
		Physics2D.OverlapCircleNonAlloc(transform.position, radius, colliders, layerMask);

		foreach (Collider2D entity in colliders)
		{
			if (entity != null && (entity.tag == "Enemy" || entity.tag == "Player"))
			{
				Debug.Log(entity.name);
				entity.SendMessage("takeDamage", damage);
				Debug.Log(entity.name + " has taken damage !");
			}
		}
	}

	IEnumerator Explose(float time)
	{
		if (rb != null)
			rb.simulated = false;
		Explosion.SetActive(true);
		MusicManager.Instance.playNoise_gun(5);
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}


}
