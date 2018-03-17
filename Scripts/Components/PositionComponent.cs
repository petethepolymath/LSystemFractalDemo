using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PositionComponent : ScriptableObject {

	public int id;
	public Vector2 position;




	[Range(0,360)]
	public float CompassHeading;
	public float worldDirectionAngle
	{
		get{return (-CompassHeading + 90f);}
		set{ CompassHeading = (-value + 90f);}
	}

	public Vector2 directionVector;


	public float directionTest;




	void Update()
	{


	//	directionVector = AngleHelper.Vector2FromAngle ((-CompassHeading+90f ));
		//directionTest = (AngleHelper.Vector2ToAngle (directionVector));

	}







}
