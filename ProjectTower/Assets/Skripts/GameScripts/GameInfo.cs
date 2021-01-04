using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo
{
    public float[] m_LeftTime = new float[Config.DPLAYER_COUNT];
    public bool[] m_activeTimer = new bool[Config.DPLAYER_COUNT];

    public void Initialize()
    {

    }

    public void SetActiveTimer(int _index, bool b)
    {
        m_activeTimer[_index] = b;
    }

    public void SetTimer(int _index, float _second)
    {
        m_LeftTime[_index] = _second;
    }

    public void Update(float delta)
    {
        for (int i = 0; i < Config.DPLAYER_COUNT; i++)
        {
            if (m_activeTimer[i])
            {
                if (m_LeftTime[i] > 0)
                    m_LeftTime[i] -= delta;
            }
        }
    }
}
