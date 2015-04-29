using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class PlayerController : MonoBehaviour 
{
	private bool _jump = false;

	private bool _grounded = false;

	private Rigidbody _rb3d;

	public float moveForce = 365f;

	public float maxSpeed = 5f;

	public float jumpForce = 1000f;

	public float heightCheck = 0.6f;

	public float DeadY = -11;

	public UnityEvent OnPlayerDied;

	void Awake () 
	{
		_rb3d = GetComponent<Rigidbody>();
		Application.CaptureScreenshot ("screemshot.png");
	}

	void Update () 
	{
		RaycastHit hit;
		
		if (Physics.Raycast (new Ray (transform.position, Vector3.down), out hit, heightCheck)) {
			
			if(hit.collider.tag == "Ground")
				_grounded = true;
			else
				_grounded = false;		
		}
		else
			_grounded = false;
		
		if (Input.GetKey (KeyCode.Space) && _grounded && GameController.Instance.CurrentState == GameState.Running) {
			_jump = true;
		}

		if (transform.position.y > DeadY) {
			OnPlayerDied.Invoke();
		}

		if (_rb3d.velocity.x <= 0 && GameController.Instance.CurrentState != GameState.Pause) {
		

			Application.LoadLevel (0);
		}

	}

	public void Jump()
	{
		if(_grounded && GameController.Instance.CurrentState == GameState.Running)
			_jump = true;
	}

	void OnDestroy()
	{
		_rb3d.Sleep();
	}
	
	void FixedUpdate()
	{
		_rb3d.velocity = new Vector2(Mathf.Sign (_rb3d.velocity.x) * maxSpeed, _rb3d.velocity.y);

		if (_jump) {
			_rb3d.AddForce (new Vector2 (0f, jumpForce));
			_jump = false;
		}
	}
}
