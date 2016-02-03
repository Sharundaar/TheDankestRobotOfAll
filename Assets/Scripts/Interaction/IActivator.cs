using UnityEngine;
using System.Collections;

/* Interface Activator : used for objects which can activate other objects (ie using IActivatable) */
interface IActivator 
{
	bool OnActivatorCalled(Object callingObject); 
	bool IsAuthorized(Object callingObject);
}
