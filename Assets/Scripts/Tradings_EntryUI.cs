using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Tradings_EntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI entryIdText = null;
    [SerializeField] private TextMeshProUGUI entryDateText = null;
    [SerializeField] private TextMeshProUGUI entryPriceText = null;
    [SerializeField] private TextMeshProUGUI entryQtyText = null;
    [SerializeField] private TextMeshProUGUI entryQuoteQtyText = null;
    [SerializeField] private bool entryIsBuyerMaker = true;

    public void Initialize(Tradings_EntryData tradingsEntryData)
    {
        entryIdText.text = tradingsEntryData.id.ToString();
        entryDateText.text = Tradings.UnixTimeStampToDateTime(tradingsEntryData.time).ToString();
        entryPriceText.text = tradingsEntryData.price;
        entryQtyText.text = tradingsEntryData.qty;
        entryQuoteQtyText.text = tradingsEntryData.quoteQty;
        entryIsBuyerMaker = tradingsEntryData.isBuyerMaker;
    }
}
