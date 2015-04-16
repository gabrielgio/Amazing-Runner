using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	public Animator MainMenuAnimator;

	private static GameController _instance;
	
	public static GameController Instance
	{
		get{
			if(_instance == null)
				_instance = GameObject.Find("Main Camera").GetComponent<GameController>();

			return _instance;
		}
	}

	public GameState CurrentState;

	public void OnPlayerDied()
	{

	}

	public void StartGameButtonPressed()
	{
		MainMenuAnimator.SetBool ("IsHidden", true);
		CurrentState = GameState.Running;
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