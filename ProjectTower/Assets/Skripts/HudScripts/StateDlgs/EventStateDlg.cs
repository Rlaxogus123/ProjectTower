using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventStateDlg : MonoBehaviour
{
    [SerializeField] GameObject m_objPlayerSelect;
    [SerializeField] GameObject m_objSelectTile;

    [SerializeField] EntityElement[] m_Elements;
    bool[] possible = new bool[Config.DPLAYER_COUNT];

    [SerializeField] AudioClip[] m_audioclip;
    private AudioSource[] m_audio = new AudioSource[5];

    bool bSelect;
    int nAttack;
    int nSelectIndex;

    void Start()
    {
        for (int i = 0; i < m_audioclip.Length; i++)
        {
            m_audio[i] = this.gameObject.AddComponent<AudioSource>();
            m_audio[i].Stop();
            m_audio[i].clip = m_audioclip[i];
            m_audio[i].playOnAwake = false;
            m_audio[i].loop = false;
        }

        m_audio[0].volume = 0.5f;
        m_audio[0].pitch = 1.5f;
    }
    public void Initialize()
    {
        Show();

        for(int i = 0; i < m_Elements.Length; i++)
        {
            possible[i] = false;
            m_Elements[i].GetComponent<Button>().interactable = false;
            
            int Check = GameMgr.Ins.m_GameScene.m_gameUI.m_Player[i].m_curTile - 1;
            while (Check >= 0)
            {
                if(!GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.m_TileList[i][Check].GetComponent<EntityTile>().m_bBreakTile)
                {
                    Debug.Log("possible = true");
                    possible[i] = true;
                    m_Elements[i].GetComponent<Button>().interactable = true;
                    break;
                }
                Check--;
            }
        }

        bool retun = true;
        for(int i = 0; i < possible.Length; i++)
        {
            if(possible[i])
            {
                retun = false;
                break;
            }
        }
        if(retun)
        {
            GameMgr.Ins.m_GameScene.m_FSM.SetReadyState();
            return;
        }

        m_objSelectTile.SetActive(false);
        if (GameMgr.Ins.m_nNowTurn == 0) m_objPlayerSelect.SetActive(true);
        else
        {
            m_objPlayerSelect.SetActive(false);
            StartCoroutine(Enum_AI());
        }

        nAttack = 2;
        bSelect = false;

        StartCoroutine(Enum_Show());
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        if (this.GetComponent<CanvasGroup>().alpha >= 1)
            StartCoroutine(Enum_Close());
    }

    IEnumerator Enum_AI()
    {
        yield return new WaitForSeconds(Random.Range(1.5f, 4.0f));

        int nRandom = GameMgr.Ins.m_nNowTurn;
        while (nRandom == GameMgr.Ins.m_nNowTurn || !possible[nRandom])
        {
            nRandom = Random.Range(0, Config.DPLAYER_COUNT);
        }
        GameMgr.Ins.m_GameScene.m_hudUI.m_ReadyDlg.CamTargetIndex = nRandom;
        GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.ViewEnableTile(nRandom);
        nSelectIndex = nRandom;

        while (nAttack > 0)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 3.0f));

            int tile = Random.Range(1, 7);
            int targetIndex = GameMgr.Ins.m_GameScene.m_gameUI.m_Player[nRandom].m_curTile - tile;
            while(targetIndex > 0)
            {
                if(!GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.m_TileList[nRandom][targetIndex].GetComponent<EntityTile>().m_bBreakTile)
                {
                    m_audio[0].Play();
                    GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.m_TileList[nRandom][targetIndex].GetComponent<EntityTile>().m_bBreakTile = true;
                    break;
                }
                targetIndex--;
            }
            nAttack--;
        }

        StartCoroutine(Enum_AttackEnd());
        yield return null;
    }

    IEnumerator Enum_Show()
    {
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        CG.alpha = 0;
        while (CG.alpha < 1)
        {
            CG.alpha += Time.deltaTime * 3;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator Enum_Close()
    {
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        while (CG.alpha > 0)
        {
            CG.alpha -= Time.deltaTime * 3;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        this.gameObject.SetActive(false);
        yield return null;
    }

    public void onClicked_Target(int index)
    {
        GameMgr.Ins.m_GameScene.m_hudUI.m_ReadyDlg.CamTargetIndex = index;
        GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.ViewEnableTile(index);
        nSelectIndex = index;

        m_objPlayerSelect.SetActive(false);
        m_objSelectTile.SetActive(true);
        bSelect = true;
    }

    public void Raycast_Tile()
    {
        if (!bSelect) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            EntityTile kTile = hit.collider.GetComponent<EntityTile>();
            if (!kTile.m_bBreakTile)
            {
                if(kTile.nIndex < GameMgr.Ins.m_GameScene.m_gameUI.m_Player[nSelectIndex].m_curTile)
                {
                    nAttack--;
                    kTile.m_bBreakTile = true;
                    m_audio[0].Play();
                    if(nAttack == 0) StartCoroutine(Enum_AttackEnd());
                }
            }
        }
    }

    IEnumerator Enum_AttackEnd()
    {
        yield return new WaitForSeconds(2.0f);
        GameMgr.Ins.m_GameScene.m_gameUI.m_GameTile.ViewDisableTile(nSelectIndex);
        GameMgr.Ins.m_GameScene.m_hudUI.m_ReadyDlg.CamTargetIndex = GameMgr.Ins.m_nNowTurn;
        GameMgr.Ins.m_GameScene.m_FSM.SetReadyState();
        Close();
    }

    void Update()
    {
        if (nAttack > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Raycast_Tile();
            }
        }
    }
}
