using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class LevelSpawner : MonoBehaviour {

	public GameObject SystemsManager;



	public MovementSystem movementSystem;
	public KeyboardInputSystem keyboardInputSystem;
	public MouseInputSystem mouseInputSystem;
	public FactorySystem factorySystem;
	public LSystemSystem lSystemSystem;
	public LSystemFactorySystem lSystemFactorySystem;


	public EntityPool entityPool;


	public List<ECS_System> systems;




	//public GameObject pref;
	public FactoryInstruction[] factoryInstructions;



	public void OnEnable()
	{
		InstantiateSystemsManager ();



	}

	void Start()
	{

		factorySystem.SpawnEntities (factoryInstructions);


	}

	private void InstantiateSystemsManager ()
	{
		SystemsManager = Instantiate(new GameObject());
		SystemsManager.name = "SystemsManager";


		movementSystem = SystemsManager.AddComponent<MovementSystem>();
		keyboardInputSystem = SystemsManager.AddComponent<KeyboardInputSystem>();
		mouseInputSystem = SystemsManager.AddComponent<MouseInputSystem>();
		factorySystem = SystemsManager.AddComponent<FactorySystem>();
		lSystemSystem = SystemsManager.AddComponent<LSystemSystem>();
		lSystemFactorySystem = SystemsManager.AddComponent<LSystemFactorySystem>();

		entityPool = SystemsManager.AddComponent<EntityPool>();
		entityPool.InitialiseEntitiesList ();


		movementSystem.entityPool = entityPool;
		keyboardInputSystem.entityPool = entityPool;
		mouseInputSystem.entityPool = entityPool;
		factorySystem.entityPool = entityPool;
		lSystemSystem.entityPool = entityPool;
		lSystemFactorySystem.entityPool = entityPool;

	}



}