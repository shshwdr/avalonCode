using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Linq;
using System.Collections.Generic;

public class MyLuaFunctions : Singleton<MyLuaFunctions>
{

    void OnEnable()
    {
        Lua.RegisterFunction("GameObjectExists", this, SymbolExtensions.GetMethodInfo(() => GameObjectExists(string.Empty)));

        Lua.RegisterFunction("ItemTitleContains", this, SymbolExtensions.GetMethodInfo(() => ItemTitleContains(string.Empty, string.Empty)));

        //quest related
        Lua.RegisterFunction("StartQuest", this, SymbolExtensions.GetMethodInfo(() => StartQuest(string.Empty)));
        Lua.RegisterFunction("GrantQuest", this, SymbolExtensions.GetMethodInfo(() => GrantQuest(string.Empty)));
        Lua.RegisterFunction("SetToReturnToNPC", this, SymbolExtensions.GetMethodInfo(() => SetToReturnToNPC(string.Empty)));
        Lua.RegisterFunction("FinishQuest", this, SymbolExtensions.GetMethodInfo(() => FinishQuest(string.Empty)));
        Lua.RegisterFunction("IsQuestToBeStart", this, SymbolExtensions.GetMethodInfo(() => IsQuestToBeStart(string.Empty)));
        Lua.RegisterFunction("IsQuestActive", this, SymbolExtensions.GetMethodInfo(() => IsQuestActive(string.Empty)));
        Lua.RegisterFunction("IsQuestToBeFinish", this, SymbolExtensions.GetMethodInfo(() => IsQuestToBeFinish(string.Empty)));
        Lua.RegisterFunction("IsQuestFinished", this, SymbolExtensions.GetMethodInfo(() => IsQuestFinished(string.Empty))); 

        //show grid

        Lua.RegisterFunction("ShowGrid", this, SymbolExtensions.GetMethodInfo(() => ShowGrid(string.Empty)));

        //token

        Lua.RegisterFunction("GetToken", this, SymbolExtensions.GetMethodInfo(() => GetToken(string.Empty)));

        //item

        Lua.RegisterFunction("UseItem", this, SymbolExtensions.GetMethodInfo(() => UseItem(string.Empty)));
        Lua.RegisterFunction("HasItem", this, SymbolExtensions.GetMethodInfo(() => HasItem(string.Empty)));

        //state

        Lua.RegisterFunction("ChangeItemState", this, SymbolExtensions.GetMethodInfo(() => ChangeItemState(string.Empty, string.Empty)));
        Lua.RegisterFunction("ItemState", this, SymbolExtensions.GetMethodInfo(() => ItemState(string.Empty)));

        //tutorial
        Lua.RegisterFunction("ShowTutorialInventoryText", this, SymbolExtensions.GetMethodInfo(() => ShowTutorialInventoryText()));
        
    }

    void OnDisable()
    {
        // Note: If this script is on your Dialogue Manager & the Dialogue Manager is configured
        // as Don't Destroy On Load (on by default), don't unregister Lua functions.
        Lua.UnregisterFunction("GameObjectExists"); // <-- Only if not on Dialogue Manager.
        Lua.UnregisterFunction("ItemTitle"); // <-- Only if not on Dialogue Manager.
    }
    public void ChangeItemState(string name, string s)
    {
        foreach (var stateItem in GameManager.FindObjectsOfType<StateItem>())
        {
            if (stateItem.name == name)
            {
                stateItem.setState(int.Parse(s));
            }
        }
    }
    public int ItemState(string name)
    {
        foreach (var stateItem in GameManager.FindObjectsOfType<StateItem>())
        {
            if (stateItem.name == name)
            {
                return stateItem.state;
            }
        }
        Debug.LogError("no state found " + name);
        return -1;

    }
    public void ShowGrid(string name)
    {
        MouseInputManager.Instance.selectItem(GameObject.Find(name).GetComponent<TokenableItem>());

    }
    public bool HasItem(string name)
    {
        return ItemInventoryManager.Instance.hasTokenableItem(name);

    }
    public void UseItem(string name)
    {
         ItemInventoryManager.Instance.removeTokenableItem(name);

    }

    public void GetToken(string name)
    {
        TokenInventoryManager.Instance.addToken(name);

    }

    public bool GameObjectExists(string name)
    {
        return GameObject.Find(name) != null;
    }

    public void StartQuest(string name)
    {
        QuestManager.Instance.activateQuest(name);
    }
    public void GrantQuest(string name)
    {
        QuestManager.Instance.grantQuest(name);
        QuestManager.Instance.updateQuestFromNoWhere();
    }

    public void SetToReturnToNPC(string name)
    {
        QuestManager.Instance.setToReturnToNPC(name);
    }

    public void ShowTutorialInventoryText()
    {
        TutorialText.Instance.showInventoryText();
    }
    

    public void FinishQuest(string name)
    {
        QuestManager.Instance.finishQuest(name);
    }
    public bool IsQuestToBeStart(string name)
    {
        return QuestManager.Instance.getQuestState(name) == QuestState.grantable;
    }
    public bool IsQuestActive(string name)
    {
        return QuestManager.Instance.getQuestState(name) == QuestState.active;
    }
    public bool IsQuestToBeFinish(string name)
    {
        return QuestManager.Instance.getQuestState(name) == QuestState.returnToNPC;
    }
    public bool IsQuestFinished(string name)
    {
        return QuestManager.Instance.getQuestState(name) == QuestState.success;
    }

    //public string itemTitle(string name, string token)
    //{
    //    var go = GameObject.Find(name);
    //    if(go == null)
    //    {
    //        return "";
    //    }
    //    else
    //    {
    //        var com = go.GetComponent<TokenableItem>();
    //        if (com != null)
    //        {
    //            var tokens = com.tokens;

    //            List<string> properties = tokens.Select(o => o.name).ToList();
    //            return properties.Contains(token);
    //        }
    //    }
    //    return "";
    //}

    public bool ItemTitleContains(string name, string title)
    {
        var go = GameObject.Find(name);
        if (go == null)
        {
            return false;
        }
        else
        {
            var com = go.GetComponent<TokenableItem>();
            if (com != null)
            {
                return com.fullTitle().Contains(title);
            }
        }
        return false;
    }
}