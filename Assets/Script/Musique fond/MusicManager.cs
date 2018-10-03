using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MusicManager :  SingletonBehaviour<MusicManager>
{

	public GameObject audioFondPrefab;
	public GameObject audioNoisePrefab;
	public GameObject audioNoisePrefab_gun;
	public GameObject audioNoisePrefab_player;

	AudioSource audioFond;
	AudioSource audioNoise;
	AudioSource audioNoise_gun;
	AudioSource audioNoise_player;
	public bool win;

	public AudioClip MainMenu;
	public AudioClip Game;
	public AudioClip Credit;

	[SerializeField]
	public Noise[] Noises;


	[System.Serializable]
	public struct Noise
	{
		public AudioClip clip;
		public bool important;
	};

	public void playNoise(int nb)
	{
		if (audioNoise !=null && (!audioNoise.isPlaying || Noises[nb].important))
		{
			audioNoise.Stop();
			audioNoise.PlayOneShot(Noises[nb].clip);
		}
	}

	public void playNoise_gun(int nb)
	{
		if (audioNoise_gun != null && (!audioNoise_gun.isPlaying || Noises[nb].important))
		{
			audioNoise_gun.Stop();
			audioNoise_gun.PlayOneShot(Noises[nb].clip);
		}
	}

	public void playNoise_player(int nb)
	{
		if (audioNoise_player != null && (!audioNoise.isPlaying || Noises[nb].important))
		{
			audioNoise_player.Stop();
			audioNoise_player.PlayOneShot(Noises[nb].clip);
		}
	}


	void Start()
	{

		if (!GameObject.Find("AudioBackground(Clone)"))
		{
			audioFond = Instantiate(audioFondPrefab).GetComponent<AudioSource>();
			audioFond.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
			audioFond.clip = MainMenu;
		}
		if (!GameObject.Find("AudioNoise(Clone)"))
		{
			audioNoise = Instantiate(audioNoisePrefab).GetComponent<AudioSource>();
			audioNoise.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
		}

		if (!GameObject.Find("AudioNoise_gun(Clone)"))
		{
			audioNoise_gun = Instantiate(audioNoisePrefab_gun).GetComponent<AudioSource>();
			audioNoise_gun.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
		}

		if (!GameObject.Find("AudioNoise_player(Clone)"))
		{
			audioNoise_player = Instantiate(audioNoisePrefab_player).GetComponent<AudioSource>();
			audioNoise_player.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);
		}

		audioFond.PlayDelayed(0.3f);
	}

	private void Update()
	{
		

		if (audioNoise == null)
		{
			audioNoise = Instantiate(audioNoisePrefab).GetComponent<AudioSource>();
			audioNoise.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);

		}

		if (audioNoise_gun == null)
		{
			audioNoise_gun = Instantiate(audioNoisePrefab_gun).GetComponent<AudioSource>();
			audioNoise_gun.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);

		}

		if (audioNoise_player == null)
		{
			audioNoise_player = Instantiate(audioNoisePrefab_player).GetComponent<AudioSource>();
			audioNoise_player.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);

		}

		if (audioFond == null)
		{

			audioFond = Instantiate(audioFondPrefab).GetComponent<AudioSource>();
			audioFond.transform.SetParent(GameObject.FindGameObjectWithTag("MainCamera").transform);

		}
		else
		{
			
			if (HUDManager.Instance.state == stateMenu.Main  && audioFond.clip != MainMenu)
			{
				audioFond.Stop();
				audioFond.clip = MainMenu;
				audioFond.PlayDelayed(0.3f);

			}

			if (HUDManager.Instance.state == stateMenu.Play  && audioFond.clip != Game)
			{
				audioFond.Stop();
				audioFond.clip = Game;
				audioFond.PlayDelayed(0.3f);
			}

			if (HUDManager.Instance.state == stateMenu.Credit && audioFond.clip != Credit)
			{
				audioFond.Stop();
				audioFond.clip = Credit;
				audioFond.PlayDelayed(0.3f);
			}
			if (HUDManager.Instance.state == stateMenu.Win || HUDManager.Instance.state == stateMenu.GameOver)
			{
				audioFond.volume = Mathf.Lerp(audioFond.volume, 0f, 0.1f);
			}

			if (!audioFond.isPlaying)
			{
				audioFond.Play();
			}
		}
	}
}
