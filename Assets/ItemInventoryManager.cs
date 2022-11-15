using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryManager : Singleton<ItemInventoryManager>
{
    public List<string> items = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
    }

    public void addTokenableItem(string item)
    {
        items.Add(item);
        QuestManager.Instance.updateQuestFromNoWhere();
        EventPool.Trigger("updateTokenableItemInventory");
    }

    public bool hasTokenableItem(string itemName)
    {
        foreach (var t in items)
        {
            if (t != null && t == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void removeTokenableItem(string itemName)
    {
        foreach (var t in items)
        {
            if (t != null && t == itemName)
            {
                items.Remove(t);
                EventPool.Trigger("updateTokenableItemInventory");
                return;
            }
        }
        Debug.Log("remove item not exsit " + itemName);
    }
}
