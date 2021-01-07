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
        if(GameMgr.Ins.m_nNowTurn != 0)
        {
            StartCoroutine(Enum_AI());
        }
        Show();
    }

    IEnumerator Enum_AI()
    {
        yield return new WaitForSeconds(Random.Range(2.0f, 5.0f));
        // AI 1
        if (GameMgr.Ins.m_ThrowCount > 0)
        {
            GameMgr.Ins.m_ThrowCount--;
            GameMgr.Ins.m_MoveCount[GameMgr.Ins.m_nNowTurn] = Random.Range(1, 7);
            GameMgr.Ins.m_GameScene.m_FSM.SetMoveState();
        }
        else
        {
            // AI End.
            GameMgr.Ins.m_GameInfo.SetTimer(0.0f);
        }
        yield return null;
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
        m_Camera[0].transform.position = Vector3.Lerp(m_Camera[0].transform.position, new Vector3(m_CamTarget[CamTargetIndex].position.x, m_CamTarget[CamTargetIndex].position.y, -10), Time.deltaTime * 5f);
        for(int i = 1; i < 4; i++)
        { 
            m_Camera[i].transform.position = Vector3.Lerp(m_Camera[i].transform.position, new Vector3(m_CamTarget[i].position.x, m_CamTarget[i].position.y, -10), Time.deltaTime * 5f);
        }
    }
}
