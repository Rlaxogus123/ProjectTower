using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityElement : MonoBehaviour
{
    public int Index;
    [SerializeField] Text m_txtPlayer;
    [SerializeField] Text m_txtInfo;
    string Destiny;

    void Start()
    {
        
    }

    void Update()
    {
        if(Index > Config.DPLAYER_COUNT || Index == 0)
        {
            gameObject.SetActive(false);
        }
        m_txtPlayer.text = string.Format("플레이어 {0}", Index + 1);
        switch (GameMgr.Ins.m_PlayerDestiny[Index] + 1)
        {
            case 1: Destiny = "황족"; break;
            case 2: Destiny = "귀족"; break;
            case 3: Destiny = "기사"; break;
            case 4: Destiny = "평민"; break;
        }
        int SetTile = 50 + (GameMgr.Ins.m_PlayerDestiny[Index] * 10);
        m_txtInfo.text = string.Format("계급 : {0}급\n → {1}\n\n 계단 : {2} / {3}칸",
            GameMgr.Ins.m_PlayerDestiny[Index] + 1, Destiny,
            SetTile - (GameMgr.Ins.m_GameScene.m_gameUI.m_Player[Index].m_curTile + 1), SetTile);
    }
}
