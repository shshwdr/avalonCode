using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : InteractiveItem
{
    ItemInfo itemInfo;
    public GameObject compressObject;
    //NPC npc;
    public override void Start()
    {
        base.Start();
        itemInfo = ItemManager.Instance.getInfo(  transform.parent.gameObject.name);
        //npc = GetComponent<NPC>();
    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        transform.parent. gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
    }

    public override void prepareUI()
    {
        interactiveText.text = itemInfo.title;
        interactiveSubText.text = itemInfo.subtitle;
    }

    public override void showPickupUI()
    {
        base.showPickupUI();

        if (!compressObject)
        {
            //Debug.LogError("cant hide " + transform.parent);
            return;
        }
        compressObject.SetActive(false);
        TutorialText.Instance.showSpaceText();
    }
    public override void hidePickupUI()
    {
        base.hidePickupUI();
        if (!compressObject)
        {
            //Debug.LogError("cant hide "+transform.parent);
            return;
        }
        compressObject.SetActive(true);
        TutorialText.Instance.hideSpaceText();
    }

    protected override bool canShowInteractUI()
    {
        return true;
        //return npc.isVisible;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
