using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour, ECS_System { 





	public EntityPool entityPool;
	//public GameObject[] Movables;

//	public List<PositionComponent> pos = new List<PositionComponent>();
//	public List<MovementComponent> mov = new List<MovementComponent>();
//	public List<Transform> trans = new List<Transform>();


	PositionComponent thisPos;
	MovementComponent thisMov;
	Transform thisTrans;


	public void InjectEntityPool(EntityPool entityPoolToInject)
	{
		entityPool = entityPoolToInject;
	}



	
	// Update is called once per frame
	void Update () 
	{
		Iterate ();

	}

//	void GetComps()
//	{
//		if(entityPool != null)
//		{
//			pos.Clear ();
//			mov.Clear();
//			trans.Clear ();
//
//
//				for (int i = 0; i < entityPool.Entities.Count; i++) 
//				{
//					if (((entityPool.Entities [i].GetComponent<PositionComponent> ()) != null) & ((entityPool.Entities [i].GetComponent<MovementComponent> ()) != null)) 
//					{
//						pos.Add (entityPool.Entities [i].GetComponent<PositionComponent> ());
//						mov.Add (entityPool.Entities [i].GetComponent<MovementComponent> ());
//						trans.Add (entityPool.Entities [i].GetComponent<Transform> ());
//
//					}
//
//					
//				}
//		}
//	}

	void Iterate()
	{
		if (entityPool != null) 
		{

			for (int i = 0; i < entityPool.Entities.Count; i++) 
			{

				if (((entityPool.Entities [i].movComp) != null) && ((entityPool.Entities [i].posComp) != null)) 
				{
					
					thisMov = entityPool.Entities [i].movComp;
					thisPos = entityPool.Entities [i].posComp;
					thisTrans = entityPool.Entities [i].trans;

				

					Rotate ();
					VectorToAngle ();

					ApplyRotationToTransform ();

					ApplyAccelerationToVelocity ();
					ApplyVelocityToPosition ();
					ApplyUpdatedPositionToTransform ();

				}

			}
		}
	}

	void Rotate()
	{
		if (thisMov.WaypointObject != null) 
		{
			RotateToWaypointObject ();
		} 
		else 
		{
			RotateToWaypoint ();
		}
	}

	void RotateToWaypoint()
	{

		//thisPos.worldDirectionAngle = AngleHelper.Vector2ToAngle (Vector2.Lerp (thisPos.directionVector, thisMov.waypoint - thisPos.position, thisMov.agility * Time.deltaTime));
		thisPos.directionVector = Vector2.Lerp (thisPos.directionVector, thisMov.waypoint - thisPos.position, thisMov.agility * Time.deltaTime).normalized;
	}

	void RotateToWaypointObject()
	{

		//thisPos.worldDirectionAngle = AngleHelper.Vector2ToAngle (Vector2.Lerp (thisPos.directionVector, (Vector2)thisMov.WaypointObject.transform.position - thisPos.position, thisMov.agility * Time.deltaTime));
		thisPos.directionVector = Vector2.Lerp (thisPos.directionVector, (Vector2)thisMov.WaypointObject.transform.position - thisPos.position, thisMov.agility * Time.deltaTime).normalized;
	}

	void VectorToAngle()
	{
		thisPos.worldDirectionAngle = AngleHelper.Vector2ToAngle (thisPos.directionVector);
	}

	void ApplyRotationToTransform()
	{
		thisTrans.rotation = Quaternion.AngleAxis(thisPos.CompassHeading, Vector3.forward);
	}






	void ApplyAccelerationToVelocity()
	{


		thisMov.dampedDesiredInertia = Mathf.Lerp (thisMov.inertia, thisMov.maxInertia, 0.5f);

		thisMov.inertia = Mathf.Lerp (thisMov.inertia, (thisMov.dampedDesiredInertia * thisMov.acceleration), (thisMov.agility * Time.deltaTime));

		//	mov.inertia += mov.acceleration * mov.agility * Time.deltaTime;
	}



	void ApplyVelocityToPosition()
	{
		thisPos.position += (CalculateVector () * Time.deltaTime);
		//thisTrans.position = thisPos.position;
	}

	Vector2 CalculateVector()
	{
		return (thisPos.directionVector.normalized * thisMov.inertia);
		//Debug.Log ("vector = " + pos.direction * mov.inertia);
	}






	void ApplyUpdatedPositionToTransform()
	{
		thisTrans.position = thisPos.position;
	}









}
