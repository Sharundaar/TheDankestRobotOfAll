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
			if(!state)
			{
				Debug.Log("Catching the Activator(false) for the pressure plate.");
				return false;
			}
			else
				Debug.Log("Activator(true).");
			
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
