using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {

	public float Chance = 0.15f;

	// Use this for initialization
	void Start () 
	{
		if (Random.Range (0, 1f) < Chance) {
			Destroy (this.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		Destroy (this.gameObject);
    }
}
