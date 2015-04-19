﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private static bool IsFirstTime = true;

	public Rigidbody Player;

	public Animator MainMenuAnimator;

	public Animator PauseAnimator;

	public Animator HUDAnimator;

	public Animator RankAnimator;

	private static GameController _instance;
	
	public static GameController Instance
	{
		get{
			if(_instance == null)
				_instance = GameObject.Find("Main Camera").GetComponent<GameController>();

			return _instance;
		}
	}

	void Awake()
	{
		if (!IsFirstTime) {
			StartGameButtonPressed();
		}

		IsFirstTime = false;
	}

	public GameState CurrentState;

	public void OnPlayerDied()
	{

	}

	public void ShowRanking()
	{
		MainMenuAnimator.SetBool("IsHidden", true);
		
		PauseAnimator.SetBool("IsHidden", true);
		
		HUDAnimator.SetBool("IsHidden", true);
		
		RankAnimator.SetBool("IsHidden", false);
	}

	public void HideRanking()
	{
		RankAnimator.SetBool("IsHidden", true);

		switch (CurrentState) 
		{
		case GameState.Death:

			break;

		case GameState.MainMenu:
			MainMenuAnimator.SetBool("IsHidden", false);
			break;

		case GameState.Pause:
			PauseAnimator.SetBool("IsHidden", false);
			break;
		}
	}

	public void StartGameButtonPressed()
	{
		MainMenuAnimator.SetBool ("IsHidden", true);
		HUDAnimator.SetBool("IsHidden",false);

		CurrentState = GameState.Running;
	}

	public void RestartButtonPressed()
	{
		Application.LoadLevel (0);
	}

	public void PauseButtonPressed()
	{
		if (CurrentState != GameState.Pause) 
		{
			Player.isKinematic = true;
			CurrentState = GameState.Pause;
			PauseAnimator.SetBool("IsHidden",false);
			HUDAnimator.SetBool("IsHidden",true);

		}
		else 
		{
			Player.isKinematic = false;
			CurrentState = GameState.Running;
			PauseAnimator.SetBool("IsHidden",true);
			HUDAnimator.SetBool("IsHidden",false);
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}


public enum GameState{
	
	Running,
	Pause,
	MainMenu,
	Death
}