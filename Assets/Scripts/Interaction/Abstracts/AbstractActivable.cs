using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Class AbstractActivable : define an abstract class for activable objects */
abstract public class AbstractActivable : MonoBehaviour, IActivable
{
	/* ==== Public variables ==== */
	public List<AbstractActivator> activators = new List<AbstractActivator>();
	
	/* ==== Private variables ==== */ 		
	protected bool canScientificActivate = true;
	protected bool canRobotActivate = true;
	
	protected bool activateState = false;
	
	/* ==== Start function ==== */
	void Start () 
	{
		this.OnStart();
	}
	
	protected void OnStart()
	{		
		
	}
	
	/* ==== IActivable functions ==== */ 	 
	public bool OnActivate(AbstractActivator activatorObject, bool state)
	{				
		/* We try to activate and inform the calling object whether the activation has been done */
		return this.Activate(activatorObject, state);
	}
	
	protected abstract bool Activate(AbstractActivator activatorObject, bool state);
	
	/* ==== Authorized Objects functions ==== */
	public bool IsActivationAuthorized(AbstractActivator activatorObject)
	{
		//return this.activators.Contains(activatorObject);
		return true;
	}
}
