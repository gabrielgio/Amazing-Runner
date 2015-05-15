using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesUpdater : MonoBehaviour {

	public Text Text;

	void Start () 
	{
		Text = GetComponent<Text> ();
	}

	void Update () 
	{
		Text.text = GameController.Instance.Lives.ToString();
	}
}
