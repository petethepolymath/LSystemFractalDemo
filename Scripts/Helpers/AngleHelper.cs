using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AngleHelper {

	public static Vector2 Vector2FromAngle(float a)
	{
		a *= Mathf.Deg2Rad;
		return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
	}

	public static float Vector2ToAngle(Vector2 dir)
	{
		float value = -(Vector2.SignedAngle (Vector2.up, dir));
		if(value < 0)
		{ 
			value = value + 360f;
		}
		return value;

	}
}
