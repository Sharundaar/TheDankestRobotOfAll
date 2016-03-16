using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (ActivatorButton))]
public class InteractivableButton : AbstractInteractivable
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
		
	/* ==== IInteractivable function ==== */ 	 
	protected override bool Interact(GameObject callingObject, bool state)
	{
		if(this.IsInteractionAuthorized(callingObject))
		{				
			ActivatorButton activatorButton = this.GetComponent<ActivatorButton>();
			
			if(activatorButton != null)
				activatorButton.OnActivator(this.gameObject, state);	
			else
				Debug.Log("ActivatorButton is null");
			
			return true;
		}		
		return false;
	}
	
	/*protected string InteractionDisplayText(int status)
	{
		
	}*/
}
