using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInventoryManager : Singleton<TokenInventoryManager>
{
    public List<string> tokens = new List<string>();

    public void addToken(string item)
    {
        tokens.Add(item);
        QuestManager.Instance.updateQuestFromNoWhere();
        EventPool.Trigger("updateTokenInventory");
    }

    public bool hasToken(string itemName)
    {
        return tokens.Contains(itemName);
    }

    public void removeToken(string itemName)
    {
        if (!tokens.Contains(itemName))
        {

            Debug.Log("remove item not exsit " + itemName);
        }
        tokens.Remove(itemName);
        EventPool.Trigger("updateTokenInventory");
    }
}
