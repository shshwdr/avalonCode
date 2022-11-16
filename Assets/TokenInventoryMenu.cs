using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInventoryMenu : MonoBehaviour
{
    TokenInventoryCell[] emptyInventoryCells;
    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn("updateTokenInventory", inventoryChange);


        emptyInventoryCells = GetComponentsInChildren<TokenInventoryCell>(true);
        inventoryChange();
    }

    public void inventoryChange()
    {

        int i = 0;
        for (; i < TokenInventoryManager.Instance.tokens.Count; i++)
        {
            emptyInventoryCells[i].init(TokenInventoryManager.Instance.tokens[i],true);
        }
        for (; i < emptyInventoryCells.Length; i++)
        {
            emptyInventoryCells[i].init("", true);
        }
    }
}
