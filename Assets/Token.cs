using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token
{
    public string name;
    public TokenInfo info;
    public Vector2Int index;
    public bool isInventory = false;
    public int indexInt;
    InteractiveItem item;

    public Token(string _n, Vector2Int _index, InteractiveItem _item)
    {
        name = _n;
        info = TokenManager.Instance.getInfo(name);
        index = _index;
        item = _item;
    }

    public void updateGridPosition(Vector2Int _index)
    {
        isInventory = false;
        index = _index;

        //update item
    }

    public void updateInventoryPosition(int _index)
    {

        indexInt = _index;
        //add into invenroty
        if (!isInventory)
        {
            isInventory = true;
            if (!item)
            {
                Debug.LogError("it should have an item");
                return;
            }
            item.removeToken(this);
            item = null;
        }
    }
}