using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowDlg : MonoBehaviour
{
    [SerializeField] Button m_btnClose;
    [SerializeField] Image m_ImgGauge;
    [SerializeField] GameObject m_BlackPannel;
    [SerializeField] Text m_txtNotice;
    [SerializeField] Image m_ImgDice;

    [SerializeField] AudioClip[] Audioclips = new AudioClip[4];
    private AudioSource[] m_AudioSource = new AudioSource[4];

    private bool bGauge;
    private bool bThrow;
    private float fGaugeFill;

    void Start()
    {
        for(int i = 0; i < m_AudioSource.Length; i++)
        {
            m_AudioSource[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
            m_AudioSource[i].clip = Audioclips[i];
            m_AudioSource[i].playOnAwake = false;
            m_AudioSource[i].loop = false;
            m_AudioSource[i].volume = GameMgr.Ins.m_GameInfo.SFXAmount;
            m_AudioSource[i].Stop();
        }

        m_btnClose.onClick.AddListener(Close);
    }
    public void Initialize()
    {
        m_BlackPannel.SetActive(true);

        if (GameMgr.Ins.m_GameScene.m_FSM.IsSwapState()) m_txtNotice.text = "시간 초과!";
        else if (GameMgr.Ins.m_nNowTurn != 0) m_txtNotice.text = "당신의 턴이 아닙니다!";
        else if (GameMgr.Ins.m_ThrowCount < 1) m_txtNotice.text = "주사위를 모두 던지셨습니다!";
        else m_BlackPannel.SetActive(false);

        bGauge = false;
        bThrow = false;
        fGaugeFill = 0.0f;
        m_ImgGauge.fillAmount = fGaugeFill;
        Show();
        StartCoroutine(Enum_Show());
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
    public void Close()
    {
        if (this.GetComponent<CanvasGroup>().alpha >= 1)
        {
            if (!bThrow)
            {
                if (GameMgr.Ins.m_MoveCount[GameMgr.Ins.m_nNowTurn] > 0)
                    GameMgr.Ins.m_GameScene.m_FSM.SetMoveState();
                StartCoroutine(Enum_Close());
            }
        }
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

    public void ThrowDown()
    {
        if (m_BlackPannel.activeSelf)
        {
            Close();
            return;
        }

        if (GameMgr.Ins.m_ThrowCount > 0)
        {
            if (!bGauge)
            {
                bGauge = true;
            }
        }
    }

    public void ThrowUp()
    {
        if (m_BlackPannel.activeSelf)
        {
            Close();
            return;
        }

        if (bGauge)
        {
            bGauge = false;
            bThrow = true;

            int Number = (int)(fGaugeFill / 0.3333333f);
            int nResult = (Number * 2) + Random.Range(1, 3);
            Debug.Log(nResult);

            StartCoroutine(Enum_Dice(nResult));

            GameMgr.Ins.m_ThrowCount--;
            GameMgr.Ins.m_MoveCount[GameMgr.Ins.m_nNowTurn] = nResult;

            fGaugeFill = 0;
            m_ImgGauge.fillAmount = fGaugeFill;
        }
    }

    IEnumerator Enum_Dice(int Number)
    {
        m_ImgDice.gameObject.SetActive(true);
        for(int i = 0; i < 16; i++)
        {
            Debug.Log(string.Format("Sprite/Dice/Dice_{0}_{1:0000}", Number, i));
            m_ImgDice.sprite = Resources.Load(string.Format("Sprite/Dice/Dice_{0}_{1:0000}", Number, i), typeof(Sprite)) as Sprite;
            yield return new WaitForSeconds(0.1f);
        }
        bThrow = false;
    }

    void Update()
    {
        if (GameMgr.Ins.m_GameScene.m_FSM.IsSwapState())
        {
            bGauge = false;
            m_txtNotice.text = "시간 초과!";
            m_BlackPannel.SetActive(true);
        }

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
