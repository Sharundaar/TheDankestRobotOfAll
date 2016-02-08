using UnityEngine;
using System.Collections;

/* Interface Activable : used for objects which can be activated */
interface IActivable 
{
	bool OnActivate(AbstractActivator activatorObject, bool state); 
	bool IsActivationAuthorized(AbstractActivator activatorObject);
}
