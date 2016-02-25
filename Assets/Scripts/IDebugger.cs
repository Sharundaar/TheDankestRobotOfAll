using UnityEngine;

public interface IDebugger
{
    void AddMessage(string _message, float _duration = 5.0f);
    void AddMessage(string _message, Color _color, float _duration = 5.0f);
}