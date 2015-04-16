using UnityEngine;
using System.Collections;

public class FreezeAxis : MonoBehaviour {

	public bool x;
	public bool y;
	public bool z;

	private float lastX;
	private float lastY;
	private float lastZ;

	// Use this for initialization
	void Awake () {
		lastX = transform.position.x;
		lastY = transform.position.y;
		lastZ = transform.position.z;

	}
	
	// Update is called once per frame
	void Update () {
	
		if(!x)
			lastX = transform.position.x;
		
		
		if(!y)
			lastY = transform.position.y;
		
		
		if(!z)
			lastZ = transform.position.z;

			

		transform.position = new Vector3(lastX, lastY,lastZ);
	}
}
