using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointsUpdater : MonoBehaviour 
{
	private Text points;

	void Start () 
	{
		points = GetComponent<Text> ();
	}

	void Update () 
	{
		points.text = ((int)GameController.Instance.Points).ToString()	;
	}
}
