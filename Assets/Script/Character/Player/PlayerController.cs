using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Rigidbody2D player;

	public float speed = 1f;

	private float vertical;
	private float horizontal;

	Animator anim;
	
	void Start ()
	{
		player = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");

		anim.SetFloat("Horizontal", horizontal);
		anim.SetFloat("Vertical", vertical);
	}

	void FixedUpdate ()
	{
		player.velocity = new Vector2(horizontal, vertical) * speed;
		anim.SetFloat("Speed", player.velocity.magnitude);
	}
}
