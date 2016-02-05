using UnityEngine;
using System.Collections;

public class DoorComponent : MonoBehaviour {

    [SerializeField]
    GameObject m_door;

    [SerializeField]
    float m_doorAmplitude;

    [SerializeField]
    Vector3 m_openingDirection = Vector3.right;

    [SerializeField]
    float m_openingSpeed;

    bool m_opening = false;
    bool m_opened = false;

    Vector3 m_doorClosedPosition;

    void Start()
    {
        m_doorClosedPosition = m_door.transform.localPosition;
    }

	void Update () {
        if ((m_opening && m_opened) || (!m_opening && !m_opened))
            return;

	    if(m_opening && !m_opened)
        {
            if(Vector2.Distance(m_door.transform.localPosition, m_doorClosedPosition) <= m_doorAmplitude)
            {
                m_door.transform.localPosition += m_openingDirection * Time.deltaTime * m_openingSpeed;
            }
            else
            {
                m_door.transform.localPosition = m_openingDirection * m_doorAmplitude + m_doorClosedPosition;
                m_opened = true;
            }
        }
        else if(!m_opening && m_opened)
        {
            if (Vector2.Distance(m_door.transform.localPosition, m_doorClosedPosition) > 0.01f)
            {
                m_door.transform.localPosition -= m_openingDirection * Time.deltaTime * m_openingSpeed;
            }
            else
            {
                m_door.transform.localPosition = m_doorClosedPosition;
                m_opened = false;
            }
        }
	}

    public void Open()
    {
        m_opening = true;
        m_opened = false;
    }

    public void Close()
    {
        m_opened = true;
        m_opening = false;
    }
}
