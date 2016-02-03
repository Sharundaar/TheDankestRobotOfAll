using UnityEngine;
using System.Collections;

public class InteractivableCube : AbstractInteractivable 
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
	
	/* ==== Interact function ==== */ 	 
	protected override bool Interact(Object callingObject)
	{
		Debug.Log("HasInteract");
		return true;
	}
}
