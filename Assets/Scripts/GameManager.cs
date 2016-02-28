using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Debugger))]
public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject m_MainMenu;

    private GameObject m_CurrentMenu;

    [SerializeField]
    private GameObject[] m_MenuList;


    private static GameManager s_Instance = null;
    public static GameManager Instance()
    {
        return s_Instance;
    }

    private Debugger m_debugger;
    public  IDebugger Debugger()
    {
        return m_debugger;
    }

    public void Start()
    {
        HideAllMenus();
        ChangeMenuToMainMenu();
    }

    public void HideAllMenus()
    {
        foreach(var menu in m_MenuList)
        {
            menu.SetActive(false);
        }
    }

    public void Awake()
    {
        s_Instance = this;
        m_debugger = GetComponent<Debugger>();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeMenuTo(GameObject _menu)
    {
        if(m_CurrentMenu != null)
            m_CurrentMenu.SetActive(false);
        _menu.SetActive(true);
        m_CurrentMenu = _menu;
    }

    public void ChangeMenuToMainMenu()
    {
        if(m_MainMenu != null)
            ChangeMenuTo(m_MainMenu);
    }
}
