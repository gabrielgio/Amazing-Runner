using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BackgroundManager : MonoBehaviour {

	private static BackgroundManager _instance;
	private float stepY;

	public GameObject Piece;
	public GameObject SecondPiece;
	public Transform Player;
	public BackgroundType Type;
	public int FrequencyBuilding;
	public int FrequencySquare;
	public Vector2 Initial;
	public Vector2 Final;
	public int[] ZVelues;
	public int[] YValues;
	public int Rows;
	public int View;

	public static BackgroundManager	 Instance
	{
		get{
			if(_instance == null)
				_instance = GameObject.Find("Background").GetComponent<BackgroundManager>();
			
			return _instance;
		}
	}

	void Start () 
	{
		stepY = (Final.y - Initial.y) / Rows;

		if (Type == BackgroundType.Building) 
		{
			for (float x = Initial.x; x < 10000; x+=FrequencyBuilding) {
				int z = ZVelues [Random.Range (0, ZVelues.Length)];
				int y = YValues [Random.Range (0, YValues.Length)];
				Instantiate (Piece, new Vector3 (x, y, z), new Quaternion ());
			}
		} 
		else 
		{
			for (float x = Initial.x; x < Final.x; x+=FrequencySquare) {

				float y = Initial.y;
				for (int i = 0; i < Rows; i++, y+= stepY) 
				{
					int z = ZVelues [Random.Range (0, ZVelues.Length)];
					Instantiate (SecondPiece, new Vector3 (x, y, z), new Quaternion ());
				}

            }
            
		}
	}

	public void Update()
	{
		if (Type == BackgroundType.Square) {
			if (Player.position.x > Final.x - View) {

				float y = Initial.y;
				for (int i = 0; i < Rows; i++, y+= stepY) {
					int z = ZVelues [Random.Range (0, ZVelues.Length)];
					Instantiate (SecondPiece, new Vector3 (Final.x, y, z), new Quaternion ());
				}

				Final.x += FrequencySquare;
			}
		}
		/*
		if (Player.position.y < Final.y + View	) {

			float x = Final.x - 150;
			for (int i = 0; i < 5; i++, x+= Frequency) 
			{
				int z = ZVelues [Random.Range (0, ZVelues.Length)];
				Instantiate (SecondPiece, new Vector3 (x, Final.y, z), new Quaternion ());
            }

			Final.y -= 30;
        }*/
    }
}

public enum BackgroundType
{
	Building,
	Square
}
