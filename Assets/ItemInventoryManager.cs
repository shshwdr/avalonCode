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
        TutorialText.Instance.showInventoryText();
        PopupImageManager.Instance.showAnim(item);
    }

    public bool hasTokenableItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public void removeTokenableItem(string itemName)
    {
        if (!items.Contains(itemName))
        {

            Debug.Log("remove item not exsit " + itemName);
        }
        items.Remove(itemName);
        EventPool.Trigger("updateTokenableItemInventory");
    }
}
