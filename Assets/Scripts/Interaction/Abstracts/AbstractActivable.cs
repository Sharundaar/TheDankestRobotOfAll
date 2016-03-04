using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractActivable : define an abstract class for activable objects */
abstract public class AbstractActivable : MonoBehaviour, IActivable
{
	/* ==== Public variables ==== */
	public List<AbstractActivator> activators = new List<AbstractActivator>();
	
	/* ==== Private variables ==== */ 			
	protected bool activateState = false;
	protected bool canBeTurnedOff = false;
	
	protected int activatorsOnCounter = 0;
	protected bool needAllActivatorsOn = true;
	
	/* Used for synchronized activators */
	protected float startActivationTime = 0.0f;
	protected float timeBeforeDesactivation = 36000;	
	
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
		if(activateState && canBeTurnedOff)
		{
			if((startActivationTime + timeBeforeDesactivation) < Time.time)
			{
				this.OnActivate(lastCallingObject, false);
			}
		}
	}
	
	/* ==== IActivable functions ==== */ 	 
	public bool OnActivate(AbstractActivator activatorObject, bool state)
	{				
		if(!(canBeTurnedOff == false && state == false))
		{
			if(state)
				activatorsOnCounter++;
			else
				activatorsOnCounter++;
			
			if(!needAllActivatorsOn || activatorsOnCounter == activators.Count)
			{
				/* We try to activate and inform the calling object whether the activation has been done */
				bool hasBeenActivated = this.Activate(activatorObject, state);
			
				if(hasBeenActivated)
				{
					activateState = state;
					lastCallingObject = callingObject;
					
					if(state)
					{
						startActivationTime = Time.time;					
					}
					
					else
					{
						foreach(AbstractActivator activator in activators)
						{
							activator.turnOff();
						}
					}
				}				
				return hasBeenActivated;
			}			
			return true;
		}		
		return false;
	}
	
	protected abstract bool Activate(AbstractActivator activatorObject, bool state);
	
	/* ==== Authorized Objects functions ==== */
	public bool IsActivationAuthorized(AbstractActivator activatorObject)
	{
		//return this.activators.Contains(activatorObject);
		return true;
	}
}
