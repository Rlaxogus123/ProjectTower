using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameUI m_gameUI;
    public HudUI m_hudUI;

    [HideInInspector] public GameFSM m_FSM = new GameFSM();

    private void Awake()
    {
        GameMgr.Ins.Initialize();
    }

    private void Start()
    {
        m_FSM.Initialize_Enter(
            OnEnter_DiceState,
            OnEnter_ReadyState,
            OnEnter_MoveState,
            OnEnter_SwapState,
            OnEnter_ResultState,
            OnEnter_EventState);
        m_FSM.Initialize_Exit(
            OnExit_DiceState,
           OnExit_ReadyState,
           OnExit_MoveState,
           OnExit_SwapState,
           OnExit_ResultState,
           OnExit_EventState);

        m_FSM.SetDiceState();
        GameMgr.Ins.SetGameScene(this);
        GameMgr.Ins.m_GameInfo.SetTimer(5.0f);
        SaveInfo.Ins.LoadFile();
    }

    public void Initialize()
    {
    }

    private void Update()
    {
        if (m_FSM != null)
        {
            m_FSM.OnUpdate();

            GameMgr.Ins.Update(Time.deltaTime);
        }
    }

    public void Open_Feedback()
    {
        m_hudUI.m_Feedback.Initialize();
    }
    public void Open_Throw()
    {
        m_hudUI.m_ThrowDlg.Initialize();
    }
    public void Open_Map()
    {
        m_hudUI.m_MapDlg.Initialize();
    }

    // * - * - * - * - * - * - * - * - * - * - * - * - *
    // *                ▼  OnEnter  ▼                  *
    // * - * - * - * - * - * - * - * - * - * - * - * - *
    void OnEnter_DiceState()
    {
        m_hudUI.m_DiceDlg.Initialize();
    }
    void OnEnter_ReadyState()
    {
        m_hudUI.m_ReadyDlg.Initialize();
    }
    
    void OnEnter_MoveState()
    {
        m_hudUI.m_MoveDlg.Initialize();
    }
    void OnEnter_SwapState()
    {
        m_hudUI.m_SwapDlg.Initialize();
    }
    void OnEnter_ResultState()
    {
        m_hudUI.m_ResultDlg.Initialize();
    }

    void OnEnter_EventState()
    {
        m_hudUI.m_EventDlg.Initialize();
    }

    // * - * - * - * - * - * - * - * - * - * - * - * - *
    // *                ▼  OnExit  ▼                   *
    // * - * - * - * - * - * - * - * - * - * - * - * - *
    void OnExit_DiceState()
    {
        m_gameUI.Initialize();
        m_hudUI.m_Gameinterface.Show();
    }
    void OnExit_ReadyState()
    {

    }
    void OnExit_MoveState()
    {

    }
    void OnExit_SwapState()
    {

    }
    void OnExit_ResultState()
    {

    }

    void OnExit_EventState()
    {

    }
}
