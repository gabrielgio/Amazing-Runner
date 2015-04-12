using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour {

	public Transform Player;

	public GameObject GroundPrefab;

	public float Progression;

	private bool done = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.x - Player.position.x) < 10 && !done) {
			Instantiate (GroundPrefab, new Vector3 (transform.position.x + Progression, 0, 0), new Quaternion ()).name = name;
			done = true;
		}

		if ((Player.position.x - transform.position.x) > 20)
			Destroy (this.gameObject);
	}
}
