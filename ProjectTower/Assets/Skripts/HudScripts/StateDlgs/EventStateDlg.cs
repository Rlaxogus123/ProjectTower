using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStateDlg : MonoBehaviour
{
    void Start()
    {

    }
    public void Initialize()
    {

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
