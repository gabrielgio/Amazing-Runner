using UnityEngine;
using System.Collections;
using System;

public class PlayerController2 : MonoBehaviour
{
	private Vector3 _moveDirection = Vector3.zero;

	private CharacterController _controller;

	private float _forceY = 0;

	private float _invertGrav;

	private Rigidbody _rb3d;

	public float speed = 6.0F;

	public float jumpSpeed = 20.0F;

	public float gravity = 20.0F;

	public float gravityForce = 3.0f; 

	public float airTime = 2f;

	void Start()
	{
		_invertGrav = gravity * airTime;
		_controller = GetComponent<CharacterController>();
		_rb3d = GetComponent<Rigidbody>();
	}

	void Update() 
	{
		_moveDirection = new Vector3 (1, 0, 0);

		_moveDirection = transform.TransformDirection (_moveDirection);
		_moveDirection *= speed;    
		if (_controller.isGrounded) 
		{
			_forceY = 0;	
			_invertGrav = gravity * airTime;

			if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButtonDown (0)) && GameController.Instance.CurrentState == GameState.Running)
			{
				_forceY = jumpSpeed;
			}
		}

		if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButtonDown (0)) && _forceY != 0 && GameController.Instance.CurrentState == GameState.Running) 
		{
			_invertGrav -= Time.deltaTime;
			_forceY += _invertGrav * Time.deltaTime;
		}

		_forceY -= gravity * Time.deltaTime * gravityForce;
		_moveDirection.y = _forceY;
		_controller.Move (_moveDirection * Time.deltaTime);



		speed += (float)(Time.deltaTime * 0.2);
	}

}
