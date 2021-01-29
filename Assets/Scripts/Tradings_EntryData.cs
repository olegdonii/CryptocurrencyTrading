using System;

[Serializable]
public class Tradings_EntryData
{
    public long id;

    public string price;

    public string qty;

    public string quoteQty;

    public long time;

    public bool isBuyerMaker;

    public bool isBestMatch;
}
