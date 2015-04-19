using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using System.Text;
using SimpleJSON;

public class Ranking : MonoBehaviour {

	private string urlScores = "http://a4l.cloudapp.net/rank/rank";

	private string urlSendScore = "http://a4l.cloudapp.net/rank/sendrank?name={0}t&score={1}";

	public RankScoresEvent OnScoreGot;

	private static Ranking _instance;
	
	public static Ranking Instance
	{
		get{
			if(_instance == null)
				_instance = GameObject.Find("Player").GetComponent<Ranking>();
			
			return _instance;
		}
	}

	public void GetRank()
	{
		StartCoroutine (ResquestScore());
	}

	public IEnumerator SendRank(string name, int score)
	{
		WWW www = new WWW (string.Format (urlSendScore, name, score));
		
		yield return www;
	}


	// Use this for initialization
	void Start () {
		StartCoroutine (ResquestScore());	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private IEnumerator ResquestScore()
	{
		WWW www = new WWW (urlScores);
		
		yield return www;

		string data = Encoding.UTF8.GetString (www.bytes, 0, www.bytes.Length);

		JSONNode root = JSON.Parse (data);

		List<Rank> scores = new List<Rank> ();

		foreach (JSONNode item in root.AsArray) {
			scores.Add(new Rank(){
				Id = item[0].AsInt,
				Name = item[1],
				Score = item[2].AsInt,
			});
		}

		OnScoreGot.Invoke (new RankScoresEvent (){Scores = scores});
	}
}

[Serializable]
public class RankScoresEvent : UnityEvent<RankScoresEvent>
{
	public List<Rank> Scores{ get; set; }
}