using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    private static GameController s_Singleton;
    public static GameController Instance() { return s_Singleton; }

    [SerializeField]
    GameObject m_MainMenu;

    [SerializeField]
    GameObject[] m_Menus;

    GameObject m_CurrentMenu;

    [SerializeField]
    UnityEngine.UI.Text m_DebugLabel;

    private class DebugMessage
    {
        public string m_message;
        public float m_duration;
    }

    private List<DebugMessage> m_DebugMessages = new List<DebugMessage>();

    private void Start()
    {
        s_Singleton = this;

        HideMenus();
        ChangeMenuTo(m_MainMenu);

        DebugText("Hello World!");
    }

    private void Update()
    {
        if (m_DebugMessages.Count == 0)
            return;

        bool labelDirty = m_DebugMessages.RemoveAll((msg) =>
            {
                msg.m_duration -= Time.deltaTime;
                return msg.m_duration < 0;
            }
        ) > 0;

        if (labelDirty)
            UpdateDebugLabelText();
    }

	public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeMenuTo(GameObject _menu)
    {
        if(m_CurrentMenu != null)
        {
            m_CurrentMenu.SetActive(false);
        }

        m_CurrentMenu = _menu;
        m_CurrentMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        ChangeMenuTo(m_MainMenu);
    }

    public void HideMenus()
    {
        foreach(var menu in m_Menus)
        {
            menu.SetActive(false);
        }
    }

    public void DebugText(string _msg, float _duration = 1.0f)
    {
        DebugMessage msg = new DebugMessage() { m_message = _msg, m_duration = _duration };
        m_DebugMessages.Add(msg);
        UpdateDebugLabelText();
    }

    void UpdateDebugLabelText()
    {
        var msg = "";
        foreach(var dbmsg in m_DebugMessages)
        {
            msg += dbmsg.m_message + "\n";
        }

        m_DebugLabel.text = msg;
    }
}
