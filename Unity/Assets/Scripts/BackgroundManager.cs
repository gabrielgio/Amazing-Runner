using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BackgroundManager : MonoBehaviour {

	public GameObject Piece;
	public GameObject SecondPiece;
	public Transform Player;
	public BackgroundType Type;
	public int Frequency;
	public Vector2 Initial;
	public Vector2 Final;
	public int[] ZVelues;
	public int[] YValues;
	public int Rows;
	public int View;

	private float stepY;


	void Start () 
	{
		stepY = (Final.y - Initial.y) / Rows;

		if (Type == BackgroundType.Building) 
		{
			for (float x = Initial.x; x < 10000; x+=Frequency) {
				int z = ZVelues [Random.Range (0, ZVelues.Length)];
				int y = YValues [Random.Range (0, YValues.Length)];
				Instantiate (Piece, new Vector3 (x, y, z), new Quaternion ());
			}
		} 
		else 
		{
			for (float x = Initial.x; x < Final.x; x+=Frequency) {

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
		if (Player.position.x > Final.x - View) 
		{

			float y = Initial.y;
			for (int i = 0; i < Rows; i++, y+= stepY) 
			{
				int z = ZVelues [Random.Range (0, ZVelues.Length)];
				Instantiate (SecondPiece, new Vector3 (Final.x, y, z), new Quaternion ());
            }

			Final.x += Frequency;
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
