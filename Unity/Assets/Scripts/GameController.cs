using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class GameController : MonoBehaviour 
{
	private static bool _IsFirstTime = true;

	private static GameController _instance;

	public float InitialSpeed = 6;

	public Rigidbody Player;

	public Vector3 InicialPosition;

	public float Points = 0;

	public GameState CurrentState;

	public Animator MainMenuAnimator;
	
	public Animator PauseAnimator;
	
	public Animator HUDAnimator;
	
	public Animator RankAnimator;

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
		
	}	

	public void Reload()
	{
		Points = 0;
		OnReload.Invoke ();
		GameObject.Find ("Player").GetComponent<Transform> ().position = InicialPosition.Clone ();
	}

	public void ShowMainMenu()
	{
		MainMenuAnimator.SetBool("IsHidden", false);
		
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
		Reload ();
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


	void Start ()
	{

	}

	void Update () 
	{
		if (CurrentState == GameState.Running)
			Points += Time.deltaTime;

	}
}


public enum GameState{
	
	Running,
	Pause,
	MainMenu,
	Death
}