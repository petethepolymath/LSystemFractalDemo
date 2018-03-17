using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LSystemFactorySystem : MonoBehaviour , ECS_System {

	public EntityPool entityPool;

	private ECS_Entity thisEntity;
	private CellDNA thisInstruction;
	private GameObject thisSpawn;
	private Renderer thisSpawnRenderer;
	private Color newColour;
	private System.Random random = new System.Random();
	private String switchString;
	private float CheckForNewComponentsCycleTime = 0.5f;
	private int thisCycle;


	public void InjectEntityPool(EntityPool entityPoolToInject)
	{
		entityPool = entityPoolToInject;
	}

	void Start()
	{

		StartCoroutine (CheckForNewComps ());
	}





	IEnumerator CheckForNewComps()
	{
		while (enabled)
		{
			yield return new WaitForSeconds ((float)GetRandomDouble(random, (double)(CheckForNewComponentsCycleTime* 0.5f), (double)(CheckForNewComponentsCycleTime * 1.5f)));

			StartEachCoroutineLoop ();
			random = new System.Random();
		}

	}





	void StartEachCoroutineLoop()
	{
		if (entityPool != null) {
			for (int entityId = 0; entityId < entityPool.Entities.Count; entityId++) {
				if (((entityPool.Entities [entityId].lSysComp) != null) && ((entityPool.Entities [entityId].lSysFacComp) != null) && ((entityPool.Entities [entityId].posComp) != null)) {

					if (entityPool.Entities [entityId].lSysComp.NeedsDrawing == true) {

						if (entityPool.Entities [entityId].lSysFacComp.IsDrawing != true) {
							
							StartCycleAndInitialise (entityId, entityPool.Entities [entityId]);

						}
					}
				}
			}
		}
	}

	void StartCycleAndInitialise(int entityId, ECS_Entity thisEntity)
	{
		
		InitialiseThisEntity (entityId, thisEntity);

		StartCoroutine (CycleLoop (entityId, thisEntity));
	}

	void InitialiseThisEntity(int entityId, ECS_Entity thisEntity)
	{
		Debug.Log ("fac axiom: " + thisEntity.lSysFacComp.FactoryAxiom); 
		thisEntity.lSysFacComp.FactoryAxiom = Instantiate(thisEntity.lSysFacComp.FactoryAxiom );
		thisEntity.lSysFacComp.FactoryAxiom.position = thisEntity.posComp.position;
		thisEntity.lSysFacComp.FactoryAxiom.Angle = (float)GetRandomDouble (random, 0, 360);
		Debug.Log ("fac axiom prepush: " + thisEntity.lSysFacComp.FactoryAxiom); 

		thisEntity.lSysFacComp.InstructionStack.Push (thisEntity.lSysFacComp.FactoryAxiom);
		Debug.Log ("fac axiom post push : " + thisEntity.lSysFacComp.FactoryAxiom); 

		thisEntity.lSysFacComp.CycleStep = 0;
		thisEntity.lSysFacComp.MaxCycle = thisEntity.lSysComp.DNA[thisEntity.lSysComp.DNA.Count-1].Length;
		thisEntity.lSysFacComp.IsDrawing = true;


	}


	IEnumerator CycleLoop(int entityId, ECS_Entity thisEntity)
	{
		while (thisEntity != null && thisEntity.lSysComp.NeedsDrawing == true && thisEntity.lSysFacComp.IsDrawing == true )
		{
			yield return new WaitForSeconds (thisEntity.lSysFacComp.CycleTime + (entityId / 100));
			//+ (entityId / 1000)
			Iterate (entityId, thisEntity);
		}

	}

	//iterate through all entities that have correct components
	void Iterate(int entityId, ECS_Entity thisEntity)
	{
		thisCycle = thisEntity.lSysFacComp.CycleStep;

		if(thisEntity.lSysFacComp.CycleStep >= thisEntity.lSysFacComp.MaxCycle)
		{
			thisEntity.lSysComp.NeedsDrawing = false;
			thisEntity.lSysFacComp.IsDrawing = false;
			return;
		}



		thisInstruction = InstantiateInstructionInList (thisEntity, thisEntity.lSysFacComp.InstructionList, thisCycle);


			RandomiseThisInstruction (thisInstruction);

			Debug.Log ("thisCycle : " + thisCycle);
		Debug.Log ("This String : " + thisEntity.lSysComp.DNA[thisEntity.lSysComp.DNA.Count- 1].ToString ());

			//Debug.Log ("DNA Length : " + thisEntity.lSysComp.DNA.Length);
		switchString = thisEntity.lSysComp.DNA[thisEntity.lSysComp.DNA.Count- 1][thisCycle].ToString ();

			switch (switchString) 
			{

				case ">":
					thisInstruction.Angle += thisInstruction.AngleChange;
					break;

				case "<":
					thisInstruction.Angle -= thisInstruction.AngleChange;
					break;

				case "F":

					Vector2 debugpos = (AngleHelper.Vector2FromAngle (thisInstruction.Angle).normalized * thisInstruction.Length);
						//Debug.Log ("vector change: " + debugpos);
					thisInstruction.position += debugpos;
					break;

				case "+":
					thisInstruction.AngleChange += 5f;
					break;

				case "-":
					thisInstruction.AngleChange -= 5f;
					break;

				case "[":
					thisEntity.lSysFacComp.InstructionStack.Push (Instantiate (thisInstruction));
					break;

				case "]":
					thisEntity.lSysFacComp.InstructionList[thisCycle] = thisEntity.lSysFacComp.InstructionStack.Pop();
					break;

				case "O":
					thisInstruction.Size *= 1.2f;
					break;
				case "o":
					thisInstruction.Size *= 0.8f;
					break;






				case "A":
						
					if (thisEntity.lSysFacComp.FactoryPrefabs_A != null) 
					{
					thisSpawn = (GameObject)Instantiate (thisEntity.lSysFacComp.FactoryPrefabs_A, new Vector3 (thisInstruction.position.x, thisInstruction.position.y), Quaternion.identity, thisEntity.trans);
					thisEntity.lSysFacComp.Children.Add(thisSpawn);

					PaintGameObject (thisSpawn, thisInstruction);
					}

					break;

				case "B":
				
					if (thisEntity.lSysFacComp.FactoryPrefabs_B != null) 
					{
						thisSpawn = (GameObject)Instantiate (thisEntity.lSysFacComp.FactoryPrefabs_B, new Vector3 (thisInstruction.position.x, thisInstruction.position.y), Quaternion.identity, thisEntity.trans);
						thisEntity.lSysFacComp.Children.Add(thisSpawn);

						PaintGameObject (thisSpawn, thisInstruction);
					}
					break;

				case "C":
					
					if (thisEntity.lSysFacComp.FactoryPrefabs_A != null) 
					{
						thisSpawn = (GameObject)Instantiate (thisEntity.lSysFacComp.FactoryPrefabs_A, new Vector3 (thisInstruction.position.x, thisInstruction.position.y), Quaternion.identity, thisEntity.trans);
						thisEntity.lSysFacComp.Children.Add(thisSpawn);

						PaintGameObject (thisSpawn, thisInstruction);
					}
					break;


				
			break;

			}
		

		thisEntity.lSysFacComp.CycleStep += 1;
	}

	public void PaintGameObject(GameObject thisSpawn, CellDNA thisInstruction )
	{
		thisSpawn.transform.localScale = new Vector3 (thisInstruction.Size, thisInstruction.Size, thisInstruction.Size);
			
		if (thisSpawn.GetComponent<Renderer> () == true) {
			thisSpawnRenderer = thisSpawn.GetComponent<Renderer> ();
			newColour = Color.HSVToRGB (thisInstruction.Hue, thisInstruction.Saturation, thisInstruction.Value);
			newColour = new Color(newColour.r, newColour.g, newColour.b, thisInstruction.Alpha);
			thisSpawnRenderer.material.color = newColour;
		}
	}

	public CellDNA InstantiateInstructionInList (ECS_Entity thisEntity, List<CellDNA> thisInstructionList, int thisCycle)
	{
		
		if (thisCycle == 0) 
		{
			

			thisInstructionList.Clear ();
			thisInstructionList.Add (Instantiate(thisEntity.lSysFacComp.InstructionStack.Peek()));


		}
		else 
		{
			thisInstructionList.Add (Instantiate(thisEntity.lSysFacComp.InstructionList[thisCycle-1]));

		}
		thisInstructionList [thisCycle].name = "Instruction";
		return thisInstructionList [thisCycle];
	}

	float Randomise(CellDNA thisInstruction)
	{

		float rand = (float)GetRandomDouble (random, (1f - thisInstruction.Randomise), (1f + thisInstruction.Randomise));
		return rand;

//		//return a random value as a percentage to multiply something by. IE 90% to 110%
//		Random.InitState((int)Time.timeSinceLevelLoad);
//		float rand = Random.Range(1f - thisInstruction.Randomise, 1f + thisInstruction.Randomise);
//		Debug.Log ("rand = " + rand);
//		

	
	}

	void RandomiseThisInstruction (CellDNA thisInstruction)
	{
		thisInstruction.Hue *= Randomise (thisInstruction);
		thisInstruction.Value *= Randomise (thisInstruction);
		thisInstruction.Saturation *= Randomise (thisInstruction);
		thisInstruction.Size *= Randomise (thisInstruction);
		thisInstruction.Angle *= Randomise (thisInstruction);
	}



	double GetRandomDouble(System.Random random, double min, double max)
	{
		double rand = min + (random.NextDouble() * (max - min));
		return rand;
		Debug.Log ("rand = " + rand);
	}


//	void ClearCurrentChildren(ECS_Entity thisEntity)
//	{
//		if(thisEntity.lSysFacComp.Children.Count > 0)
//		{
//			for (int i = 0; i < thisEntity.lSysFacComp.Children.Count; i++) 
//			{
//				Destroy (thisEntity.lSysFacComp.Children [i]);
//
//			}
//
//			thisEntity.lSysFacComp.Children.Clear ();
//		}
//	}

}
