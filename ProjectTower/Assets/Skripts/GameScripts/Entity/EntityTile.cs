using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTile: MonoBehaviour
{
    public bool m_bAttackTile;
    public bool bRight;

    private bool bCurBreak = false;
    public bool m_bBreakTile = false;
    public int nIndex;

    void Start()
    {
    }

    void Update()
    {
        if(bCurBreak != m_bBreakTile)
        {
            if (m_bBreakTile)
            {
                this.GetComponent<SpriteRenderer>().sprite = Resources.Load(string.Format("Sprite/Tower/BreakTile{0}", Random.Range(0, 3)), typeof(Sprite)) as Sprite;
                this.GetComponent<SpriteRenderer>().color = Color.red;
                bCurBreak = m_bBreakTile;
            }
        }
        
        else if (m_bAttackTile)
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
    }
}
