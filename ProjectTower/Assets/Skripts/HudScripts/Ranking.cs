using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField] Text[] m_txtNowTile;
    [SerializeField] Text[] m_txtLeftTime;

    void Start()
    {
    }

    void Update()
    {
        for(int i = 0; i < Config.DPLAYER_COUNT; i++)
        {
            int SetTile = 50 + (GameMgr.Ins.m_PlayerDestiny[i] * 10);

            m_txtNowTile[i].text = string.Format("{0}/{1} 계단", SetTile - (GameMgr.Ins.m_GameScene.m_gameUI.m_Player[i].m_curTile + 1), SetTile);

            m_txtLeftTime[i].gameObject.SetActive(false);
            if(GameMgr.Ins.m_nNowTurn == i)
            {
                m_txtLeftTime[i].gameObject.SetActive(true);
                m_txtLeftTime[i].text = string.Format("{0:0.0}초 남음",GameMgr.Ins.m_GameInfo.m_LeftTime);
            }
        }
    }
}