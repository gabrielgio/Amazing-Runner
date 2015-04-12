using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public float heightCheck = 0.6f;

	private float velocity;
	private bool jump = false;
	private bool grounded = false;
	private Rigidbody rb3d;
	
	
	// Use this for initialization
	void Awake () 
	{
		
		rb3d = GetComponent<Rigidbody>();
		Application.CaptureScreenshot ("screemshot.png");
	}
	
	// Update is called once per frame
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
		
		if (Input.GetKey (KeyCode.Space) && grounded) {
			jump = true;
		}

		if (rb3d.velocity.x <= 0)
			Application.LoadLevel (Application.loadedLevel);
	}
	
	void FixedUpdate()
	{
		rb3d.velocity = new Vector2(Mathf.Sign (rb3d.velocity.x) * maxSpeed, rb3d.velocity.y);
		
		velocity = rb3d.velocity.magnitude;
		
		if (jump) {
			rb3d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
	}
}
