using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject symbolPicker;

    [SerializeField] private GameObject systemTradings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeWindows()
    {
        symbolPicker.SetActive(false);
        systemTradings.SetActive(true);
    }
}
