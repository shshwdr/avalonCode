using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changeable : InteractiveItem
{
    ItemInfo itemInfo;
    TokenableItem item;
    public override void Start()
    {
        base.Start();
        itemInfo = ItemManager.Instance.getInfo(transform.parent.gameObject.name);
        item = GetComponentInParent<TokenableItem>();
        //npc = GetComponent<NPC>();
    }
    public override void interact(PlayerPickup player)
    {
        base.interact(player);
        MouseInputManager.Instance.selectItem(item);
        //transform.parent.gameObject.SendMessage("OnUse", player.transform, SendMessageOptions.DontRequireReceiver);
    }

    public override void prepareUI()
    {
        interactiveText.text = "Check " + itemInfo.title + "\n(space)";
    }

    protected override bool canShowInteractUI()
    {
        return true;
        //return npc.isVisible;
    }
}
