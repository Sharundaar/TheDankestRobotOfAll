using UnityEngine;
using System.Collections;

/* Interface Activator : used for objects which can activate other objects (ie IActivatable objects) */
interface IActivator 
{
	bool OnActivator(GameObject callingObject, bool state); 
	bool IsActivatorAuthorized(GameObject callingObject);
	
	void TurnOff();
}
