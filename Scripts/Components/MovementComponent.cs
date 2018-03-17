using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementComponent : ScriptableObject {

	[Range(1,10)]
	public int id;

	public float inertia;
	[Range(-1,1)]
	public float acceleration;
	[Range(-2,2)]
	public float agility = 3f;
	[Range(-20,20)]
	public float maxInertia = 10f;
	public float dampedDesiredInertia;
	public Vector2 waypoint = new Vector2(0f, 0f);
	public GameObject WaypointObject;


	public void DebugMe()
	{
		
	}

}
