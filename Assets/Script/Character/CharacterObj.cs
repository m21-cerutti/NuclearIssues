using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterObj : MonoBehaviour {

	public int max_life;
	public int life;
	public int damage;

	//float time_death = 5f;
	//Color startColor;
	//bool start_corout = false;


	private void Start()
	{
		//startColor = this.GetComponent<SpriteRenderer>().color;
	}


	public float ratio()
	{
		return (float)(life * 1f / max_life);
	}

	public void takeDamage(int dam)
	{
		if (life - dam > 0)
		{
			life -= dam;
		}
		else
		{
			die();
		}
	}

	public abstract void die();

	public void destroy()
	{
		Destroy(this.gameObject);
	}

}
