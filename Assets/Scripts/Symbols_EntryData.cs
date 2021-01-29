using System;
using System.Collections.Generic;

[Serializable]
public class Symbols_EntryData
{
    public List<Symbol> symbols = new List<Symbol>();
}

[Serializable]
public class Symbol
{
    public string symbol;
    public List<string> permissions = new List<string>();
}
