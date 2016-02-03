using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractInteractivable : define an abstract class for interactivable objects */
abstract public class AbstractInteractivable : MonoBehaviour, IInteractivable 
{
	/* ==== Public variables ==== */
	
	/* ==== Private variables ==== */ 	
	protected List<Object> authorizedObjectList = new List<Object>(); 
	
	protected bool canScientificInteract = true;
	protected bool canRobotInteract = true;
	
	/* ==== Start function ==== */
	void Start () 
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{		
	}
	
	/* ==== IInteractivable functions ==== */ 	 
	public bool OnInteract(Object callingObject)
	{		
		/* If we don't know who's calling or if the calling object isn't authorized to interact, we return false */
		/*if(callingObject == null || !IsAuthorized(callingObject))
			return false;*/
		
		/* Else, we try to interact and inform the calling object whether the interaction has been done */
		return this.Interact(callingObject);
	}
	
	protected abstract bool Interact(Object callingObject);
	
	/* ==== Authorized Objects functions ==== */
	public bool IsAuthorized(Object callingObject)
	{
		return authorizedObjectList.Contains(callingObject);
	}
}
