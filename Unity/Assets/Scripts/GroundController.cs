using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundController : MonoBehaviour
{
	private float _xLastPosition;

	private float _delay;

	public Transform Player;

	public float DelayTime = 3;

	public List<GameObject> GroundPrefabs;

	public float Progression;

	public float DistanceView;

	void Awake () 
	{
		_xLastPosition = transform.position.x;
		_delay = DelayTime;
	}

	void Update () 
	{
		GameObject game = null;

		switch (GameController.Instance.CurrentState) 
		{
			case GameState.Pause:

				break;
			
			case GameState.MainMenu:
				game = GroundPrefabs[0];
				break;

			case GameState.Death:
				break;

			case GameState.Running:
				if(DelayTime <= 0)
					game = GroundPrefabs [Random.Range(1,GroundPrefabs.Count)];
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

		if ((Player.position.x) > _xLastPosition - DistanceView) {
			_xLastPosition += Progression;
			Instantiate (game, new Vector3 (_xLastPosition, transform.position.y), new Quaternion ());
		}
	}

	public void Reload()
	{
		_xLastPosition = transform.position.x;
		DelayTime = _delay;
	}
}
