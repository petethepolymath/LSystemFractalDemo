using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySystem : MonoBehaviour, ECS_System {

	public EntityPool entityPool;





	public void InjectEntityPool(EntityPool entityPoolToInject)
	{
		entityPool = entityPoolToInject;
	}

	void OnEnable()
	{
		
	}



	public void SpawnEntities(FactoryInstruction[] entityTemplatesToSpawn )
	{
		foreach(FactoryInstruction i in entityTemplatesToSpawn)
		{
			for (int ii = 0; ii < i.spawnPoint.Length; ii++) 
			{
			//	Debug.Log (this.gameObject + " . " + this + "SpawnLevelStartEntities : " + i + "  " + i.entity);

				AddToEntityPool (i,i.spawnPoint[ii]);

			}

		}
	}


	public void AddToEntityPool(FactoryInstruction thisTemp, Vector2 spawnPoint)
	{


		ECS_Entity thisEntity = (ECS_Entity)Instantiate (thisTemp.entity);
		entityPool.Entities.Add (thisEntity);
		thisEntity.posComp.position = spawnPoint;
		randomiseEntityMovStats (thisEntity);

		InstantiateGameObjectAndInitialise (thisEntity);




	}


	public  void randomiseEntityMovStats (ECS_Entity thisEntity)
	{
		if(thisEntity.movComp != null)
		{

			thisEntity.movComp.maxInertia *= Random.Range(0.9f, 1.1f);
			thisEntity.movComp.agility *= Random.Range(0.9f, 1.1f);
		}


	}

	private void InstantiateGameObjectAndInitialise (ECS_Entity thisEntity)
	{
		InstantiateComponentsAndInitialise (thisEntity);

		thisEntity.gameObj = (GameObject)Instantiate(thisEntity.gameObj);
		thisEntity.gameObj.transform.parent = entityPool.gameObject.transform;
		thisEntity.gameObj.transform.position = thisEntity.posComp.position;
		thisEntity.trans = thisEntity.gameObj.transform;


		EntityLink myEntityLink = thisEntity.gameObj.AddComponent<EntityLink> ();
		myEntityLink.myEntity = thisEntity;

	}

	private void InstantiateComponentsAndInitialise (ECS_Entity thisEntity)
	{

		if (thisEntity.posComp != null) 
		{
			thisEntity.posComp = (PositionComponent)Instantiate (thisEntity.posComp);
		}

		if (thisEntity.movComp != null) {
			thisEntity.movComp = (MovementComponent)Instantiate (thisEntity.movComp);
		}
		if (thisEntity.mouseInputComp != null) {
			thisEntity.mouseInputComp = (MouseInputComponent)Instantiate (thisEntity.mouseInputComp);
		}
		if (thisEntity.keyInputComp != null) {
			thisEntity.keyInputComp = (KeyboardInputComponent)Instantiate (thisEntity.keyInputComp);
		}
		if (thisEntity.navWaypointComp != null) {
			thisEntity.navWaypointComp = (NavigationWaypointComponent)Instantiate (thisEntity.navWaypointComp);
		}
		if (thisEntity.factoryComp != null) {
			thisEntity.factoryComp = (FactoryComponent)Instantiate (thisEntity.factoryComp);
		}
		if (thisEntity.lSysComp != null) {
			thisEntity.lSysComp = (LSystemComponent)Instantiate (thisEntity.lSysComp);
		}
		if (thisEntity.lSysFacComp != null) {
			thisEntity.lSysFacComp = (LSystemFactoryComponent)Instantiate (thisEntity.lSysFacComp);
		}

	}







}
