using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	public GameObject bullet;
	public GameObject rocket;

	public float speed = 15f;
	public float speedRocket = 10f;

	public float cadence = 0.3f;
	public float cadenceRocket = 0.8f;

	private float timer;
	private float timerRocket;
	public int nbRockets = 1;

	/*
	public enum Weapons
	{
		MP5,
		MachineGun,
		RocketLauncher,
		C4
	};
	*/
	private bool shoot;
	private bool rocketShoot;

	void Update ()
	{
		shoot = Input.GetMouseButton(0);
		rocketShoot = Input.GetMouseButton(1) && (nbRockets > 0);
	}

	void FixedUpdate()
	{
		//Normal
		if (shoot && timer < 0) {
			Shoot(bullet, speed);
			MusicManager.Instance.playNoise_gun(16);
			timer = cadence;
		}else if (timer >= 0)
		{
			timer -= Time.deltaTime;
		}

		//Rocket
		if (rocketShoot && timerRocket < 0)
		{
			Shoot(rocket, speedRocket);
			MusicManager.Instance.playNoise_gun(4);
			timerRocket = cadenceRocket;
			nbRockets--;
		}
		else if (timerRocket >= 0)
		{
			timerRocket -= Time.deltaTime;
		}
	}

	public void machineGun(float speed)
	{
		cadence = speed;
	}

	public void newRocket()
	{
		nbRockets++;
	}

	public int getNbRockets()
	{
		return nbRockets;
	}

	void Shoot(GameObject prefab, float speed)
	{
		Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 vect = mouse - transform.position;
		float rot_z = Mathf.Atan2(vect.y, vect.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.Euler(0f, 0f, rot_z);

		GameObject obj = Instantiate(prefab, transform.position + bullet.transform.position, rotation);

		obj.GetComponent<Rigidbody2D>().velocity = new Vector3(mouse.x - transform.position.x,	mouse.y - transform.position.y).normalized * speed;
	}
}
