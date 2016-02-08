using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (PressurePlateAnimation))]
public class ActivatorPressurePlate : AbstractActivator
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
			foreach(AbstractActivable activable in activableTargets)
			{
				if(activable != null)
					activable.OnActivate(this, state);				
			}
			
			return true;
		}		
		return false;
	}
}
