using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundController : MonoBehaviour {

	public Transform Player;

	public float DelayTime = 3;

	public List<GameObject> GroundPrefabs;

	public float Progression;

	public float DistanceView;

	private float xLastPosition;

	void Awake () {
		xLastPosition = transform.position.x;
	}
	

	void Update () {


		GameObject game = null;

		switch (GameController.Instance.CurrentState) {
		
		case GameState.Pause:

			break;
		
		case GameState.MainMenu:
			game = GroundPrefabs[0];
			break;

		case GameState.Death:
			break;

		case GameState.Running:
			if(DelayTime <= 0)
				game = GroundPrefabs [Random.Range(1,GroundPrefabs.Count-1)];
			else
			{
				DelayTime -= Time.deltaTime;
				game = GroundPrefabs[0];
			}
			break;
		
		default:
			game = GroundPrefabs[0];
			break;
		}

		 

		if ((Player.position.x) > xLastPosition - DistanceView) {
			xLastPosition += Progression;
			Instantiate (game, new Vector3 (xLastPosition, transform.position.y), new Quaternion ());
		}
	}
}
