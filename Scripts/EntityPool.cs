using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPool : MonoBehaviour {

	//public GameObject[] Entities;
	public List<ECS_Entity> Entities;





	public void InitialiseEntitiesList()
	{
		Entities = new List<ECS_Entity> ();
		Entities.Clear();
	}
}
