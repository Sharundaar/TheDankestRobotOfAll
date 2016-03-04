using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractInteractivable : define an abstract class for interactivable objects */
abstract public class AbstractInteractivable : MonoBehaviour, IInteractivable 
{
	/* ==== Public variables ==== */
	
	/* ==== Private variables ==== */ 		
	protected bool canScientificInteract = true;
	protected bool canRobotInteract = true;
	
	protected bool interactionState = false;
	
	/* ==== Start function ==== */
	void Start () 
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{		
	}
	
	protected void OnUpdate()
	{		
	}
	
	/* ==== IInteractivable functions ==== */ 	 
	public bool OnInteract(GameObject callingObject, bool state)
	{				
		/* We try to interact and inform the calling object whether the interaction has been done */
		return this.Interact(callingObject, state);
	}
	protected abstract bool Interact(GameObject callingObject, bool state);
		
	/*public string GetInteractionDisplayText()
	{
		return this.InteractionDisplayText();
	}
	protected abstract string InteractionDisplayText();*/
	
	/* ==== Authorized Objects functions ==== */
	public bool IsInteractionAuthorized(GameObject callingObject)
	{
		return true;
	}

}
