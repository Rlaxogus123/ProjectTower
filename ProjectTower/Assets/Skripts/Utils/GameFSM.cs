using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFSM
{
    public delegate void DelegateFunc();

    public class CState
    {
        public DelegateFunc m_OnEnterFunc = null;
        public DelegateFunc m_OnExitFunc = null;

        public virtual void Initialize_Enter(DelegateFunc OnEnter)
        {
            m_OnEnterFunc = new DelegateFunc(OnEnter);
        }
        public virtual void Initialize_Exit(DelegateFunc OnExit)
        {
            m_OnExitFunc = new DelegateFunc(OnExit);
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }

    public class CDiceState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }
    }
    public class CReadyState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }
    }
    public class CMoveState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }
    }
    public class CSwapState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }
    }
    public class CResultState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }
    }
    public class CEventState : CState
    {
        public override void OnEnter()
        {
            if (m_OnEnterFunc != null)
                m_OnEnterFunc();
        }
        public override void OnUpdate()
        {
        }
        public override void OnExit()
        {
            if (m_OnExitFunc != null)
                m_OnExitFunc();
        }
    }

    private CState m_curState = null;
    private CState m_newState = null;

    private CState m_kDice = new CDiceState();
    private CState m_kReady = new CReadyState();
    private CState m_kMove = new CMoveState();
    private CState m_kSwap = new CSwapState();
    private CState m_kResult = new CResultState();
    private CState m_kEvent = new CEventState();

    public void Initialize_Enter(DelegateFunc kDice, DelegateFunc kReady, DelegateFunc kMove, DelegateFunc kSwap, DelegateFunc kResult, DelegateFunc kEvent)
    {
        m_kDice.Initialize_Enter(kDice);
        m_kReady.Initialize_Enter(kReady);
        m_kMove.Initialize_Enter(kMove);
        m_kSwap.Initialize_Enter(kSwap);
        m_kResult.Initialize_Enter(kResult);
        m_kEvent.Initialize_Enter(kEvent);
    }

    public void Initialize_Exit(DelegateFunc kDice,DelegateFunc kReady,DelegateFunc kMove, DelegateFunc kSwap, DelegateFunc kResult, DelegateFunc kEvent)
    {
        m_kDice.Initialize_Exit(kDice);
        m_kReady.Initialize_Exit(kReady);
        m_kMove.Initialize_Exit(kMove);
        m_kSwap.Initialize_Exit(kSwap);
        m_kResult.Initialize_Exit(kResult);
        m_kEvent.Initialize_Exit(kEvent);
    }

    public void SetState(CState kState)
    {
        m_newState = kState;
    }

    public void OnUpdate()
    {
        if (m_newState != null)
        {
            if (m_curState != null)
                m_curState.OnExit();

            m_curState = m_newState;
            m_newState = null;
            m_curState.OnEnter();
        }

        if (m_curState != null)
        {
            m_curState.OnUpdate();
        }
    }

    public void SetDiceState()
    {
        SetState(m_kDice);
    }
    public void SetReadyState()
    {
        SetState(m_kReady);
    }
    public void SetMoveState()
    {
        SetState(m_kMove);
    }
    public void SetSwapState()
    {
        SetState(m_kSwap);
    }
    public void SetResultState()
    {
        SetState(m_kResult);
    }
    public void SetEventState()
    {
        SetState(m_kEvent);
    }
    public void SetNoneState()
    {
        m_newState = null;
        m_curState = null;
    }

    public CState GetCurState()
    {
        return m_curState;
    }

    public bool IsCurState(CState kState)
    {
        if (m_curState == null)
            return false;
        return (m_curState == kState) ? true : false;
    }
    public bool IsDiceState()
    {
        if (m_curState == null)
            return false;
        return (m_curState == m_kDice) ? true : false;
    }
    public bool IsReadyState()
    {
        if (m_curState == null)
            return false;
        return (m_curState == m_kReady) ? true : false;
    }
    public bool IsMoveState()
    {
        if (m_curState == null)
            return false;
        return (m_curState == m_kMove) ? true : false;
    }
    public bool IsSwapState()
    {
        if (m_curState == null)
            return false;
        return (m_curState == m_kSwap) ? true : false;
    }
    public bool IsResultState()
    {
        if (m_curState == null)
            return false;
        return (m_curState == m_kResult) ? true : false;
    }

    public bool IsEventState()
    {
        if (m_curState == null)
            return false;
        return (m_curState == m_kEvent) ? true : false;
    }
}
