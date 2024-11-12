using UnityEngine;

public class ARSwitch : MonoBehaviour
{
    public GameObject map;
    public GameObject map_2;

    private bool m_switch = false;

    private void Start()
    {
        map.gameObject.SetActive(true);
        m_switch = false;
    }

    public void SwitchOn()
    {
        if (m_switch == false)
        {
            map.gameObject.SetActive(false);
            map_2.gameObject.SetActive(false);
            m_switch = true;
        }
        else if (m_switch != false)
        {
            map.gameObject.SetActive(true);
            map_2.gameObject.SetActive(true);
            m_switch = false;
        }
    }
}
