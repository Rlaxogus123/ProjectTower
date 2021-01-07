using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : Singleton<GameMgr>
{
    public GameMgr()
    {

    }

    public GameInfo m_GameInfo = new GameInfo();
    public GameScene m_GameScene = null;
    public void SetGameScene(GameScene _scene) {
        m_GameScene = _scene;
    }

    public int m_nNowTurn;
    public int[] m_PlayerDestiny = new int[Config.DPLAYER_COUNT];
    public int[] m_MoveCount = new int[Config.DPLAYER_COUNT]; // 플레이어가 움직일 거리
    public int m_ThrowCount = 1; // 주사위를 던질 횟수
    public bool m_bFeedbackCheck; // 피드백 확인했나

    // Singleton Datas -

    // * end

    public void Initialize()
    {
        for (int i = 0; i < Config.DPLAYER_COUNT; i++)
        {
            m_PlayerDestiny[i] = -1;
        }
    }

    public void Update(float delta)
    {
        m_GameInfo.Update(delta);
    }
}
