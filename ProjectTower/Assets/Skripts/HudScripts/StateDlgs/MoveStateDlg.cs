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

    public void CheckTile(int playerID)
    {
        EntityTile kTile = GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.m_TileList[playerID][m_Player[playerID].m_curTile].GetComponent<EntityTile>();
        
        // 공격타일에 갔을 때
        if (kTile.m_bAttackTile) GameMgr.Ins.m_GameScene.m_FSM.SetEventState();
        else GameMgr.Ins.m_GameScene.m_FSM.SetReadyState();

        // 공격된 타일을 밟았을 때
        if (kTile.m_bBreakTile)
        {
            m_Player[GameMgr.Ins.m_nNowTurn].stat_Attacked = 1;
            m_Player[GameMgr.Ins.m_nNowTurn].transform.GetChild(0).gameObject.SetActive(true);
            m_Player[GameMgr.Ins.m_nNowTurn].transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
        Close();
    }

    IEnumerator Enum_PlayerMove(int playerID)
    {
        while(GameMgr.Ins.m_MoveCount[playerID] > 0)
        {
            if (m_Player[playerID].m_curTile > 0)
            {
                this.GetComponent<AudioSource>().volume = GameMgr.Ins.m_GameInfo.SFXAmount;
                this.GetComponent<AudioSource>().Play();
                m_Player[playerID].m_curTile--;
            }
            else break;

            m_Player[playerID].SetMove(GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.m_TileList[playerID][m_Player[playerID].m_curTile].transform.position + new Vector3(0, Config.DTILE_UPPER, 0));
            GameMgr.Ins.m_MoveCount[playerID]--;
            yield return new WaitForSeconds(0.4f);
        }
        CheckTile(playerID);
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
