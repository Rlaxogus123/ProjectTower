using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUI : MonoBehaviour
{
    public SetDiceDlg m_DiceDlg;
    public ReadyStateDlg m_ReadyDlg;
    public ThrowDlg m_ThrowDlg;
    public MapDlg m_MapDlg;
    public MoveStateDlg m_MoveDlg;
    public SwapStateDlg m_SwapDlg;
    public ResultStateDlg m_ResultDlg;
    public EventStateDlg m_EventDlg;
    public GameInterface m_Gameinterface;
    public FeedbackDlg m_Feedback;

    [SerializeField] Camera m_Camera;
    private float fLerpSize;
    void Start()
    {
        fLerpSize = m_Camera.orthographicSize;
    }

    public float Lerp(float start, float end, float amount)
    {
        return start + (end - start) * amount;
    }

    void Update()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (wheelInput < 0)
        {
            if(fLerpSize < 9.5)
               fLerpSize += 0.4f;
        }
        if (wheelInput > 0)
        {
            if (fLerpSize > 3)
                fLerpSize -= 0.4f;
        }
        m_Camera.orthographicSize = Lerp(m_Camera.orthographicSize, fLerpSize, Time.deltaTime * 8);
    }
}
