using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInformations : NetworkBehaviour {
    public enum PlayerType
    {
        NONE,
        SCIENTIST,
        ROBOT
    }

    private int m_id;
    private string m_name;
    private Color m_color;
    private PlayerType m_type;

    public PlayerInformations(int _id, string _name, Color _color, PlayerType _type)
    {
        m_name = _name;
        m_color = _color;
        m_type = _type;
        m_id = _id;

        GameManager.Instance().Debugger().AddMessage("New PlayerInformations created : " + m_id);
    }

    public int ID
    {
        get { return m_id; }
    }

    public string Name
    {
        get { return m_name; }
        set { m_name = value; }
    }

    public Color Color
    {
        get { return m_color; }
        set { m_color = value; }
    }

    public PlayerType Type
    {
        get { return m_type; }
        set { m_type = value; }
    }
}
