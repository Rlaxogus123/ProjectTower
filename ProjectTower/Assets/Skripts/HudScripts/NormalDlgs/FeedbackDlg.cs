using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackDlg : MonoBehaviour
{
    [SerializeField] Button m_btnClose;
    [SerializeField] Text[] m_txts;

    [SerializeField] AudioClip[] m_audioclip;
    private AudioSource[] m_audio;
    void Start()
    {
        for(int i = 0; i < m_audioclip.Length; i++)
        {
            m_audio[i] = this.gameObject.AddComponent<AudioSource>();
            m_audio[i].Stop();
            m_audio[i].clip = m_audioclip[i];
            m_audio[i].playOnAwake = false;
            m_audio[i].loop = false;
        }
        m_btnClose.onClick.AddListener(Close);
    }

    public string ConvertString(string str)
    {
        return str.Replace("\\n", "\n");
    }

    public void Initialize()
    {
        Show();
        SetTableData(0);
        StartCoroutine(Enum_Show());
    }

    public void SetTableData(int playerID)
    {
        GameMgr.Ins.m_bFeedbackCheck = false;

        List<AssetFeedback> kList = new List<AssetFeedback>();
        for(int i = 0; i < AssetMgr.Inst.m_AssFeedbacks.Count; i++)
        {
            AssetFeedback kAsset = AssetMgr.Inst.GetAssetFeedback(i);
            if(kAsset.type == GameMgr.Ins.m_PlayerDestiny[playerID])
                kList.Add(kAsset);
        }

        AssetFeedback kAss = kList[Random.Range(0, kList.Count)];

        m_txts[0].text = kAss.title;
        m_txts[1].text = "- " + kAss.lore;
        m_txts[2].text = ConvertString(kAss.translataion);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        if (this.GetComponent<CanvasGroup>().alpha >= 1)
            StartCoroutine(Enum_Close());
    }

    IEnumerator Enum_Show()
    {
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        CG.alpha = 0;
        while (CG.alpha < 1)
        {
            CG.alpha += Time.deltaTime * 3;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    IEnumerator Enum_Close()
    {
        CanvasGroup CG = this.GetComponent<CanvasGroup>();
        while (CG.alpha > 0)
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
