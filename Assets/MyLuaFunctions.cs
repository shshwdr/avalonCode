using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Linq;
using System.Collections.Generic;

public class MyLuaFunctions : MonoBehaviour
{

    void OnEnable()
    {
        Lua.RegisterFunction("GameObjectExists", this, SymbolExtensions.GetMethodInfo(() => GameObjectExists(string.Empty)));
        Lua.RegisterFunction("ItemTitle", this, SymbolExtensions.GetMethodInfo(() => itemTitle(string.Empty, string.Empty)));
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

    public bool itemTitle(string name, string title)
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