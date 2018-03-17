using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class LSystemSystem : MonoBehaviour , ECS_System
{
	

	public EntityPool entityPool;

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
//
//		if (entityPool != null) 
//		{
//
//			for (int i = 0; i < entityPool.Entities.Count; i++) 
//			{
//				InitialiseLsystemComponent (entityPool.Entities [i]);
//			}
//
//			StartCoroutine (CycleLoop ());
//
//		}
//	}

	IEnumerator CheckForNewComps()
	{
		while (enabled)
		{
			yield return new WaitForSeconds (CheckForNewComponentsCycleTime);

			StartEachCoroutineLoop ();

		}

	}
	//for (int i = 0; i < entityPool.Entities.Count; i++) 
		//			{
		//				InitialiseLsystemComponent (entityPool.Entities [i]);
		//			}
		//
		//			StartCoroutine (CycleLoop ());

	void StartEachCoroutineLoop()
	{
		if (entityPool != null) 
		{
			for (int entityId = 0; entityId < entityPool.Entities.Count; entityId++) 
			{
				if ((entityPool.Entities [entityId].lSysComp) != null) 
				{

					if (entityPool.Entities [entityId].lSysComp.NeedsGenerating == true) 
					{

						if (entityPool.Entities [entityId].lSysComp.IsGenerating != true) 
						{

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

	void InitialiseThisEntity (int entityId, ECS_Entity thisEntity)
	{
		if (((thisEntity.lSysComp) != null)) {
			thisEntity.lSysComp.CycleStep = 0;
			thisEntity.lSysComp.DNA[0] = thisEntity.lSysComp.Axiom.Axiom;
			thisEntity.lSysComp.DNACycle = 0;


			thisEntity.lSysComp.IsGenerating = true;
			Debug.Log ("lSysComp.IsGenerating : " + thisEntity.lSysComp.IsGenerating);
		}
	}




	IEnumerator CycleLoop(int entityId, ECS_Entity thisEntity)
	{
		while (thisEntity != null && thisEntity.lSysComp.NeedsGenerating == true && thisEntity.lSysComp.IsGenerating == true )
		{
			yield return new WaitForSeconds (thisEntity.lSysComp.CycleTime + (entityId / 10));

			Iterate (entityId, thisEntity);
		}

	}

	//find entities with components for this role
	void Iterate(int entityId, ECS_Entity thisEntity)
	{
		if(thisEntity.lSysComp.CycleStep >= thisEntity.lSysComp.MaxCycle)
		{
			thisEntity.lSysComp.NeedsGenerating = false;
			thisEntity.lSysComp.IsGenerating = false;
			thisEntity.lSysComp.NeedsDrawing = true;
			return;
		}


			
		for (int rule = 0; rule < thisEntity.lSysComp.Rules.Length; rule++) 
		{
	
			{
					ApplyProductionRule (thisEntity, thisEntity.lSysComp.Rules[rule]);

			}	


		}
	}

	void ApplyProductionRule(ECS_Entity thisEntity, LSystemRule rule )
	{
		//1. Create new blank string
		//2. for each char in old sting check if rule applies
		//3.if rule applies then add rule.output to new string.
		//4. if rule does not apply then add old char to new string



			//Add new version of the whole string
		thisEntity.lSysComp.CycleStep += 1;
		thisEntity.lSysComp.DNA.Add("");


		//Debug.Log("CycleStep : " + thisEntity.lSysComp.CycleStep + "DNA.Count : " + thisEntity.lSysComp.DNA[thisEntity.lSysComp.DNA.Count] );


		for (int charIndex = 0; charIndex < thisEntity.lSysComp.DNA [thisEntity.lSysComp.CycleStep-1].Length; charIndex++) 
			{
				if (thisEntity.lSysComp.DNA [thisEntity.lSysComp.CycleStep - 1] [charIndex].ToString () == rule.Input)
				{
					thisEntity.lSysComp.DNA [thisEntity.lSysComp.CycleStep] += rule.Output;
				} 
				else 
				{
				thisEntity.lSysComp.DNA [thisEntity.lSysComp.CycleStep] += (thisEntity.lSysComp.DNA [thisEntity.lSysComp.CycleStep - 1] [charIndex].ToString ());
				}
			}
		
	}


//	bool ApplyProductionRules(ECS_Entity thisEntity, char c)
//	{
//
//	//	Debug.Log ("char c : " + c);
//		//Debug.Log ("thisEntity : " + thisEntity);
//		foreach (LSystemRule rule in thisEntity.lSysComp.Rules) 
//		{
////			Debug.Log ("rule : " + rule);
////			Debug.Log ("rule.input : " + rule.Input);
////			Debug.Log ("rule.Output: " + rule.Output);
////			Debug.Log ("c.ToString() : " + c.ToString());
//
//			if (c.ToString() == rule.Input) 
//			{
//				thisEntity.lSysComp.NewDNA += rule.Output;
//				return true;
//			}
//		}
//		return false;
//	}



}
