using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Symbols : MonoBehaviour
{
    public readonly string BinanceUrl = "https://api.binance.com/";
    public List<string> SymbolList = new List<string>();
    [SerializeField] private Transform symbolsHolderTransform = null;
    [SerializeField] private GameObject symbolGameObject = null;

    void Start()
    {
        StartCoroutine(GetRequest($"{BinanceUrl}api/v3/exchangeInfo"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }

            var completeJson = webRequest.downloadHandler.text;
            Symbols_EntryData symbols = JsonUtility.FromJson<Symbols_EntryData>(completeJson);

            foreach (var symbol in 
                from symbol in symbols.symbols 
                from permission in symbol.permissions
                    .Where(permission => permission.Equals("SPOT")) select symbol)
            {
                SymbolList.Add(symbol.symbol);
            }
            UpdateUI(SymbolList);
        }
    }

    private void UpdateUI(List<string> symbols)
    {
        foreach (Transform child in symbolsHolderTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (var symbol in symbols)
        {
            Instantiate(symbolGameObject, symbolsHolderTransform).GetComponent<Symbols_EntryUI>().Initialize(symbol);
        }
    }
}
