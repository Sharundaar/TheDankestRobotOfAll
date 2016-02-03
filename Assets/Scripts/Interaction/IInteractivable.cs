using UnityEngine;
using System.Collections;

/* Interface Interactivable : used for objects players can interact with */
interface IInteractivable 
{
	bool OnInteract(Object callingObject); 
	bool IsAuthorized(Object callingObject);
}
