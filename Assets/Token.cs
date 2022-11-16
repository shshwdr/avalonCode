//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Token
//{
//    public string name;
//    public TokenInfo info;
//    public Vector2Int index;
//    public bool isInventory = false;
//    public int indexInt;
//    TokenableItem item;

//    public Token(string _n, Vector2Int _index, TokenableItem _item)
//    {
//        name = _n;
//        info = TokenManager.Instance.getInfo(name);
//        index = _index;
//        item = _item;
//    }

//    public Token(string _n, int _index)
//    {
//        name = _n;
//        info = TokenManager.Instance.getInfo(name);
//        indexInt = _index;

//    }

//    public void updateGridPosition(Vector2Int _index, TokenableItem _item)
//    {
//        index = _index;

//        //update item
//        if (isInventory)
//        {

//            isInventory = false;
//            if (item)
//            {
//                Debug.LogError("it should not have an item");
//                return;
//            }
//            TokenInventoryManager.Instance.removeToken(indexInt);
//            item = _item;
//            item.addToken(this);
//        }
//    }

//    public void updateInventoryPosition(int _index)
//    {

//        indexInt = _index;
//        //add into invenroty
//        if (!isInventory)
//        {
//            isInventory = true;
//            if (!item)
//            {
//                Debug.LogError("it should have an item");
//                return;
//            }
//            TokenInventoryManager.Instance.addToken(this);
//            item.removeToken(this);
//            item = null;
//        }
//    }
//}