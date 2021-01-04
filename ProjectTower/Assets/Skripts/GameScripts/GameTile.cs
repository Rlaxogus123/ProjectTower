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
                case 1: fSide = -2; break;
                case 2: 
                    fSide = -4;
                    kTile.GetComponent<EntityTile>().m_bAttackTile = true;
                    kTile.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
                    break;
                case 3: fSide = -2; break;
                case 4:
                    fSide = 0;
                    IsRight = !IsRight;
                    pointIndex = i;
                    break;
            }
            kTile.transform.position = m_prefabTile.transform.position + 
                new Vector3((100 *_index) + (IsRight ? Mathf.Abs(fSide) : fSide), 0.6f * i, 0);
            kTile.SetActive(true);
            m_TileList[_index].Add(kTile);
        }
    }

    void Update()
    {
        
    }
}
