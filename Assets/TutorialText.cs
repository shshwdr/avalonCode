using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : Singleton<TutorialText>
{
    public GameObject textObject;
    public GameObject inventoryTextObject;

    int triggerCount = 3;
    int inventoryTriggerCount = 1;
    public void triggerSpace()
    {
        triggerCount--;
        
    }
    public void triggerInventory()
    {
        inventoryTriggerCount--;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showSpaceText()
    {
        if (triggerCount >= 0)
        {
            textObject.SetActive(true);
        }
    }
    public void hideSpaceText()
    {
        textObject.SetActive(false);
    }

    public void showInventoryText()
    {

        if (inventoryTriggerCount >= 0)
        {
            inventoryTextObject.SetActive(true);
        }
    }
    public void hideInventoryText()
    {
        inventoryTextObject.SetActive(false);
    }
}
