using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameTile m_GameTile;
    public EntityPlayer[] m_Player;
    void Start()
    {
        
    }

    public void Initialize()
    {
        GameMgr.Ins.m_PlayerDestiny[1] = Random.Range(0, 4);
        GameMgr.Ins.m_PlayerDestiny[2] = Random.Range(0, 4);
        GameMgr.Ins.m_PlayerDestiny[3] = Random.Range(0, 4);
        
        for (int i = 0; i < m_Player.Length; i++)
        {
            int SetTile = 50 + (GameMgr.Ins.m_PlayerDestiny[i] * 10);
            m_GameTile.SetTile(i, SetTile);

            m_Player[i].gameObject.SetActive(true);
            m_Player[i].m_curTile = SetTile - 1;
            m_Player[i].transform.position = m_GameTile.m_TileList[i][m_Player[i].m_curTile].transform.position
                + new Vector3(0, Config.DTILE_UPPER, 0);
            m_Player[i].Initialize(m_GameTile.m_TileList[i][m_Player[i].m_curTile - 1].transform.position);
        }
    }

    void Update()
    {
        
    }
}
