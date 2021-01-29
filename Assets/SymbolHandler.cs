using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SymbolHandler : MonoBehaviour
{
    public Button yourButton;
    public Text aObject;
    [SerializeField] private GameObject windowManager;

    void Start()
    {
        //windowManager = GameObject.Find("WindowManager");
    }

    public void SetSymbol()
    {
        Tradings.symbol = aObject.text;
        windowManager = GameObject.FindGameObjectWithTag("Manager");
        Manager manager = (Manager) windowManager.GetComponent("Manager");
        manager.ChangeWindows();
        //symbolPicker.SetActive(false);

    }
}
