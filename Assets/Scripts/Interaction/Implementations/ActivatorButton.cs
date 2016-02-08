using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivatorButton : AbstractActivator
{
	/* ==== Public variables ==== */
	
	/* ==== Private variables ==== */ 	
	
	/* ==== Start function ==== */
	void Start ()
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{
		base.OnStart();
	}
	
	/* ==== IActivator function ==== */ 	 
	protected override bool Activator(GameObject callingObject, bool state)
	{
		if(this.IsActivatorAuthorized(callingObject))
		{
			if(activatorState)
			{
				if(canBeTurnedOff)
				{
					activatorState = false;
					foreach(AbstractActivable activable in activableTargets)
					{
						if(activable != null)
							activable.OnActivate(this, false);				
					}				
					return true;
				}
			}
			else
			{
				activatorState = true;
				foreach(AbstractActivable activable in activableTargets)
				{
					if(activable != null)
						activable.OnActivate(this, true);				
				}				
				return true;
			}			
		}		
		return false;
	}
}
