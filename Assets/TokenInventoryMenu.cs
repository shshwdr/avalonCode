using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInventoryMenu : MonoBehaviour
{
    public TokenInventoryCellEmpty[] emptyInventoryCells;
    public CodeBookMenu bookMenu;
    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn("updateTokenInventory", inventoryChange);


        emptyInventoryCells = GetComponentsInChildren<TokenInventoryCellEmpty>(true);
        for (int i = 0; i < emptyInventoryCells.Length; i++)
        {
            emptyInventoryCells[i].index = i;
        }
    }

    public void inventoryChange()
    {
        for (int i = 0; i < TokenInventoryManager.Instance.tokens.Length; i++)
        {
            if (TokenInventoryManager.Instance.tokens[i] != null)
            {
                bookMenu.createToken(TokenInventoryManager.Instance.tokens[i]);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
