using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SymbolHandler : MonoBehaviour
{
    public Text aObject;
    [SerializeField] private GameObject windowManager;

    public void SetSymbol()
    {
        Tradings.symbol = aObject.text;
        windowManager = GameObject.FindGameObjectWithTag("Manager");
        Manager manager = (Manager) windowManager.GetComponent("Manager");
        manager.ChangeWindows();
    }
}
