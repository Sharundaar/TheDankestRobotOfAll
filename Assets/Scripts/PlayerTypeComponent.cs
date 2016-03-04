using UnityEngine;

public class PlayerTypeComponent : MonoBehaviour
{
    [SerializeField]
    private PlayerType m_type;
    public PlayerType Type { get { return m_type; } }

    public void Awake()
    {
        // Veryfying we are the only type
        var types = FindObjectsOfType<PlayerTypeComponent>();
        foreach (var type in types)
        {
            if (type == this)
                continue;

            if (type.Type == Type)
                Debug.LogWarning("Two same type player registered.");
        }
    }
}
