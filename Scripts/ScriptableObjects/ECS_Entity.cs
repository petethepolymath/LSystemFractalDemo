using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif



[CreateAssetMenu]
public class ECS_Entity : ScriptableObject {

	public GameObject gameObj;
	public Transform trans ;

	public PositionComponent posComp;

	public MovementComponent movComp;

	public MouseInputComponent mouseInputComp;

	public KeyboardInputComponent keyInputComp;

	public NavigationWaypointComponent navWaypointComp;

	public FactoryComponent factoryComp;


	public LSystemComponent lSysComp;
	//[ExpandableAttribute]
	public LSystemFactoryComponent lSysFacComp;







}
