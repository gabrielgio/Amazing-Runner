using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BackgroundManager : MonoBehaviour {

	public GameObject Piece;
	public int Frequency;
	public Vector2 Initial;
	public Vector2 Final;
	public int[] ZVelues;
	public int[] YValues;

	void Start () 
	{
		for (float x = Initial.x; x < 10000; x+=Frequency) 
		{
			int z = ZVelues[Random.Range(0, ZVelues.Length)];
			int y = YValues[Random.Range(0, YValues.Length)];
			Instantiate(Piece,new Vector3(x,y,z), new Quaternion());
		}
	}
}
