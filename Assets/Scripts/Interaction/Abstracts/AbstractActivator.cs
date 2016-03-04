using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractActivator : define an abstract class for activator objects */
abstract public class AbstractActivator : MonoBehaviour, IActivator 
{
	/* ==== Public variables ==== */
	public List<AbstractActivable> activableTargets = new List<AbstractActivable>();	
		
	/* ==== Private variables ==== */ 			
	protected bool activatorState = false;		
	protected bool canBeTurnedOff = true;
	
	protected GameObject lastCallingObject = null;
	
	protected bool canScientificActivate = true;
	protected bool canRobotActivate = true;
	
	/* Used for synchronized activators */
	protected float startActivationTime = 0.0f;
	protected float timeBeforeDesactivation = 36000;	
	
	/* Used for holding activators */
	protected float holdingTimeForActivation = 0.0f;
	
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
		if(activatorState && canBeTurnedOff)
		{
			if((startActivationTime + timeBeforeDesactivation) < Time.time)
			{
				this.OnActivator(lastCallingObject, false);
			}
		}
	}
	
	/* ==== IActivator functions ==== */ 	 
	public bool OnActivator(GameObject callingObject, bool state)
	{				
		if(!(canBeTurnedOff == false && state == false))
		{
			/* We try to activate and inform the calling object whether the activation has been done */
			bool hasBeenActivated = this.Activator(callingObject, state);
			
			if(hasBeenActivated)
			{
				activatorState = state;
				lastCallingObject = callingObject;
				
				if(activatorState)	
				{
					startActivationTime = Time.time;					
				}
				
				return true;
			}			
		}		
		return false;
	}
	
	public void turnOff()
	{
		activatorState = false;		
	}
	
	protected abstract bool Activator(GameObject callingObject, bool state);
	
	/* ==== Authorized Objects functions ==== */
	public bool IsActivatorAuthorized(GameObject callingObject)
	{
		return true;
	}
}
