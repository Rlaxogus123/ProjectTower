using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    [SerializeField] Text[] m_txtNowTile;
    [SerializeField] Text[] m_txtLeftTime;
    [SerializeField] Image[] m_ImgCrown;
    [SerializeField] GameObject m_Glowturn;

    public int[] Rank = new int[Config.DPLAYER_COUNT];
    bool active = false;

    void Start()
    {
        for (int i = 0; i < Rank.Length; i++)
        {
            Rank[i] = i;
        }
    }

    IEnumerator Enum_Start()
    {
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        CG.alpha = 0;
        while(CG.alpha < 1)
        {
            CG.alpha += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void RankingUpdate()
    {
        for (int i = 0; i < Config.DPLAYER_COUNT; i++)
        {
            EntityPlayer kPlayer = GameMgr.Ins.m_GameScene.m_gameUI.m_Player[i];
            int calulation = 0;
            int matchCount = 0;
            while (calulation < 90)
            {
                if (calulation == kPlayer.m_curTile)
                    break;

                for (int j = 0; j < Config.DPLAYER_COUNT; j++)
                {
                    if (j == i) continue;
                    EntityPlayer jPlayer = GameMgr.Ins.m_GameScene.m_gameUI.m_Player[j];
                    if (jPlayer.m_curTile == calulation)
                        matchCount++;
                }
                calulation++;
            }
            Rank[i] = matchCount;
        }

        for (int i = 0; i < m_ImgCrown.Length; i++)
        {
            switch (Rank[i])
            {
                case 0: m_ImgCrown[i].color = new Color(1, 1, 0, 1); break;
                case 1: m_ImgCrown[i].color = new Color(0.8f, 0.8f, 0.8f, 1); break;
                case 2: m_ImgCrown[i].color = new Color(1, 168 / 255f, 0, 1); break;
                case 3: m_ImgCrown[i].color = new Color(0, 0, 0, 0); break;
            }
        }
    }

    void Update()
    {
        if (!active)
        {
            if (GameMgr.Ins.m_PlayerDestiny[1] != -1)
            {
                active = true;
                StartCoroutine(Enum_Start());
            }
        }
        if (active)
        {
            RankingUpdate();

            m_Glowturn.SetActive(true);
            m_Glowturn.transform.position = Vector3.Lerp(m_Glowturn.transform.position, new Vector3(m_Glowturn.transform.position.x, m_ImgCrown[GameMgr.Ins.m_nNowTurn].transform.position.y - 0.5f, m_Glowturn.transform.position.z), Time.deltaTime * 8);

            for (int i = 0; i < Config.DPLAYER_COUNT; i++)
            {
                int SetTile = 50 + (GameMgr.Ins.m_PlayerDestiny[i] * 10);
                m_txtNowTile[i].text = string.Format("{0}/{1} 계단", SetTile - (GameMgr.Ins.m_GameScene.m_gameUI.m_Player[i].m_curTile + 1), SetTile);

                m_txtLeftTime[i].gameObject.SetActive(false);
                if (GameMgr.Ins.m_nNowTurn == i)
                {
                    m_txtLeftTime[i].gameObject.SetActive(true);
                    m_txtLeftTime[i].text = string.Format("{0:0.0}초 남음", GameMgr.Ins.m_GameInfo.m_LeftTime);
                }
            }
        }
    }
}