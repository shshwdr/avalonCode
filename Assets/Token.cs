using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token
{
    public string name;
    public TokenInfo info;
   public Token(string _n)
    {
        name = _n;
        info = TokenManager.Instance.getInfo(name);
    }
}