using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.Events;
using System;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour 
{
	private static bool _IsFirstTime = true;

	private static GameController _instance;

	private const string FILENAME = "filename.txt";

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

	public Animator AboutAnimator;

	public Animator CameraAnimator;

	public UnityEvent OnReload;

	public InputField Name;

	public int Lives = 3;

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
		_instance = this;

		if (!_IsFirstTime) 
		{
			StartGameButtonPressed();
		}

		_IsFirstTime = false;

		LoadName ();
	}

	public void OnPlayerDied()
	{
		if (CurrentState != GameState.Death) 
		{
			Ranking.Instance.PostRank (GameController.Instance.Name.text, (int)Points);
			Ranking.Instance.GetRank();
			Player.transform.position -= Vector3.back;
			PlayerAnimator.SetBool ("IsFaded", true);
			CurrentState = GameState.Death;
			Lives = 3;
			ShowPostMenu();
		}
	}

	public void LoadName()
	{
		if (File.Exists (string.Format ("{0}/{1}", Application.persistentDataPath, FILENAME))) 
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream fs = File.Open(string.Format ("{0}/{1}", Application.persistentDataPath, FILENAME), FileMode.OpenOrCreate);
			Name.text = (string)bf.Deserialize(fs);
			fs.Close();	
		}
	}

	public void NameChangedEdit(string name)
	{
		Debug.LogFormat ("Save {0}", Name.text);

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream fs = File.Open (string.Format ("{0}/{1}", Application.persistentDataPath, FILENAME), FileMode.OpenOrCreate);
		bf.Serialize (fs, Name.text);
		fs.Close ();
	}

	public void Reload()
	{
		Player.transform.position -= Vector3.back;
		PlayerAnimator.SetBool ("IsFaded", false);
		Player.speed = 10;
		Ranking.Instance.GetRank ();
		Points = 0;
		Lives = 3;
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

	public void ShowAboutMenu()
	{
		MainMenuAnimator.SetBool("IsHidden", true);
		AboutAnimator.SetBool("IsHidden", false);
		CameraAnimator.SetBool("Zoom", true);
    }

	public void HideAboutMenu()
	{
		MainMenuAnimator.SetBool("IsHidden", false);
		AboutAnimator.SetBool("IsHidden", true);
		CameraAnimator.SetBool("Zoom", false);
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
		Reload ();

		CurrentState = GameState.MainMenu;

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

		CameraAnimator.SetBool("Zoom", true);
	}

	public void HideRanking()
	{
		RankAnimator.SetBool("IsHidden", true);
		CameraAnimator.SetBool("Zoom", false);

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
			Time.timeScale = 0;
			CurrentState = GameState.Pause;
			PauseAnimator.SetBool("IsHidden",false);
			HUDAnimator.SetBool("IsHidden",true);

		}
		else 
		{
			Time.timeScale = 1;
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