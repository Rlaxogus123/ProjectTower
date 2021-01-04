using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackDlg : MonoBehaviour
{
    [SerializeField] Button m_btnClose;
    void Start()
    {
        m_btnClose.onClick.AddListener(Close);
    }

    public void Initialize()
    {
        Show();
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
