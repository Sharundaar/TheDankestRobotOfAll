using UnityEngine;
using System.Collections;

public class AutoSinMovement : MonoBehaviour {

    [SerializeField]
    float m_amplitude = 1.0f;

    [SerializeField]
    float m_frequency = 1.0f;

    [SerializeField]
    Vector3 m_direction = Vector3.right;

    Vector3 m_initialPosition;

	// Use this for initialization
	void Start () {
        m_initialPosition = transform.position;
        m_direction = m_direction.normalized;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = m_initialPosition + Mathf.Sin(Time.time * m_frequency * (Mathf.PI * 2.0f)) * m_direction * m_amplitude;
	}
}
