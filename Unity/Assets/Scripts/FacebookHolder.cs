using UnityEngine;
using System.Collections;

public class FacebookHolder : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		FB.Init (SetInit, OnHideUnity);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	private void SetInit()
	{

	}

	private void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void FacebookLogin()
	{
		if (!FB.IsLoggedIn)
			FB.Login ("user_about_me, user_birthday", AuthCallback);
		else
			FB.Logout ();
	}

	void AuthCallback (FBResult result)
	{
		if (FB.IsLoggedIn) {
			Debug.Log ("FB logged in");
			FB.API("/me?fields=first_name", Facebook.HttpMethod.GET, ApiCallback);
		} else {
			Debug.Log ("FB didn't log in");
		}
	}

	void ApiCallback(FBResult result)
	{
		if (result.Error != null)
			Debug.Log("Error Response:\n" + result.Error);

		else if (!FB.IsLoggedIn)
			Debug.Log("Login cancelled by Player");

		else
		{
			IDictionary dict = Facebook.MiniJSON.Json.Deserialize(result.Text) as IDictionary;
			GameController.Instance.Name = dict["first_name"].ToString();
		}
	}
}
