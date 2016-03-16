using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractActivable : define an abstract class for activable objects */
abstract public class AbstractActivable : MonoBehaviour, IActivable
{
	/* ==== Public variables ==== */
	public List<AbstractActivator> activators = new List<AbstractActivator>();
	
	public bool canBeTurnedOff = false;	
	public bool needAllActivatorsOn = true;
	
	/* Used for synchronized activators */
	public float timeBeforeDesactivation = 36000;	
	
	/* ==== Private variables ==== */ 			
	protected bool activateState = false;	
	protected AbstractActivator lastActivatorObject = null;
	
	protected int activatorsOnCounter = 0;
	protected float startActivationTime = 0.0f;
	
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
				this.OnActivate(lastActivatorObject, false);
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
				activatorsOnCounter--;
			
			if(state)
			{
				if(!needAllActivatorsOn || activatorsOnCounter == activators.Count)
				{
					/* We try to activate and inform the calling object whether the activation has been done */
					bool hasBeenActivated = this.Activate(activatorObject, state);
					
					if(hasBeenActivated)
					{
						activateState = state;
						lastActivatorObject = activatorObject;
						
						startActivationTime = Time.time;	

						if(!canBeTurnedOff)
						{
							foreach(AbstractActivator activator in activators)
							{
								activator.canBeTurnedOff = false;
							}
						}
					}				
					
					return hasBeenActivated;
				}
			}
			else
			{
				/* We try to activate and inform the calling object whether the activation has been done */
				bool hasBeenActivated = this.Activate(activatorObject, state);
			
				if(hasBeenActivated)
				{
					activateState = state;
					lastActivatorObject = activatorObject;
					
					foreach(AbstractActivator activator in activators)
					{
						activator.TurnOff();
					}
				}				
				return hasBeenActivated;
			}			
			return true;
		}		
		return false;
	}
	public bool IsActivationAuthorized(AbstractActivator activatorObject)
	{
		return this.activators.Contains(activatorObject);
	}
	
	protected abstract bool Activate(AbstractActivator activatorObject, bool state);
}
