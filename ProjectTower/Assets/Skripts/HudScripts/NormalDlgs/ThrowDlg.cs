using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowDlg : MonoBehaviour
{
    [SerializeField] Button m_btnClose;
    [SerializeField] Image m_ImgGauge;
    [SerializeField] GameObject m_BlackPannel;
    [SerializeField] Text m_txtNotice;

    [SerializeField] AudioClip[] Audioclips;
    private AudioSource[] m_AudioSource;

    private bool bGauge;
    private float fGaugeFill;
    void Start()
    {
        for(int i = 0; i < Audioclips.Length; i++)
        {

        }
        m_btnClose.onClick.AddListener(Close);
    }
    public void Initialize()
    {
        m_BlackPannel.SetActive(true);

        if (GameMgr.Ins.m_GameScene.m_FSM.IsSwapState()) m_txtNotice.text = "시간 초과!";
        else if (GameMgr.Ins.m_nNowTurn != 0) m_txtNotice.text = "당신의 턴이 아닙니다!";
        else if (GameMgr.Ins.m_ThrowCount < 1) m_txtNotice.text = "주사위를 모두 던지셨습니다!";
        else m_BlackPannel.SetActive(false);

        bGauge = false;
        fGaugeFill = 0.0f;
        Show();
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        if (GameMgr.Ins.m_MoveCount[GameMgr.Ins.m_nNowTurn] > 0)
            GameMgr.Ins.m_GameScene.m_FSM.SetMoveState();
        this.gameObject.SetActive(false);
    }

    public void ThrowDown()
    {
        if (m_BlackPannel.activeSelf)
        {
            Close();
            return;
        }

        if (!bGauge)
            bGauge = true;
    }

    public void ThrowUp()
    {
        if (m_BlackPannel.activeSelf)
        {
            Close();
            return;
        }

        if (bGauge)
        {
            bGauge = false;
            int Number = (int)(fGaugeFill / 0.3333333f);
            int nResult = (Number * 2) + Random.Range(1, 3);
            Debug.Log(nResult);

            GameMgr.Ins.m_ThrowCount--;
            GameMgr.Ins.m_MoveCount[GameMgr.Ins.m_nNowTurn] = nResult;

            fGaugeFill = 0;
            m_ImgGauge.fillAmount = fGaugeFill;
        }
    }

    void Update()
    {
        if (GameMgr.Ins.m_GameScene.m_FSM.IsSwapState())
        {
            bGauge = false;
            m_txtNotice.text = "시간 초과!";
            m_BlackPannel.SetActive(true);
        }

        if (bGauge)
        {
            fGaugeFill += Time.deltaTime*1.67f;
            if(fGaugeFill > 1.0f)
                fGaugeFill = 0;
            m_ImgGauge.fillAmount = fGaugeFill;
            m_ImgGauge.color = new Color(1, 1 - (fGaugeFill / 1.0f), 1 - (fGaugeFill / 1.0f), 1);
        }
    }
}
