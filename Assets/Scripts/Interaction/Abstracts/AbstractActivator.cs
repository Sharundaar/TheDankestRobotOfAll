using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractActivator : define an abstract class for activator objects */
abstract public class AbstractActivator : MonoBehaviour, IActivator 
{
	/* ==== Public variables ==== */
	public List<AbstractActivable> activableTargets = new List<AbstractActivable>();	
	
	public bool canBeTurnedOff = true;	
	
	public bool canScientificActivate = true;
	public bool canRobotActivate = true;	
	
	/* Used for synchronized activators */
	public float timeBeforeDesactivation = 36000;	
		
	/* ==== Private variables ==== */ 			
	protected bool activatorState = false;	
	protected GameObject lastCallingObject = null;
	
	protected float startActivationTime = 0.0f;
	
	/* ==== Start function ==== */
	void Start () 
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{		
	}
	
	void Update () 
	{
		this.OnUpdate();
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
	public bool IsActivatorAuthorized(GameObject callingObject)
	{
		return true;
	}	
	
	public void TurnOff()
	{
		activatorState = false;		
	}
	
	protected abstract bool Activator(GameObject activatorObject, bool state);
}
