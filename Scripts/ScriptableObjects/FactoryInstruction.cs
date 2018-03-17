using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class FactoryInstruction : ScriptableObject
{
	
	//public GameObject prefab;

	public ECS_Entity entity;

	//public int numberToSpawn;
	public Vector2[] spawnPoint;


	public bool gameObj;
	public bool trans;
	public bool posComp;
	public bool movComp;
	public bool mouseInputComp;
	public bool keyInputComp;
	public bool navWaypointComp;
	public bool factoryComp;
	public bool lSysComp;




}



