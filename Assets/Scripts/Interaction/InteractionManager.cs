using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionManager : MonoBehaviour 
{
	/*
	 * TODO : 
	 *		- Move the UI things in a manager 
	 *		- Test the raycast range when eveything will be in place (can bug with small or on-ground objects)
	 */
	
	/* ==== Public variables ==== */
	
	/* ==== Private variables ==== */ 	
	private Text interactTextField = null;
	
	/* ==== Start function ==== */
	void Start () 
	{
		Text[] textFieldsArray = FindObjectsOfType(typeof(Text)) as Text[];
		foreach(Text tf in textFieldsArray)
		{
			if(tf.name.Equals("InteractTextField"))
				this.interactTextField = tf;				
		}
	}
	
	/* ==== Update function ==== */
	void Update () 
	{		
		Camera c = this.GetComponentInChildren<Camera>();
		if(c != null)
		{			
			bool interactTextFieldVisibility = false;
		
			RaycastHit hitInfo;							
			if (Physics.Raycast(c.transform.position, c.transform.TransformDirection(Vector3.forward), out hitInfo, 3)) 
			{
				IInteractivable interactivableObject = hitInfo.collider.gameObject.GetComponent<IInteractivable>();

				if(interactivableObject != null)
				{
					if(Input.GetButtonDown("Interact"))		
					{
						bool hasInteract = interactivableObject.OnInteract(this.gameObject);						
					}
					else
						interactTextFieldVisibility = true;
				}
			}
			
			this.SetInteractTextFieldVisibility(interactTextFieldVisibility);
		}				
	}
	
	/* ==== UI functions ==== */
	private void SetInteractTextFieldVisibility(bool visibility)
	{			
		if(this.interactTextField != null)
			this.interactTextField.enabled  = visibility;
	}
}
