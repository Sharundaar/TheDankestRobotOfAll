using UnityEngine;
using System.Collections;

public class InteractivableHoldingCube : AbstractInteractivable 
{
	/* ==== Public variables ==== */
	
	/* ==== Private variables ==== */ 	
	private Transform startParentTransform = null;
	
	/* ==== Start function ==== */
	void Start ()
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{
		base.OnStart();
		this.startParentTransform = this.transform.parent;
	}
	
	/* ==== Interact function ==== */ 	 
	protected override bool Interact(GameObject callingObject, bool state)
	{		
		InteractionManager interactionManager = callingObject.GetComponent<InteractionManager>();
		
		if(interactionManager != null)
		{					
			if(!state)
			{
				this.GetComponent<Rigidbody>().useGravity = true;
				this.GetComponent<Rigidbody>().isKinematic = false;
					
				this.transform.parent = startParentTransform;
				
				interactionManager.SetHoldingCube(this.gameObject, false);
				
				return true;
			}
			else
			{		
				Transform holdingTransform = interactionManager.GetHoldingTransform();
				
				if(holdingTransform != null)
				{
					Vector3 tmpPosition = new Vector3(0.0f, 0.0f, 0.0f);
					
					this.GetComponent<Rigidbody>().useGravity = false;
					this.GetComponent<Rigidbody>().isKinematic = true;
					
					this.transform.parent = holdingTransform;					
					this.transform.localPosition = tmpPosition;
			
					interactionManager.SetHoldingCube(this.gameObject, true);
			
					return true;
				}
			}
		}
		
		return false;
	}
}
