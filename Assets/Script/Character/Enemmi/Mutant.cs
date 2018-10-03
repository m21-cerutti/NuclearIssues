using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : EnemyClass{

	
	public Vector3 startPosition;
	public float speed;
	public GameObject itemRocket;

	Rigidbody2D rb;


	private Vector3 velocity = Vector3.zero;
	

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		startPosition = this.transform.position;
	}

	public override void die()
	{
		base.die();
		if(Random.Range(0, 6) == 0)
			Instantiate(itemRocket, transform.position, Quaternion.identity);
		MusicManager.Instance.playNoise(0);
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
			//Debug.Log("follow");
			Vector2 aimed = computePathFinding();
			rb.velocity = aimed * speed;
		}
		else if (!follow_player)
		{
			rb.velocity = Vector3.SmoothDamp(rb.velocity, Vector3.zero, ref velocity, 0.3f);
		}
	}

	Vector2 computePathFinding()
	{
		//Choisis que le layer 8
		LayerMask layerMask = 1 << 9 | 1<<8;
		//Inverse
		//layerMask = ~layerMask;
		//Debug.DrawLine(player.transform.position, transform.position,  Color.green, Time.deltaTime, false);

		Vector2 bestPoint = Vector2.zero;
		RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, layerMask );
		if (hit.collider != null)
		{
			Debug.Log("hit " + hit.collider.gameObject.name);
			Debug.DrawLine(transform.position, hit.point, Color.green, Time.deltaTime, false);
			bestPoint = hit.point;
		}
		Debug.DrawLine(transform.position, bestPoint, Color.red, Time.deltaTime, false);
		return new Vector2(bestPoint.x- transform.position.x, bestPoint.y - transform.position.y).normalized;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		foreach (ContactPoint2D pt in coll.contacts)
		{
			if (pt.collider.tag == "Player")
			{
				Debug.Log("Atackkk");
				pt.collider.gameObject.SendMessage("takeDamage",damage);
			}
		}
		
	}
}
