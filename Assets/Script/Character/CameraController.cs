using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

	#region Editor
	[SerializeField]
	private GameObject _player;
	public GameObject Player { set { _player = value; } }

	[SerializeField]
	public bool freeCamera;

	[SerializeField]
	public float heightDown;
	public float heightUp;
	#endregion

	//Height of camera
	//public float relative_height;

	#region smooth camera variable
	float _smoothTimeY = 0.2F;
	float _smoothTimeX = 0.3F;
	//Always set to 0
	float _yVelocity = 0.0F;
	float _xVelocity = 0.0F;
	#endregion



	void Start()
	{
		freeCamera = false ;

	}

	//void Update () {}

	void FixedUpdate()
	{
		
		if (!freeCamera && _player!=null)
		{
			//Debug.Log("test" + GetComponent<Camera>().gameObject.name);
			float trslty = Mathf.SmoothDamp(0f, _player.transform.position.y - transform.position.y , ref _yVelocity, _smoothTimeY);
			float trsltx = Mathf.SmoothDamp(0f, _player.transform.position.x - transform.position.x, ref _xVelocity, _smoothTimeX);

			if ((_player.transform.position.y > heightDown && _player.transform.position.y < heightUp) ||( transform.position.y > heightDown && transform.position.y < heightUp) )
			{

				transform.Translate(new Vector3(trsltx, trslty, 0f));

			}
			else
			{
				
				transform.Translate(new Vector3(trsltx, 0f, 0f));
				
			}
		}
	}

	
	
}
