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
	public Transform holdingTransform = null;
	
	/* ==== Private variables ==== */ 	
	private Text interactTextField = null;
	
	private bool isHoldingCube = false;
	private GameObject holdingCube = null;
	
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
		Camera fpsCamera = this.GetComponentInChildren<Camera>();
		if(fpsCamera != null)
		{			
			bool interactTextFieldVisibility = false;
		
			RaycastHit hitInfo;							
			if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.TransformDirection(Vector3.forward), out hitInfo, 3)) 
			{
				IInteractivable interactivableObject = hitInfo.collider.gameObject.GetComponent<IInteractivable>();

				if(interactivableObject != null)
				{
					if(this.isHoldingCube)
					{
						if(hitInfo.collider.gameObject == this.holdingCube)
						{
							if(Input.GetButtonDown("Interact"))		
							{
								bool hasInteract = interactivableObject.OnInteract(this.gameObject, false);											
							}						
							else
							{
								interactTextFieldVisibility = true;				
								interactTextField.text = "Press E to Release";										
							}	
						}			
					}
					else
					{
						if(Input.GetButtonDown("Interact"))		
						{
							bool hasInteract = interactivableObject.OnInteract(this.gameObject, true);											
						}		
						else
						{
							interactTextFieldVisibility = true;		
							interactTextField.text = "Press E to Interact";
						}			
					}
				}
			}
			
			this.SetInteractTextFieldVisibility(interactTextFieldVisibility);
		}				
	}
	
	/* ==== Interactions related functions ==== */
	public Transform GetHoldingTransform()
	{
		return this.holdingTransform;
	}
	public void SetHoldingCube(GameObject holdingCube, bool state)
	{
		this.isHoldingCube = state;
		this.holdingCube = holdingCube;
	}
	
	/* ==== UI functions ==== */
	private void SetInteractTextFieldVisibility(bool visibility)
	{			
		if(this.interactTextField != null)
			this.interactTextField.enabled  = visibility;
	}
}
