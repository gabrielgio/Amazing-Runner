using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;
using System;

public class GameController : MonoBehaviour 
{
	private static bool _IsFirstTime = true;

	private static GameController _instance;

	public float InitialSpeed = 6;

	public float YDeath = -11;

	public Vector3 InicialPosition;

	public float Points = 0;

	public PlayerController2 Player;

	public GameState CurrentState;

	public Animator MainMenuAnimator;
	
	public Animator PauseAnimator;
	
	public Animator HUDAnimator;
	
	public Animator RankAnimator;

	public Animator PostAnimator;

	public Animator PlayerAnimator;

	public UnityEvent OnReload;

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
		if (!_IsFirstTime) {
			StartGameButtonPressed();
		}

		_IsFirstTime = false;
	}

	public void OnPlayerDied()
	{
		if (CurrentState != GameState.Death) 
		{
			Player.transform.position -= Vector3.back;
			PlayerAnimator.SetBool ("IsFaded", true);
			CurrentState = GameState.Death;	
			ShowPostMenu();
		}
	}	

	public void Reload()
	{
		Player.transform.position -= Vector3.back;
		PlayerAnimator.SetBool ("IsFaded", false);
		Player.speed = 10;
		Ranking.Instance.PostRank (Guid.NewGuid ().ToString().ToUpper(), (int)Points);
		Ranking.Instance.GetRank ();
		Points = 0;
		CurrentState = GameState.Running;
		OnReload.Invoke ();
		OnReload.Invoke ();
		GameObject.Find ("Player").GetComponent<Transform> ().position = InicialPosition.Clone ();
		ShowHUD ();
	}


	public void ShowHUD()
	{
		MainMenuAnimator.SetBool("IsHidden", true);
		
		PostAnimator.SetBool("IsHidden", true);
		
		PauseAnimator.SetBool("IsHidden", true);
		
		HUDAnimator.SetBool("IsHidden", false);
		
		RankAnimator.SetBool("IsHidden", true);
	}


	public void ShowPostMenu()
	{
		MainMenuAnimator.SetBool("IsHidden", true);
		
		PostAnimator.SetBool("IsHidden", false);
		
		PauseAnimator.SetBool("IsHidden", true);
		
		HUDAnimator.SetBool("IsHidden", true);
		
		RankAnimator.SetBool("IsHidden", true);
	}

	public void ShowMainMenu()
	{
		MainMenuAnimator.SetBool("IsHidden", false);

		PostAnimator.SetBool("IsHidden", true);
		
		PauseAnimator.SetBool("IsHidden", true);
		
		HUDAnimator.SetBool("IsHidden", true);
		
		RankAnimator.SetBool("IsHidden", true);
	}

	public void ShowRanking()
	{
		MainMenuAnimator.SetBool("IsHidden", true);
		
		PauseAnimator.SetBool("IsHidden", true);
		
		HUDAnimator.SetBool("IsHidden", true);
		
		RankAnimator.SetBool("IsHidden", false);

		PostAnimator.SetBool("IsHidden", true);
	}

	public void HideRanking()
	{
		RankAnimator.SetBool("IsHidden", true);

		switch (CurrentState) 
		{
		case GameState.Death:
			PostAnimator.SetBool("IsHidden", false);
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
		Reload ();
	}

	public void PauseButtonPressed()
	{
		if (CurrentState != GameState.Pause) 
		{
			CurrentState = GameState.Pause;
			PauseAnimator.SetBool("IsHidden",false);
			HUDAnimator.SetBool("IsHidden",true);

		}
		else 
		{
			CurrentState = GameState.Running;
			PauseAnimator.SetBool("IsHidden",true);
			HUDAnimator.SetBool("IsHidden",false);
		}
	}


	void Start ()
	{

	}

	void Update () 
	{
		if (CurrentState == GameState.Running) 
		{
			Points += Time.deltaTime;
		}

	}
}


public enum GameState{
	
	Running,
	Pause,
	MainMenu,
	Death
}