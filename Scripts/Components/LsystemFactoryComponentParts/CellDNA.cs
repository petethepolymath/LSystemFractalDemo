using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CellDNA : ScriptableObject {

	public Vector2 position = new Vector2(0,0);
	public float Angle = 0f;
	public float AngleChange = 30f;
	public GameObject gameObject;
	public float Size = 1f;

	public float LengthMultiplyer = 1f;

	public float Length
	{
		get {return (Size * LengthMultiplyer);}
	}
	public float Randomise = 0.1f;

	//[Range (0f,1f)]
	public float Hue
	{
		get{return (hue);} 
		set{hue = Mathf.Repeat (value, 1f);}
	}


			

	[Range (0f,1f)]
	public float hue = 0.4f;

	[Range (0f,1f)]
	public float Saturation = 0.5f;

	[Range (0f,1f)]
	public float Value = 1;

	[Range (0f,1f)]
	public float Alpha = 0.9f;
}
