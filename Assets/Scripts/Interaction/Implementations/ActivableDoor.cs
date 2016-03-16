using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		
	void Update ()
	{
		this.OnUpdate();
	}
	
	protected void OnUpdate()
	{
		base.OnUpdate();
	}
	
	/* ==== IActivable function ==== */ 	 
	protected override bool Activate(AbstractActivator activatorObject, bool state)
	{
		// AbstractActivator activatorObject
		if(this.IsActivationAuthorized(activatorObject))
		{
			Component[] doorComponents =  GetComponentsInChildren<DoorComponent>();
		
			if(state)
			{
				foreach(DoorComponent doorComponent in doorComponents)
				{
						doorComponent.Open();
				}
			}
			else
			{
				foreach(DoorComponent doorComponent in doorComponents)
				{
						doorComponent.Close();
				}
			}		
			
			return true;
		}		
		return false;
	}
}
