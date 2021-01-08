using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDiceDlg : MonoBehaviour
{
    [SerializeField] Button m_btnDice = null;
    void Start()
    {
        m_btnDice.onClick.AddListener(onClicked_Dice);
    }
    public void Initialize()
    {
        Show();
    }

    public void onClicked_Dice()
    {
        if (GameMgr.Ins.m_PlayerDestiny[0] >= 0) return;

        int nRandom = Random.Range(0, 4);
        GameMgr.Ins.m_PlayerDestiny[0] = nRandom;
        m_btnDice.GetComponentInChildren<Text>().text = string.Format("인생 계급 {0}등급", nRandom + 1);
        StartCoroutine(Enum_Close());
    }

    IEnumerator Enum_Close()
    {
        yield return new WaitForSeconds(1.0f);
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        CG.alpha = 1;
        while(CG.alpha > 0)
        {
            CG.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameMgr.Ins.m_GameScene.m_FSM.SetReadyState();
        Close();
        yield return null;
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
}
