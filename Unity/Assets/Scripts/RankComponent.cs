using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RankComponent : MonoBehaviour {

	[Range(0,9)]
	public int Index;

	private List<Rank> scores;
	private Text text;
	private Text name;
	private Text score;


	// Use this for initialization
	void Awake () 
	{
		text = transform.GetChild(0).GetComponent<Text>();
		name = transform.GetChild(1).GetComponent<Text>();
		score = transform.GetChild(2).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (scores != null && Index < scores.Count) {
			text.text = (Index + 1).ToString ();
			name.text = scores [Index].Name;
			score.text = scores [Index].Score.ToString ();
		} else {
			text.text = (Index + 1).ToString ();
			name.text = "---";
			score.text = "---";
		}
	}


	public void Ranking(RankScoresEvent data)
	{
		scores = data.Scores;
	}

}
