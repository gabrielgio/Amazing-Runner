using UnityEngine;
using System.Collections;

public class SquareMove : MonoBehaviour {


	private enum SquareState
	{
		Idle,
		Moving
	}

	private SquareState CurrentState = SquareState.Idle;
	private int To;
	private float	 factor;

	public float Chance = 0.001f;
	public float Velocity = 5;


	void Start () 
	{
		Velocity = Random.Range (1, 6);
	}

	void Update () 
	{
		if (CurrentState == SquareState.Idle) 
		{

			if (Random.Range(0,1f) < Chance)
			{
							
				CurrentState = SquareState.Moving;
				To = BackgroundManager.Instance.ZVelues [Random.Range (0, BackgroundManager.Instance.ZVelues.Length)];
				factor = (To - transform.position.z);
			}
		}
		else
		{
			float increment = factor*(Velocity*Time.deltaTime);
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + increment);

			if(factor >= 0)
			{
				if(transform.position.z >= To)
					CurrentState = SquareState.Idle;

			}
			else
			{
				if(transform.position.z < To)
					CurrentState = SquareState.Idle;
			}
		}
	}
}
