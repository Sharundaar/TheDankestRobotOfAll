using UnityEngine;
using System.Collections;

public class StartPoint : MonoBehaviour {
    [SerializeField]
    private PlayerType m_type;
    public PlayerType Type { get { return m_type; } }
}
