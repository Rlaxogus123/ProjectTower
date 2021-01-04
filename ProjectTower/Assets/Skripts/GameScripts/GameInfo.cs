using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public float m_LeftTime = 0.0f;
    public int m_TurnCount = 0;

    public bool isBGM = true;
    public bool isSFX = true;
    public float BGMAmount = 1.0f;
    public float SFXAmount = 1.0f;

    public void Initialize()
    {

    }

    public void SetTimer(float _second)
    {
        m_LeftTime = _second;
    }

    public void Update(float delta)
    {
        if (GameMgr.Ins.m_PlayerDestiny[1] != -1) // 주사위로 운명을 정한 후
        {
            if (m_LeftTime > 0)
                m_LeftTime -= delta;
            else
            {
                m_LeftTime = 0.0f;
                TimerEnd();
            }
        }
    }

    public void TimerEnd()
    {
        if(GameMgr.Ins.m_GameScene.m_FSM.IsReadyState())
        {
            if(GameMgr.Ins.m_MoveCount[GameMgr.Ins.m_nNowTurn] <= 0)
            {
                GameMgr.Ins.m_GameScene.m_FSM.SetSwapState();
            }
        }
    }
}
