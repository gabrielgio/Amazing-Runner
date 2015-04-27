using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RankComponent : MonoBehaviour 
{
	private List<Rank> _scores;

	private Text _position;

	private Text _player;

	private Text _score;

	[Range(0,9)]
	public int Index;

	void Awake () 
	{
		_position = transform.GetChild(0).GetComponent<Text>();
		_player = transform.GetChild(1).GetComponent<Text>();
		_score = transform.GetChild(2).GetComponent<Text>();
	}
	

	void Update () 
	{
		if (_scores != null && Index < _scores.Count) {
			_position.text = (Index + 1).ToString ();
			_player.text = _scores [Index].Name;
			_score.text = _scores [Index].Score.ToString ();
		} else {
			_position.text = (Index + 1).ToString ();
			_player.text = "---";
			_score.text = "---";
		}
	}


	public void Ranking(RankScoresEvent data)
	{
		_scores = data.Scores;
	}

}
