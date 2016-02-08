using UnityEngine;
using System.Collections;

/* Interface Interactivable : used for objects players can interact with */
interface IInteractivable 
{
	bool OnInteract(GameObject callingObject, bool state); 
	bool IsInteractionAuthorized(GameObject callingObject);
	
	//string GetInteractionDisplayText();
}
