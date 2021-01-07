using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _STR
{
    public const string DTABLE_BASEPATH = "Assets/Resources/";

    public const string DTABLE_GAME = "TableData/Feedback";
    //public const string DTABLE_ITEM = "TableData/item";
}


public class CAsset
{
    public int m_id = 0;                // id
}

// 스테이지 정보 파일
public class AssetFeedback : CAsset
{
    public int type = 0;   // 계급
    public bool isProfit = false;   // 버프 / 디버프
    public string title = "";   // 제목
    public string lore = "";   // 설명
    public int Value; // 버프 / 디버프 값
    public string translataion = "";   // 번역본
}


/*
 *  어셋 정보 관리자
 */
public class AssetMgr 
{
    static AssetMgr instance = null;
    public static AssetMgr Inst
    {
        get
        {
            if (instance == null)
                instance = new AssetMgr();

            return instance;
        }
    }

    private AssetMgr()
    {
        IsInstalled = false;
    }

    //----------------------------------------------------------
    public bool IsInstalled { get; set; }
    public List<AssetFeedback> m_AssFeedbacks = new List<AssetFeedback>();
    //public List<AssetItem> m_AssItems = new List<AssetItem>();

    public void Initialize()
    {
        Initialzie_Game(_STR.DTABLE_GAME);
        //Initialize_Item(_STR.DTABLE_ITEM);
        //Initialzie_Stage(_STR.DTABLE_BASEPATH + _STR.DTABLE_STAGE);
        IsInstalled = true;
    }

    public int GetAssetGameCount() {
        return m_AssFeedbacks.Count;
    }
    //public int GetAssetStageCount() {
    //    return m_AssStages.Count;
    //}


    public void Initialzie_Game( string pathName )
    {
        List<string[]> kDatas = CSVParser.Load(pathName);
        if (kDatas == null)
            return;

        for (int i = 1; i < kDatas.Count; i++)
        {
            string[] aStr = kDatas[i];
            AssetFeedback kGame = new AssetFeedback();
            int index = 0;

            kGame.m_id = int.Parse(aStr[index++]);
            kGame.type = int.Parse(aStr[index++]);
            int value = int.Parse(aStr[index++]);
            kGame.isProfit = value == 1 ? true : false;
            kGame.title = aStr[index++];
            kGame.lore = aStr[index++];
            kGame.Value = int.Parse(aStr[index++]);
            kGame.translataion = aStr[index++];

            m_AssFeedbacks.Add(kGame);
       }
        kDatas.Clear();
    }

    //public void Initialize_Item( string pathName )
    //{
    //    List<string[]> kDatas = CSVParser.Load(pathName);
    //    if (kDatas == null)
    //        return;
    //
    //    for (int i = 1; i < kDatas.Count; i++)
    //    {
    //        string[] aStr = kDatas[i];
    //        AssetItem kItem = new AssetItem();
    //
    //        int index = 0;
    //
    //        kItem.m_id = int.Parse(aStr[index++]);
    //        kItem.m_nType = int.Parse(aStr[index++]);
    //        kItem.m_sPrefabName = aStr[index++];
    //        kItem.m_fValue = float.Parse(aStr[index++]);
    //        kItem.m_sDesc = aStr[index++];
    //
    //        m_AssItems.Add(kItem);
    //    }
    //    kDatas.Clear();
    //}



    //------------------------------------------------------------------
    public AssetFeedback GetAssetFeedback(int id )
    {
        if(id >= 0 && id < m_AssFeedbacks.Count) {
            return m_AssFeedbacks[id];
        }
        return null;
    }

    //------------------------------------------------------------------
    //public AssetItem GetAssetItem(int id)
    //{
    //    if (id > 0 && id <= m_AssItems.Count)
    //    {
    //        return m_AssItems[id - 1];
    //    }
    //    return null;
    //}
    //
}
