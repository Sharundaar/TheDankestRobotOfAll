using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractActivator : define an abstract class for activator objects */
abstract public class AbstractActivator : MonoBehaviour, IActivator 
{
	/* ==== Public variables ==== */
	public List<AbstractActivable> activableTargets = new List<AbstractActivable>();
	
	/* ==== Private variables ==== */ 		
	protected bool canScientificActivate = true;
	protected bool canRobotActivate = true;
	
	protected bool activatorState = false;
	protected bool canBeTurnedOff = true;
	
	/* ==== Start function ==== */
	void Start () 
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{		
	}
	
	/* ==== IActivator functions ==== */ 	 
	public bool OnActivator(GameObject callingObject, bool state)
	{				
		/* We try to activate and inform the calling object whether the activation has been done */
		return this.Activator(callingObject, state);
	}
	
	protected abstract bool Activator(GameObject callingObject, bool state);
	
	/* ==== Authorized Objects functions ==== */
	public bool IsActivatorAuthorized(GameObject callingObject)
	{
		return true;
	}
}
