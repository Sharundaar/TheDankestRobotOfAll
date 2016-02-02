using UnityEngine;
using System.Collections;

/* Interface Activable : used for objects which can be activated */
interface IActivable 
{
	bool OnActivate(Object callingObject); 
	bool IsAuthorized(Object callingObject);
}
