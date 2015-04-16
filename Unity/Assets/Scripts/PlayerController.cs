using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public float heightCheck = 0.6f;
	public float DeadY = -11;

	public UnityEvent OnPlayerDied;

	private bool jump = false;
	private bool grounded = false;
	private Rigidbody rb3d;


	
	// Use this for initialization
	void Awake () 
	{
		rb3d = GetComponent<Rigidbody>();
		Application.CaptureScreenshot ("screemshot.png");
	}

	void Update () 
	{
		
		RaycastHit hit;
		
		if (Physics.Raycast (new Ray (transform.position, Vector3.down), out hit, heightCheck)) {
			
			if(hit.collider.tag == "Ground")
				grounded = true;
			else
				grounded = false;		
		}
		else
			grounded = false;
		
		if (Input.GetKey (KeyCode.Space) && grounded && GameController.Instance.CurrentState == GameState.Running) {
			jump = true;
		}

		if (transform.position.y > DeadY) {
			OnPlayerDied.Invoke();
		}

		if (rb3d.velocity.x <= 0 && GameController.Instance.CurrentState != GameState.Pause)
			Application.LoadLevel (0);

	}

	public void Jump()
	{
		if(grounded && GameController.Instance.CurrentState == GameState.Running)
			jump = true;
	}

	void OnDestroy()
	{
		rb3d.Sleep();
	}
	
	void FixedUpdate()
	{
		rb3d.velocity = new Vector2(Mathf.Sign (rb3d.velocity.x) * maxSpeed, rb3d.velocity.y);

		if (jump) {
			rb3d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
	}
}
