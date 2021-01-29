using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Symbols_EntryUI : MonoBehaviour
{
    [SerializeField] private Text entrySymbolText = null;

    public void Initialize(string symbol)
    {
        entrySymbolText.text = symbol;
    }
}
