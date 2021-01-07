using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDlg : MonoBehaviour
{
    [SerializeField] Button m_btnClose;
    void Start()
    {
        m_btnClose.onClick.AddListener(Close);
    }
    public void Initialize()
    {
        Show();
        StartCoroutine(Enum_Show());
    }
    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        if(this.GetComponent<CanvasGroup>().alpha >= 1)
            StartCoroutine(Enum_Close());
    }
    
    IEnumerator Enum_Show()
    {
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        CG.alpha = 0;
        while(CG.alpha < 1)
        {
            CG.alpha += Time.deltaTime * 3;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator Enum_Close()
    {
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        while(CG.alpha > 0)
        {
            CG.alpha -= Time.deltaTime * 3;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        this.gameObject.SetActive(false);
        yield return null;
    }

    void Update()
    {

    }
}
