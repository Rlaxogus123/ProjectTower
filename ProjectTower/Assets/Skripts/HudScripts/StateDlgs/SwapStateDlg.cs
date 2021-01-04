using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapStateDlg : MonoBehaviour
{
    private CanvasGroup CG_Panel;
    private float fAlpha;
    void Start()
    {
    }
    public void Initialize()
    {
        Show();
        Close();
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        StartCoroutine(Enum_Close());
    }

    public void CloseDlgs()
    {
        GameMgr.Ins.m_GameScene.m_hudUI.m_ThrowDlg.Close();
    }

    public void SetSwap()
    {
        GameMgr.Ins.m_nNowTurn++;
        GameMgr.Ins.m_nNowTurn %= 4;

        GameMgr.Ins.m_GameInfo.m_TurnCount++;
        GameMgr.Ins.m_GameInfo.SetTimer(45.0f);
        GameMgr.Ins.m_GameScene.m_FSM.SetReadyState();
    }

    IEnumerator Enum_Close()
    {
        CG_Panel = this.GetComponentInChildren<CanvasGroup>();
        fAlpha = 1.5f;
        while(fAlpha > 0)
        {
            CG_Panel.alpha = fAlpha;
            fAlpha -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        SetSwap();
        gameObject.SetActive(false);
        yield return null;
    }

    void Update()
    {

    }
}
