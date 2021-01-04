using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStateDlg : MonoBehaviour
{
    [SerializeField] EntityPlayer[] m_Player = new EntityPlayer[4];
    void Start()
    {

    }
    public void Initialize()
    {
        Show();
        if(GameMgr.Ins.m_MoveCount[GameMgr.Ins.m_nNowTurn] > 0)
        {
            StartCoroutine(Enum_PlayerMove(GameMgr.Ins.m_nNowTurn));
        }
        else
        {
            GameMgr.Ins.m_GameScene.m_FSM.SetReadyState();
        }
    }

    IEnumerator Enum_PlayerMove(int playerID)
    {
        while(GameMgr.Ins.m_MoveCount[playerID] > 0)
        {
            if (m_Player[playerID].m_curTile > 0) m_Player[playerID].m_curTile--;
            else break;

            m_Player[playerID].SetMove(GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.m_TileList[playerID][m_Player[playerID].m_curTile].transform.position + new Vector3(0,1.35f,0));
            GameMgr.Ins.m_MoveCount[playerID]--;
            yield return new WaitForSeconds(0.6f);
        }

        GameMgr.Ins.m_GameScene.m_FSM.SetReadyState();
        Close();
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

    }
}
