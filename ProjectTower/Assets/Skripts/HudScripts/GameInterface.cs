using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    [SerializeField] Text m_txtLeftTime;
    [SerializeField] Text m_txtTurnCount;

    [SerializeField] Button[] m_Dlgs;
    [SerializeField] Button m_btnNextTurn;
    [SerializeField] GameObject m_WhiteEffect;

    public int nTurnCount;
    void Start()
    {
        m_btnNextTurn.onClick.AddListener(onClicked_NextTurn);
    }

    public void onClicked_NextTurn()
    {
        GameMgr.Ins.m_GameInfo.SetTimer(0.0f);
    }

    public void Initialize()
    {

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
        for (int i = 0; i < m_Dlgs.Length; i++)
            m_Dlgs[i].interactable = GameMgr.Ins.m_GameScene.m_FSM.IsReadyState();
            
        if(GameMgr.Ins.m_GameScene.m_FSM.IsReadyState())
        {
            m_Dlgs[4].interactable = GameMgr.Ins.m_bFeedbackCheck;
            m_WhiteEffect.SetActive(GameMgr.Ins.m_bFeedbackCheck);
        }

        if (GameMgr.Ins.m_nNowTurn == 0) m_txtLeftTime.text = string.Format("제한시간 : {0:0.0}초", GameMgr.Ins.m_GameInfo.m_LeftTime);
        else
        {
            m_btnNextTurn.interactable = false;
            m_txtLeftTime.text = string.Format("플레이어{0} 차례", GameMgr.Ins.m_nNowTurn + 1);
        }

        if(nTurnCount != GameMgr.Ins.m_GameInfo.m_TurnCount)
        {
            nTurnCount = GameMgr.Ins.m_GameInfo.m_TurnCount;
            m_txtTurnCount.text = string.Format("턴 횟수 : {0} 번", nTurnCount);
        }
    }
}
