using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


[CreateAssetMenu]
public class LSystemComponent : ScriptableObject {

	public LSystemRule[] Rules;
	public LSystemAxiom Axiom;
	//public LSystemOutputObject[] Output;

	//[System.Serializable]
	public List<string> DNA;
	public int DNACycle;
	//[System.Serializable]
	public string NewDNA;

	public string PrintDNA;
	public int CycleStep = 0;
	public int MaxCycle = 5;
	public float CycleTime = 0.1f;
	public bool NeedsDrawing = false;
	public bool NeedsGenerating = true;
	public bool IsGenerating = false;



}
