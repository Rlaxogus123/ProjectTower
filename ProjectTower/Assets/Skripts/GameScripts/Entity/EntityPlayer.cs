using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayer : MonoBehaviour
{
    public int m_curTile;
    Vector3 m_Target;

    void Start()
    {
        
    }

    public void Initialize()
    {

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
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 8);
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
