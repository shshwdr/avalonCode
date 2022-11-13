using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInventoryManager : Singleton<TokenInventoryManager>
{
    public Token[] tokens;
    // Start is called before the first frame update
    void Start()
    {
        tokens = new Token[4] {null,null,null,null };
    }

    public void addToken(Token token)
    {
        var index = token.indexInt;

        if (tokens[index] != null)
        {
            Debug.LogError("token inventory should be empty " + index);
        }

        tokens[index] = token;
        //EventPool.Trigger("updateTokenInventory");
    }

    public bool hasToken(string tokenName)
    {
        foreach(var t in tokens)
        {
            if(t.name == tokenName)
            {
                return true;
            }
        }
        return false;
    }

    public void removeToken(int index)
    {
        if (tokens[index] == null)
        {
            Debug.LogError("token inventory should not be empty " + index);
        }

        tokens[index] = null;
        EventPool.Trigger("updateTokenInventory");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
