using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class TrapManager : MonoBehaviour 
{	
	private static TrapManager _instance;

	private bool _inTrap;

	private delegate void BackIt();

	private float _delay;

	private BackIt Back;

	public Camera MainCamera;

		

	public static TrapManager Instance
	{
		get{
			if(_instance == null)
				_instance = GameObject.Find("TrapManager").GetComponent<TrapManager>();
			
			return _instance;
		}

	}

	void Update () 
	{
		if (_delay <= 0)
			_inTrap = false;

		if (_inTrap)
			_delay -= Time.deltaTime;
		else if (Back != null)
			Back ();

	}

	public void TrapIt()
	{
		if (_inTrap)
			return;

		float value = Random.Range(0, 1f);

		if (value <= 0.25) {

			MainCamera.transform.localRotation = new Quaternion (0, 0, 180, 0);
			Back = UntrapCamera;
			_inTrap = true;
			_delay = 2;
		} 
		else if (value <= 0.50) 
		{
			MainCamera.GetComponent<VignetteAndChromaticAberration>().intensity = 9;
			_inTrap = true;
			_delay = 5;
			Back = UntrapVignette;
		}
		else if (value <= 0.75) 
		{
			MainCamera.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 50;
			_inTrap = true;
			_delay = 5;
			Back = UntrapChroma;
		}
		else if (value <= 1) 
		{
			MainCamera.GetComponent<Blur>().enabled = true;
			_inTrap = true;
			_delay = 5;
			Back = UntrapBlur;
		}
	}

	private void UntrapCamera()
	{
		MainCamera.transform.localRotation = new Quaternion (0, 0, 0, 0);
		Back = null;
	}

	private void UntrapVignette()
	{
		MainCamera.GetComponent<VignetteAndChromaticAberration>().intensity = 0;
		Back = null;
	}

	private void UntrapChroma()
	{
		MainCamera.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 0;
		Back = null;
	}

	private void UntrapBlur()
	{
		MainCamera.GetComponent<Blur>().enabled = false;
		Back = null;
	}
}
