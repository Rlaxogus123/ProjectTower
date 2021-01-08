using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayer : MonoBehaviour
{
    public int m_curTile;
    Vector3 m_Target;
    SpriteRenderer m_Sprite;

    GameObject m_Particle;

    public int stat_Attacked = 0;

    void Start()
    {
        StartCoroutine(Enum_Effect());
    }

    IEnumerator Enum_Effect()
    {
        while(true)
        {
            if(stat_Attacked > 0)
            {

            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void Initialize(Vector3 target)
    {
        m_Sprite = this.GetComponent<SpriteRenderer>();
        m_Target = target;
        if (this.transform.position.x - m_Target.x > 0) m_Sprite.flipX = false;
        else m_Sprite.flipX = true;
    }

    public void SetMove(Vector3 target)
    {
        m_Target = target;
        StartCoroutine(Enum_MoveLerp(target));
    }

    IEnumerator Enum_MoveLerp(Vector3 target)
    {
        while((target - transform.position).magnitude > 0.01f)
        {
            if (m_Target != target)
                break;

            if(this.transform.position.x - m_Target.x > 0) m_Sprite.flipX = false;
            else m_Sprite.flipX = true;

            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 10);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (m_Target == target)
            transform.position = target;
        yield return null;
    }

    void Update()
    {

    }
}
