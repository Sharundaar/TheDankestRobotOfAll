using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Debugger : MonoBehaviour, IDebugger {
    
    public class DebugMessage
    {
        public DebugMessage(Color _color, string _message, float _duration)
        {
            m_color = _color;
            m_message = _message;
            m_duration = _duration;
        }

        public Color m_color;
        public string m_message;
        public float m_duration;
    }

    private LinkedList<DebugMessage> m_messages = new LinkedList<DebugMessage>();

	// Update is called once per frame
	void Update () {
	    if(m_messages.Count > 0)
        {
            for(var node = m_messages.First; node != null; node = node != null ? node.Next : null)
            {
                node.Value.m_duration -= Time.deltaTime;
                if(node.Value.m_duration <= 0)
                {
                    var temp = node;
                    node = node.Previous;
                    m_messages.Remove(temp);
                    if (node == null)
                        node = m_messages.First;
                }
            }
        }
	}

    void OnGUI()
    {
        var fontSizeSave = GUI.skin.label.fontSize;
        GUI.skin.label.fontSize = 16;
        using (var areaScope = new GUILayout.AreaScope(new Rect(20, 10, Screen.width - 20, Screen.height - 10)))
        {
            GUILayout.BeginVertical();
            foreach (var message in m_messages)
            {
                GUI.color = message.m_color;
                GUILayout.Label(message.m_message);
            }
            GUILayout.EndVertical();
        }
        GUI.skin.label.fontSize = fontSizeSave;
    }

    public void AddMessage(string _message, float _duration = 5.0f)
    {
        AddMessage(_message, Color.green, _duration);
    }

    public void AddMessage(string _message, Color _color, float _duration = 5.0f)
    {
        m_messages.AddLast(new DebugMessage(_color, _message, _duration));
    }
}
