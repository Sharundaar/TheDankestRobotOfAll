using UnityEngine;
using System;

[ExecuteInEditMode]
public class LethalZone : MonoBehaviour
{
	/* Private variables */
	protected float timeScalingFactor = 1.0f / 20.0f;
	
	/* Public variables */
	public CheckpointComponent checkpoint = null;
	
	void Update()
	{
		this.OnUpdate();
	}
	
	protected void OnUpdate()
	{
		Renderer r = GetComponent<Renderer>();
		if (!r)
		{
			return;
		}
		Material mat = r.sharedMaterial;
		if (!mat)
		{
			return;
		}

		Vector4 waveSpeed = mat.GetVector("WaveSpeed");
		float waveScale = mat.GetFloat("_WaveScale");
		float t = Time.time * timeScalingFactor;

		Vector4 offset4 = waveSpeed * (t * waveScale);
		Vector4 offsetClamped = new Vector4(Mathf.Repeat(offset4.x, 1.0f), Mathf.Repeat(offset4.y, 1.0f),
			Mathf.Repeat(offset4.z, 1.0f), Mathf.Repeat(offset4.w, 1.0f));
		mat.SetVector("_WaveOffset", offsetClamped);			
	}
}