using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUtils : Singleton<DialogueUtils>
{
    PlayerPickup playerPick;
    public bool isInDialogue;
    public List<GameObject> hideItems;
    // Start is called before the first frame update
    void Start()
    {
        playerPick = GameObject.FindObjectOfType<PlayerPickup>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startConversation()
    {
        playerPick.isPickingUp = true;
        isInDialogue = true;
        MouseInputManager.Instance.selectItem(null);
        foreach (var item in hideItems)
        {
            item.SetActive(false);
        }
    }

    public void endConversation()
    {
        playerPick.isPickingUp = false;
        isInDialogue = false;
        foreach (var item in hideItems)
        {
            item.SetActive(true);
        }
        //QuestManager.Instance.updateQuestFromNoWhere();
    }
}
