using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    [SerializeField] Button m_btnMap;
    [SerializeField] Button m_btnDice;
    [SerializeField] Button m_btnFeedback;
    [SerializeField] Text m_txtLeftTime;
    void Start()
    {
        m_btnFeedback.onClick.AddListener(onClicked_Feedback);
        m_btnDice.onClick.AddListener(onClicked_Throw);
        m_btnMap.onClick.AddListener(onClicked_Map);
    }

    public void Initialize()
    {

    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        this.gameObject.SetActive(false);
    }

    public void onClicked_Throw()
    {
        GameMgr.Ins.m_GameScene.Open_Throw();
    }

    public void onClicked_Map()
    {
        GameMgr.Ins.m_GameScene.Open_Map();
    }

    public void onClicked_Feedback()
    {
        GameMgr.Ins.m_GameScene.Open_Feedback();
    }

    void Update()
    {
        m_txtLeftTime.text = string.Format("제한시간 : {0:0.0}초", GameMgr.Ins.m_GameInfo.m_LeftTime[0]);
    }
}
