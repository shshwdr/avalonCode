using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token
{
    public string name;
    public TokenInfo info;
    public Vector2Int index;
   public Token(string _n, Vector2Int _index)
    {
        name = _n;
        info = TokenManager.Instance.getInfo(name);
        index = _index;
    }

    public void updatePosition(Vector2Int _index)
    {
        index = _index;
    }
}