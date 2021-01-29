using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Microsoft.CSharp;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Tradings : MonoBehaviour
{
    [SerializeField] private int maxTradingEntries = 100;
    [SerializeField] private Transform tradingsHolderTransform = null;
    [SerializeField] private GameObject tradingGameObject = null;

    public readonly string BinanceUrl = "https://api.binance.com/";
    public readonly string maxTrades = "1000";
    public static string symbol { get; set; }

    private readonly Color32 seller = new Color32(255, 153, 153, 255);
    private readonly Color32 buyer = new Color32(153, 255, 187, 255);


    [Header("Test")]
    [SerializeField] Tradings_EntryData testEntryData = new Tradings_EntryData();

    private Tradings_SaveData tradings = new Tradings_SaveData();

    private string SavePath => $"{Application.persistentDataPath}/response.json";

    private void Start()
    {
        InvokeRepeating("GetTradings", 0f, 2f);
    }

    private void GetTradings()
    {
        StartCoroutine(GetRequest($"{BinanceUrl}api/v3/trades?symbol={symbol}&limit={maxTrades}"));
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
            /*else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }*/

            var completeJson = "{\"Tradings\":" + webRequest.downloadHandler.text + "}";
            tradings = JsonUtility.FromJson<Tradings_SaveData>(completeJson);

            UpdateUI(tradings);
            SaveTradings(tradings);
        }
    }

    private void UpdateUI(Tradings_SaveData savedTradings)
    {
        foreach (Transform child in tradingsHolderTransform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxTradingEntries; i++)
        {
            var tradeObject = Instantiate(tradingGameObject, tradingsHolderTransform);
            tradeObject.GetComponent<Image>().color = savedTradings.Tradings[i].isBuyerMaker ? buyer : seller;
            tradeObject.GetComponent<Tradings_EntryUI>().Initialize(savedTradings.Tradings[i]);
        }
    }

    public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);
        return dtDateTime;
    }

    [ContextMenu("Add test trade")]
    public void AddTestEntry()
    {
        AddEntry(testEntryData);
    }

    public void AddEntry(Tradings_EntryData tradingsEntryData)
    {
        Tradings_SaveData savedTrades = GetSavedTradings();

        if (savedTrades.Tradings.Count < maxTradingEntries)
        {
            savedTrades.Tradings.Add(tradingsEntryData);
        }

        if (savedTrades.Tradings.Count > maxTradingEntries)
        {
            savedTrades.Tradings.RemoveRange(maxTradingEntries, savedTrades.Tradings.Count - maxTradingEntries);
        }

        UpdateUI(savedTrades);
        SaveTradings(savedTrades);
    }

    private Tradings_SaveData GetSavedTradings()
    {
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            return new Tradings_SaveData();
        }

        using (StreamReader stream = new StreamReader(SavePath))
        {
            string json = stream.ReadToEnd();
            var completeJson = "{\"Tradings\":" + json + "}";

            return JsonUtility.FromJson<Tradings_SaveData>(completeJson);
        }
    }

    private void SaveTradings(Tradings_SaveData tradingsSaveData)
    {
        using (StreamWriter stream = new StreamWriter(SavePath))
        {
            string json = JsonUtility.ToJson(tradingsSaveData, true);
            stream.Write(json);
        }
    }
}
