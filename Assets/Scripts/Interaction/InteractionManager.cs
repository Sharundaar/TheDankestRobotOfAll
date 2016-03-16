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
		
	enum InteractionState 
	{
		NotInteracting, 
		PendingInteracting,
		Holding
	};
	
	/* ==== Public variables ==== */	
	public Transform holdingTransform = null;
	
	/* ==== Private variables ==== */ 	
	private InteractionState interactionState = InteractionState.NotInteracting;
	private Text interactTextField = null;
	
	private AbstractInteractivable currentInteractionObject = null;	
	private float startInteractionTime = 0.0f;
	
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
				AbstractInteractivable interactivableObject = hitInfo.collider.gameObject.GetComponent<AbstractInteractivable>();

				if(interactivableObject != null)
				{
					if(this.interactionState == InteractionState.NotInteracting)
					{
						if(Input.GetButtonDown("Interact"))		
						{
							currentInteractionObject = interactivableObject;
							
							if(interactivableObject.holdingTimeForActivation > 0.01f)
							{								
								this.interactionState = InteractionState.PendingInteracting;
								this.startInteractionTime = Time.time;								
							}
							else
							{
								this.LaunchTheInteraction();
							}								
						}		
						else
						{
							interactTextFieldVisibility = true;	
							
							if(interactivableObject.holdingTimeForActivation > 0.01f)
							{								
								interactTextField.text = "Hold E to Interact";			
							}
							else
							{
								interactTextField.text = "Press E to Interact";									
							}		
						}	
					}
					else if(this.interactionState == InteractionState.PendingInteracting)
					{
						if(interactivableObject == currentInteractionObject)
						{
							if(Input.GetButton("Interact"))		
							{
								if((this.startInteractionTime + interactivableObject.holdingTimeForActivation) < Time.time)
								{
									this.LaunchTheInteraction();								
									interactTextFieldVisibility = false;			
								}		
								else
								{
									interactTextFieldVisibility = true;		
									interactTextField.text = "Keep holding E to Interact";											
								}															
							}						
							else
							{
								StopTheInteraction();
							}	
						}
						else
						{
							StopTheInteraction();
						}
					}
					else
					{
						if(Input.GetButtonDown("Interact"))		
						{
							StopTheInteraction();															
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
					StopTheInteraction();
				}
			}
			
			this.SetInteractTextFieldVisibility(interactTextFieldVisibility);
		}				
	}
	
	private bool LaunchTheInteraction()
	{
		bool hasInteract = this.currentInteractionObject.OnInteract(this.gameObject, true);			
		if(hasInteract)
		{
			if(this.currentInteractionObject.isHoldingInteraction)
			{
				this.interactionState = InteractionState.Holding;										
			}			
		}
		return hasInteract;
	}
	private void StopTheInteraction()
	{
		bool hasInteract = true;
		
		if(this.interactionState == InteractionState.Holding)
			hasInteract = this.currentInteractionObject.OnInteract(this.gameObject, false);			
		
		if(hasInteract)
		{
			this.currentInteractionObject = null;
			this.interactionState = InteractionState.NotInteracting;
		}
	}

	public Transform GetHoldingTransform()
	{
		return this.holdingTransform;
	}
	
	/* ==== UI functions ==== */
	private void SetInteractTextFieldVisibility(bool visibility)
	{			
		if(this.interactTextField != null)
			this.interactTextField.enabled  = visibility;
	}
}
