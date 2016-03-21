using UnityEngine;
using System;

[ExecuteInEditMode]
public class LavaBasic : LethalZone
{
	void Start()
	{
		this.timeScalingFactor = 1.0f / 60.0f;
	}
	
	void Update()
	{
		base.OnUpdate();
	}
	
    void OnTriggerEnter(Collider colliding) 
	{
		if(this.checkpoint != null)
		{
			PlayerTypeComponent playerTypeComponent = colliding.gameObject.GetComponent<PlayerTypeComponent>();			
			
			Vector3 newPosition = this.checkpoint.transform.position;
			Quaternion newRotation = this.checkpoint.transform.rotation;
			
			if(playerTypeComponent != null)
			{
				if(playerTypeComponent.Type == PlayerType.SCIENTIST)
				{
					colliding.gameObject.transform.position = newPosition;
					colliding.gameObject.transform.rotation = newRotation;
				}
			}			
		}
    }
}