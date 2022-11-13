using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : InteractiveItem
{
    ItemInfo itemInfo;
    //NPC npc;
    public override void Start()
    {
        base.Start();
        itemInfo = ItemManager.Instance.getInfo(  GetComponentInParent<NPC>().gameObject.name);
        //npc = GetComponent<NPC>();
    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        transform.parent. gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
    }

    public override void prepareUI()
    {
        interactiveText.text = "Talk to " + itemInfo.title+"\n(space)";
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
