using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public enum stateMenu
{
	Main,
	Tutorial,
	Pause,
	Play,
	Credit,
	GameOver,
	Win,
	Cinematique
}
public class HUDManager : SingletonBehaviour<HUDManager>
{

	public GameObject menuStart;
	public GameObject pausePanel;
	public GameObject ATHPanel;
	public GameObject TutorialPanel;
	public GameObject GameOverPanel;
	public GameObject WinPanel;
	public GameObject CreditPanel;
	public GameObject LoadingObjects;
	

	public Button[] buttonMain;
	public Button[] buttonPause;

	AsyncOperation async;
	public stateMenu state = stateMenu.Main;

	public Texture2D cursorTexture;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot;

	new void Awake()
	{
		base.Awake();

		#if UNITY_WEBGL
			buttonMain[3].gameObject.SetActive(false);
		#endif

		state = stateMenu.Main;
		hotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);

	}

	void Update()
	{

		if (state == stateMenu.Main)
		{
			if (Input.GetButtonDown("Cancel"))
			{
				quitGame();
			}
		}
		else if (state == stateMenu.Tutorial && Input.GetButtonDown("Cancel"))
		{
			Backtutorial();
		}
		else if (state == stateMenu.Credit && Input.GetButtonDown("Cancel"))
		{
			Backcredits();
		}
		else if (state == stateMenu.Play && Input.GetButtonDown("Cancel"))
		{
			Pause();
		}
		else if (state == stateMenu.Pause)
		{
			if (Input.GetButtonDown("Cancel"))
			{
				Backpause();
			}
		}
		else if (state == stateMenu.GameOver)
		{
			if (Input.anyKeyDown)
			{
				MenuLoad();
			}
		}
		else if (state == stateMenu.Win)
		{
			if (Input.anyKeyDown)
			{
				MenuLoad();
			}
		}
		else if (state == stateMenu.Cinematique)
		{
			if (Input.anyKeyDown)
			{
				LoadingObjects.SetActive(false);
				LoadGameScene();
			}
		}
	}

	/*Scripts button*/


	public void cursorChange()
	{
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

	public void cursorStd()
	{
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
	}
	//Menu

	public void playDemo()
	{
		//LoadGameScene();
		PlayCinematique();
	}

	public void LoadGameScene()
	{
		cursorChange();
		SceneManager.LoadScene(1, LoadSceneMode.Single);
		state = stateMenu.Play;
		menuStart.SetActive(false);
		ATHPanel.SetActive(true);

	}

	public void PlayCinematique()
	{
		LoadingObjects.SetActive(true);
		state = stateMenu.Cinematique;
		buttonMain[0].GetComponent<DialogueTrigger>().dialogueTrigger();
	}

	public void MenuLoad()
	{
		cursorStd();
		Time.timeScale = 1f;
		state = stateMenu.Main;
		SceneManager.LoadScene(0, LoadSceneMode.Single);
		menuStart.SetActive(true);
		ATHPanel.SetActive(false);
		pausePanel.SetActive(false);
		GameOverPanel.SetActive(false);
		WinPanel.SetActive(false);

		Time.timeScale = 1f;
		
	}

	public void quitGame()
	{
		Application.Quit();
		Debug.Log("Game closed");
	}


	//Credits

	public void Credits()
	{
		state = stateMenu.Credit;
		CreditPanel.SetActive(true);
	}

	public void Backcredits()
	{
		state = stateMenu.Main;
		CreditPanel.SetActive(false);
	}

	//Tutorial

	public void Tutorial()
	{
		state = stateMenu.Tutorial;
		TutorialPanel.SetActive(true);

	}

	public void Backtutorial()
	{
		state = stateMenu.Main;
		TutorialPanel.SetActive(false);
	}

	//Pause

	public void Pause()
	{
		cursorStd();
		Time.timeScale = 0f;
		state = stateMenu.Pause;
		pausePanel.SetActive(true);
	}

	public void Backpause()
	{
		cursorChange();
		Time.timeScale = 1f;
		state = stateMenu.Play;
		pausePanel.SetActive(false);
	}


	public void GameOver()
	{
		Time.timeScale = 0f;
		StartCoroutine(delayState(stateMenu.GameOver, 2f)); 
		MusicManager.Instance.playNoise_player(12);
		GameOverPanel.SetActive(true);
		
	}

	public void BackGameOver()
	{
		Time.timeScale = 1f;
		GameOverPanel.SetActive(false);
		state = stateMenu.Play;
	}

	public void Win()
	{
		Time.timeScale = 0f;
		StartCoroutine(delayState(stateMenu.Win, 2f));
		MusicManager.Instance.playNoise_player(14);
		WinPanel.SetActive(true);
		
	}

	public void BackWin()
	{
		Time.timeScale = 1f;
		WinPanel.SetActive(false);
		state = stateMenu.Play;
	}



	IEnumerator delayState(stateMenu s, float time)
	{
		yield return new WaitForSecondsRealtime(time);
		state = s;
	}

	/*
	public void Loading()
	{
		StartCoroutine(loadingScreen());
	}
	IEnumerator loadingScreen()
	{
		LoadingObjects.SetActive(true);
		async = SceneManager.LoadSceneAsync(1);
		async.allowSceneActivation = false;
		while (async.isDone == false)
		{
			// [0, 0.9] > [0, 1]
			float progress = Mathf.Clamp01(async.progress / 0.9f);
			Debug.Log("Loading progress: " + (progress * 100) + "%");

			LoadingObjects.GetComponent<DialogueTrigger>().dialogueTrigger();


			// Loading completed
			if (async.progress == 0.9f)
			{
				Debug.Log("Press a key to start");
				if (Input.anyKeyDown)
				{
					async.allowSceneActivation = true;
					LoadingObjects.SetActive(false);
					state = stateMenu.Play;
					menuStart.SetActive(false);
					ATHPanel.SetActive(true);
				}
			}
			yield return null;
		}
	}
	*/
}

