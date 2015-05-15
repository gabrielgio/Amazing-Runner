using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour 
{
	void Start () 
	{
		GameController.Instance.OnReload.AddListener(Self);
	}

	private void Self()
	{
		GameController.Instance.OnReload.RemoveListener (Self);
		Destroy (gameObject);
	}

	void OnDestroy ()
	{
		GameController.Instance.OnReload.RemoveListener (Self);
	}

}
