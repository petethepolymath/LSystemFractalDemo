using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputSystem : MonoBehaviour, ECS_System {

	public EntityPool entityPool;
//	public List<PositionComponent> pos = new List<PositionComponent>();
//	public List<MovementComponent> mov = new List<MovementComponent>();
//	public List<MouseInputComponent> Imouse = new List<MouseInputComponent>();






	public void InjectEntityPool(EntityPool entityPoolToInject)
	{
		entityPool = entityPoolToInject;
	}

//	void Start()
//	{
//		GetComps ();
//	}

	void Update () 
	{
		
		if (Input.GetMouseButton(0)) 
		{
			
			Ray ray1 = Camera.main.ScreenPointToRay (Input.mousePosition);

			Iterate (ray1);

		}







	}

//	void GetComps()
//	{
//		if (entityPool != null) 
//		{
//			for (int i = 0; i < entityPool.Entities.Count; i++) 
//			{
//				if ((entityPool.Entities [i].GetComponent<ImouseInputReceiver> ()) != null) {
//					pos.Add (entityPool.Entities [i].GetComponent<PositionComponent> ());
//					mov.Add (entityPool.Entities [i].GetComponent<MovementComponent> ());
//					Imouse.Add (entityPool.Entities [i].GetComponent<MouseInputComponent> ());
//
//				
//				}
//			}
//		}
//	}

	 
	void Iterate(Ray ray1)
	{
		

		if (entityPool != null) 
		{
			for (int i = 0; i < entityPool.Entities.Count; i++) 
			{
				if (((entityPool.Entities[i].mouseInputComp) != null) && ((entityPool.Entities[i].movComp) != null) && ((entityPool.Entities[i].posComp) != null)) 
				{


					entityPool.Entities[i].movComp.waypoint = ray1.GetPoint (-1000);

				}
			}
		}
			
	
	}


}
