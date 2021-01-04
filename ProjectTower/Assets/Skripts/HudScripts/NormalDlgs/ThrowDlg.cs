using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowDlg : MonoBehaviour
{
    [SerializeField] Button m_btnClose;
    [SerializeField] Image m_ImgGauge;
    [SerializeField] GameObject m_BlackPannel;

    private bool bGauge;
    private float fGaugeFill;
    void Start()
    {
        m_btnClose.onClick.AddListener(Close);
    }
    public void Initialize()
    {
        if (GameMgr.Ins.m_ThrowCount < 1) m_BlackPannel.SetActive(true);
        else m_BlackPannel.SetActive(false);
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
        if (GameMgr.Ins.m_ThrowCount < 1)
        {
            Close();
            return;
        }

        if (!bGauge)
            bGauge = true;
    }

    public void ThrowUp()
    {
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
