using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif

[CreateAssetMenu]
public class LSystemFactoryComponent : ScriptableObject {

	public GameObject FactoryPrefabs_A;
	public GameObject FactoryPrefabs_B;
	public GameObject FactoryPrefabs_C;

	public List<GameObject> Children;

	//public Vector2 position = new Vector2(0,0);

	//the angle to step by;
	//public float Angle = 30f;


	public bool IsDrawing = false;
	public int CycleStep = 0;
	public int MaxCycle;
	public float CycleTime = 0.1f;


	public CellDNA FactoryAxiom;


	public CellDNA CurrentCell;
	[SerializeField]
	public List<CellDNA> InstructionList;

	public Stack<CellDNA> InstructionStack = new Stack<CellDNA>();

//
//	void OnEnable()
//	{
//		
//	}
}
