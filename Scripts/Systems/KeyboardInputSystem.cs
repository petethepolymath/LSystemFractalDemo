using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputSystem : MonoBehaviour, ECS_System {

	public EntityPool entityPool;
//	public List<MovementComponent> mov = new List<MovementComponent>();
//	public List<KeyboardInputComponent> Ikeys = new List<KeyboardInputComponent>();



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
		
		if (Input.GetKeyDown(KeyCode.Equals)) 
		{
			
		
			Iterate (1);

		}



		if (Input.GetKeyDown(KeyCode.Minus)) 
		{


			Iterate (0);

		}





	}

	void Iterate(int input)
	{
		if (entityPool != null) 
		{
			for (int i = 0; i < entityPool.Entities.Count; i++) 
			{
				if (((entityPool.Entities[i].keyInputComp) != null) && ((entityPool.Entities[i].movComp) != null)) 
				{
					

					entityPool.Entities[i].movComp.acceleration = input;

				}
			}
		}
	}

	 




}
