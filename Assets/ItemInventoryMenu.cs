using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryMenu : MonoBehaviour
{
    public GameObject panel;
    ItemInventoryCell[] emptyInventoryCells;
    // Start is called before the first frame update
    void Start()
    {
        EventPool.OptIn("updateTokenableItemInventory", inventoryChange);


        emptyInventoryCells = GetComponentsInChildren<ItemInventoryCell>(true);
        inventoryChange();
        panel.SetActive(false);
        transform.position = Vector3.zero;
    }

    public void inventoryChange()
    {

        int i = 0;
        for (; i < ItemInventoryManager.Instance.items.Count; i++)
        {
            emptyInventoryCells[i].init(ItemInventoryManager.Instance.items[i]);
        }
        for (; i < emptyInventoryCells.Length; i++)
        {
            emptyInventoryCells[i].init("");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            panel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            panel.SetActive(false);
        }
    }
}
