using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (DoorComponent))]
public class ActivableDoor : AbstractActivable
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
	
	/* ==== IActivable function ==== */ 	 
	protected override bool Activate(AbstractActivator activatorObject, bool state)
	{
		if(this.IsActivationAuthorized(activatorObject))
		{
			DoorComponent doorComponent = this.GetComponent<DoorComponent>();
			
			if(doorComponent != null)
			{				
				if(state)
					doorComponent.Open();
				else
					doorComponent.Close();			
			}
			else
				Debug.Log("DoorComponent is null");
			
			return true;
		}		
		return false;
	}
}
