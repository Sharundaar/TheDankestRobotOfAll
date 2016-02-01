using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PressurePlateAnimation : MonoBehaviour {

    [SerializeField]
    private float m_AnimationTime = 1.0f;

    [SerializeField]
    private float m_TriggerMass = 1.0f;

    [SerializeField]
    private Vector3 m_EndPosition;

    private Vector3 m_StartPosition;

    public bool IsActive { get; private set; }

    private LinkedList<GameObject> m_collidingObjects = new LinkedList<GameObject>();
    private bool m_activate = false;

    private bool m_atRightPosition = false;
    private float m_timer = 0;

    void Start()
    {
        m_StartPosition = transform.localPosition;
    }

    void Update()
    {
        if(m_activate)
        {
            if (!m_atRightPosition)
            {
                m_timer += Time.deltaTime;

                if (Vector3.Distance(transform.position, m_EndPosition) > float.Epsilon)
                {
                    transform.localPosition = Vector3.Lerp(m_StartPosition, m_EndPosition, m_timer / m_AnimationTime);
                }
                else
                {
                    m_atRightPosition = true;
                    IsActive = true;
                }
            }
        }
        else
        {
            if(!m_atRightPosition)
            {
                m_timer += Time.deltaTime;

                if(Vector3.Distance(transform.position, m_StartPosition) > float.Epsilon)
                {
                    transform.localPosition = Vector3.Lerp(m_EndPosition, m_StartPosition, m_timer / m_AnimationTime);
                }
                else
                {
                    m_atRightPosition = true;
                    IsActive = false;
                }
            }
        }
    }

    void OnCollisionEnter(Collision _collision)
    {
        if(_collision.rigidbody)
        {
            if(_collision.rigidbody.mass >= m_TriggerMass)
            {
                m_collidingObjects.AddFirst(_collision.gameObject);
                if(!m_activate)
                {
                    m_activate = true;
                    m_atRightPosition = false;
                    m_timer = 0;
                }
            }
        }
    }

    void OnCollisionExit(Collision _collision)
    {
        if(m_collidingObjects.Contains(_collision.gameObject))
        {
            m_collidingObjects.Remove(_collision.gameObject);
        }

        if(m_collidingObjects.Count == 0)
        {
            m_activate = false;
            m_atRightPosition = false;
            m_timer = 0;
        }
    }
}
