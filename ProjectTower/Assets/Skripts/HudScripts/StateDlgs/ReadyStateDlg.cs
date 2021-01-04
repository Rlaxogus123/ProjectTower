using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyStateDlg : MonoBehaviour
{
    [SerializeField] Camera[] m_Camera;
    [SerializeField] Transform[] m_CamTarget;

    public int CamTargetIndex = 0;
    void Start()
    {

    }
    public void Initialize()
    {
        Show();
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) CamTargetIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) CamTargetIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) CamTargetIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) CamTargetIndex = 3;
        m_Camera[0].transform.position = Vector3.Lerp(m_Camera[0].transform.position, new Vector3(m_CamTarget[CamTargetIndex].position.x, m_CamTarget[CamTargetIndex].position.y, -10), Time.deltaTime * 5f);
        for(int i = 1; i < 4; i++)
        { 
            m_Camera[i].transform.position = Vector3.Lerp(m_Camera[i].transform.position, new Vector3(m_CamTarget[i].position.x, m_CamTarget[i].position.y, -10), Time.deltaTime * 5f);
        }
    }
}
