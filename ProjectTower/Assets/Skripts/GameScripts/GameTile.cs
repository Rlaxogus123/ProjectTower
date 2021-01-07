using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile : MonoBehaviour
{
    [SerializeField] GameObject m_prefabTile;
    public List<GameObject>[] m_TileList = new List<GameObject>[4];



    void Start()
    {
        for(int i = 0; i <4; i++)
        {
            m_TileList[i] = new List<GameObject>();
        }
    }

    public void ViewEnableTile(int _index)
    {
        for (int i = 0; i < m_TileList[_index].Count; i++)
        {
            m_TileList[_index][i].GetComponent<SpriteRenderer>().sortingLayerName = "View";
        }
    }

    public void ViewDisableTile(int _index)
    {
        for (int i = 0; i < m_TileList[_index].Count; i++)
        {
            m_TileList[_index][i].GetComponent<SpriteRenderer>().sortingLayerName = "Tile";
        }
    }

    public void SetTile(int _index, int _number)
    {
        if (m_TileList[_index].Count > 0)
        {
            for (int i = 0; i < m_TileList[_index].Count; i++)
                Destroy(m_TileList[_index][i].gameObject);
        }
        m_TileList[_index].Clear();

        int pointIndex = 0;
        bool IsRight = false;
        float fSide = 0.0f;
        for(int i = 0; i < _number; i++)
        {
            GameObject kTile = Instantiate(m_prefabTile, this.transform);
            switch (i - pointIndex)
            {
                case 1: fSide = -2.5f; break;
                case 2: 
                    fSide = -5f;
                    kTile.GetComponent<EntityTile>().m_bAttackTile = true;
                    break;
                case 3: fSide = -2.5f; break;
                case 4:
                    fSide = 0;
                    IsRight = !IsRight;
                    pointIndex = i;
                    break;
            }
            kTile.GetComponent<EntityTile>().nIndex = i;
            kTile.transform.position = m_prefabTile.transform.position + 
                new Vector3((100 *_index) + (IsRight ? Mathf.Abs(fSide) : fSide), 1f * i, 0);
            kTile.SetActive(true);
            m_TileList[_index].Add(kTile);
        }
    }

    void Update()
    {
        
    }
}
