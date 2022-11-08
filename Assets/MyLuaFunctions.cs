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
        Lua.RegisterFunction("FinishQuest", this, SymbolExtensions.GetMethodInfo(() => FinishQuest(string.Empty)));
        Lua.RegisterFunction("IsQuestToBeStart", this, SymbolExtensions.GetMethodInfo(() => IsQuestToBeStart(string.Empty)));
        Lua.RegisterFunction("IsQuestToBeFinish", this, SymbolExtensions.GetMethodInfo(() => IsQuestToBeFinish(string.Empty)));
        Lua.RegisterFunction("IsQuestFinished", this, SymbolExtensions.GetMethodInfo(() => IsQuestFinished(string.Empty)));
    }

    void OnDisable()
    {
        // Note: If this script is on your Dialogue Manager & the Dialogue Manager is configured
        // as Don't Destroy On Load (on by default), don't unregister Lua functions.
        Lua.UnregisterFunction("GameObjectExists"); // <-- Only if not on Dialogue Manager.
        Lua.UnregisterFunction("ItemTitle"); // <-- Only if not on Dialogue Manager.
    }

    public bool GameObjectExists(string name)
    {
        return GameObject.Find(name) != null;
    }

    public void StartQuest(string name)
    {
        QuestManager.Instance.activateQuest(name);
    }

    public void FinishQuest(string name)
    {
        QuestManager.Instance.finishQuest(name);
    }
    public bool IsQuestToBeStart(string name)
    {
        return QuestManager.Instance.getQuestState(name) == QuestState.grantable;
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