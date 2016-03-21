using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class InteractionManager : NetworkBehaviour
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

    [SyncVar]
    private GameObject ms_currentInteractionObject = null;

    private AbstractInteractivable currentInteractionObject { get { return ms_currentInteractionObject.GetComponent<AbstractInteractivable>(); } set { ms_currentInteractionObject = value != null ? value.gameObject : null; } }
    private float startInteractionTime = 0.0f;

    private bool m_isLocallyControlled = false;
    public bool IsLocallyControlled { get { return m_isLocallyControlled; } set { m_isLocallyControlled = value; } }

    /* ==== Start function ==== */
    void Start () 
	{
        if (!IsLocallyControlled)
            return;

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
						if(Input.GetButtonDown("Interact") && interactivableObject.IsInteractionAuthorized(this.gameObject))		
						{
							currentInteractionObject = interactivableObject;
							
							if(interactivableObject.holdingTimeForActivation > 0.01f)
							{								
								this.interactionState = InteractionState.PendingInteracting;
								this.startInteractionTime = Time.time;								
							}
							else
							{
								this.CmdLaunchTheInteraction();
							}								
						}		
						else
						{
                            if(interactTextField != null)
                            {
							    interactTextFieldVisibility = true;	
							
							    if(interactivableObject.holdingTimeForActivation > 0.01f)
							    {								
								    interactTextField.text = "Hold E to Interact";			
							    }
							    else
							    {
								    interactTextField.text = this.GetStartingInteractionText(interactivableObject);									
							    }		
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
									this.CmdLaunchTheInteraction();								
									interactTextFieldVisibility = false;			
								}		
								else
								{
                                    if(interactTextField != null)
                                    {
									    interactTextFieldVisibility = true;		
									    interactTextField.text = "Keep holding E to Interact";											
                                    }
								}															
							}						
							else
							{
                                CmdStopTheInteraction();
							}	
						}
						else
						{
                            CmdStopTheInteraction();
						}
					}
					else
					{
						if(Input.GetButtonDown("Interact"))		
						{
                            CmdStopTheInteraction();															
						}						
						else
						{
                            if(interactTextField != null)
                            {
							    interactTextFieldVisibility = true;		
							    interactTextField.text = "Press E to Release";	
                            }
						}	
					}				
				}
				else
				{
                    CmdStopTheInteraction();
				}
			}
			
			this.SetInteractTextFieldVisibility(interactTextFieldVisibility);
		}				
	}
	
    [Command]
	private void CmdLaunchTheInteraction()
	{
		bool hasInteract = this.currentInteractionObject.OnInteract(this.gameObject, true);			
		if(hasInteract)
		{
			if(this.currentInteractionObject.isHoldingInteraction)
			{
				this.interactionState = InteractionState.Holding;										
			}			
		}
		else
		{
			this.currentInteractionObject = null;
		}
	}

    [Command]
	private void CmdStopTheInteraction()
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
	
	private string GetStartingInteractionText(AbstractInteractivable interactivableObject)
	{
		PlayerTypeComponent playerTypeComponent = this.GetComponent<PlayerTypeComponent>();
		if(playerTypeComponent != null)
		{
			PlayerType playerType = playerTypeComponent.Type;
			
			if((playerType == PlayerType.SCIENTIST && interactivableObject.canScientificInteract) 
			|| (playerType == PlayerType.ROBOT && interactivableObject.canRobotInteract))
			{
				return "Press E to Interact";			
			}
			else if(playerType == PlayerType.SCIENTIST && interactivableObject.canRobotInteract)
			{
				return "Only the Robot can Interact with this it";	
			}
			else if(playerType == PlayerType.ROBOT && interactivableObject.canScientificInteract)
			{
				return "Only the Scientific can Interact with it";	
			}
			else
			{
				return "This interaction is not available yet";	
			}
		}
		return "Undefined StartingInteractionText";
	}
}
