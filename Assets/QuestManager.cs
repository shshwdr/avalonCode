using PixelCrushers.DialogueSystem;
using Pool;
using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GeneralTypeAmount
{

    public int amount;
    public string type;
    public string subtype;
}
public class QuestEntry
{
    public int amount;
    public string type;
    public string subtype;
    public string text;
    public QuestState state;
    public bool unlockByLast;
}

public class QuestInfo
{
    public string name;
    public string title;
    public string returnNPC;
    public string activateNext;
    public QuestState state;
    //public GeneralTypeAmount[] reward;
    //public GeneralTypeAmount[] grantBehavior;
    //public GeneralTypeAmount[] activeBehavior;
    public List<string> grantTitleRequest;
    public List<string> grantQuestRequest;



    public string type;
    public string typeCategory;
    public string value;
    //public string[] entriesString;
    //public QuestEntry[] entries;
}
public enum QuestState { unassigned, grantable, active, returnToNPC, success }
public class QuestManager : InfoManager<QuestManager, QuestInfo>
{
    public Dictionary<string, GameObject> itemsDict = new Dictionary<string, GameObject>();
    public Dictionary<string, int> questAmountItemDict = new Dictionary<string, int>();

    public AudioClip addQuestSFX;
    public AudioClip finishQuestSFX;
    public void init()
    {
        infoList = CsvUtil.LoadObjects<QuestInfo>("quest");
        foreach (var info in infoList)
        {
            infoDict[info.name] = info;
        }
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        //build this when all item is ready
        yield return new WaitForSeconds(0.5f);

        updateQuestFromNoWhere();
    }



    public void updateQuestFromNoWhere()
    {

        updateQuestState();
        updateGrantQuest();

        EventPool.Trigger("updateQuest");
        //questController.UpdateQuest();
    }

    //public void addQuestItem(string name, int amount = 1)
    //{
    //    //if(name == "leave")
    //    {
    //        if (!questAmountItemDict.ContainsKey(name))
    //        {
    //            questAmountItemDict[name] = 0;
    //        }
    //        questAmountItemDict[name] += amount;

    //        updateQuestState();
    //        questController.UpdateQuest();
    //    }
    //}
    public List<QuestInfo> activeQuests()
    {
        var res = new List<QuestInfo>();
        foreach (var info in QuestManager.Instance.infoDict.Values)
        {
            if (info.state == QuestState.active || info.state == QuestState.returnToNPC)
            {
                res.Add(info);
            }
        }
        return res;
    }

    public List<QuestInfo> unsignedQuests()
    {
        var res = new List<QuestInfo>();
        foreach (var info in QuestManager.Instance.infoDict.Values)
        {
            if (info.state == QuestState.unassigned)
            {
                res.Add(info);
            }
        }
        return res;
    }
    public void updateQuestState()
    {
        foreach (var info in activeQuests())
        {
            bool isFinished = true;
            bool res;
            //foreach (var entry in info.entries)
            {
                    switch (info.type)
                {
                    case "hasToken":
                        if (!TokenInventoryManager.Instance.hasToken(info.typeCategory))
                        {
                            isFinished = false;
                        }
                        break;
                    //case "setTitleExclude":
                    //    res = MyLuaFunctions.Instance.ItemTitleContains(info.typeCategory, info.value);
                    //    if (!res)
                    //    {
                    //        isFinished = true;
                    //    }
                    //    break;
                    //case "setTitleInclude":
                    //    res = MyLuaFunctions.Instance.ItemTitleContains(info.typeCategory, info.value);
                    //    if (res)
                    //    {
                    //        isFinished = true;
                    //    }
                    //    break;
                    //        //case "questItemAmount":
                    //        //    if (getQuestItemAmount(entry.subtype) >= entry.amount)
                    //        //    {
                    //        //        entry.state = QuestState.success;
                    //        //    }
                    //        //    break;
                    //        //case "inventoryAmount":
                    //        //    if (Inventory.Instance.hasItem(entry.subtype))
                    //        //    {
                    //        //        entry.state = QuestState.success;
                    //        //    }
                    //        //    break;
                    //        //case "variableAmount":
                    //        //    if (DialogueLua.GetVariable(entry.subtype).asInt >= entry.amount)
                    //        //    {

                        //        //        entry.state = QuestState.success;
                        //        //    }
                        //        //    break;
                        //        //case "metNPC":
                        //        //    if (DialogueLua.GetActorField(entry.subtype, "hasTalked").asBool)
                        //        //    {
                        //        //        entry.state = QuestState.success;
                        //        //    }
                        //        //    break;
                        //        //case "fullFriendshipAmount":
                        //        //    int fullFriendshipAmount = 0;
                        //        //    foreach (var npc in NPCManager.Instance.npcDict.Keys)
                        //        //    {
                        //        //        if (DialogueLua.GetActorField(npc, "friendship").asInt >= 100)
                        //        //        {
                        //        //            fullFriendshipAmount += 1;
                        //        //        }
                        //        //    }
                        //        //    if (fullFriendshipAmount >= entry.amount)
                        //        //    {
                        //        //        entry.state = QuestState.success;

                        //        //    }
                        //        //    break;
                }
                //    if (entry.state != QuestState.success)
                //    {
                //        isFinished = false;
                //    }
            }
            if (isFinished)
            {
                info.state = QuestState.returnToNPC;

                var returnNPC = infoDict[info.name].returnNPC;
                if (returnNPC != null)
                {

                    NPCManager.Instance.npcScriptDict[returnNPC].canFinishQuest();
                }
                DialogueLua.SetQuestField(info.name, "State", "returnToNPC");
            }
        }
    }

    public void updateGrantQuest()
    {
        foreach (var info in unsignedQuests())
        {
            if (info.grantQuestRequest != null || info.grantTitleRequest !=null)
            {
                bool allFinished = true;
                foreach (var request in info.grantQuestRequest)
                {
                    if(request == "")
                    {
                        break;
                    }
                    if(infoDict[request].state != QuestState.success)
                    {
                        allFinished = false;
                        break;
                    }

                }

                foreach (var request in info.grantTitleRequest)
                {
                    if (request == "")
                    {
                        break;
                    }
                    var npc = NPCManager.Instance.getNPC(info.returnNPC);
                    if (request.Contains("not_"))
                    {
                        var fullTitle = npc.GetComponent<TokenableItem>().fullTitle();
                        if (fullTitle =="" || fullTitle.Contains(request.Substring(4)))
                        {

                            allFinished = false;
                            break;
                        }
                    }
                    else if (!npc.GetComponent<TokenableItem>().fullTitle().Contains(request))
                    {

                        allFinished = false;
                        break;
                    }

                }

                if (allFinished)
                {
                    grantQuest(info.name);
                }
            }
            else
            {

                grantQuest(info.name);
            }
        }
    }
    void startNextQuest(QuestInfo info)
    {
        if (info.activateNext != null)
        {
            activateQuest(info.activateNext);
        }
    }

    void getReward(QuestInfo info)
    {
        //foreach (var reward in info.reward)
        //{
        //    switch (reward.type)
        //    {
        //        case "friendship":

        //            DialogueLua.SetActorField(reward.subtype, "friendship", DialogueLua.GetActorField(reward.subtype, "friendship").asInt + reward.amount);
        //            DialogueManager.ShowAlert("Friendship increased!");
        //            break;
        //        case "item":
        //            Inventory.Instance.addItem(reward.subtype);
        //            DialogueManager.ShowAlert("Get a " + reward.subtype + "!");//Inventory.Instance.item);
        //            break;
        //        case "inventoryCell":
        //            Inventory.Instance.addInventoryUnlockedCell();
        //            DialogueManager.ShowAlert("Get an extra inventory bag !");//Inventory.Instance.item);
        //            break;
        //    }

        //}
    }

    public int getQuestItemAmount(string name)
    {

        if (!questAmountItemDict.ContainsKey(name))
        {
            return 0;
        }
        return questAmountItemDict[name];
    }

    public QuestState getQuestState(string name)
    {
        var info = getInfo(name);
        return info.state;
    }

    public void changeQuestState(string[] args)
    {
        switch (args[0])
        {
            case "active":
                if (infoDict[args[1]].state != QuestState.active)
                {

                    activateQuest(args[1]);
                }
                break;
            case "success":
                if (infoDict[args[1]].state != QuestState.success)
                {
                    finishQuest(args[1]);
                }
                break;
            case "grantable":
                if (infoDict[args[1]].state != QuestState.grantable)
                {
                    grantQuest(args[1]);
                }
                break;

        }
    }
    public void activateQuest(string name)
    {
        //MusicManager.Instance.playSound(addQuestSFX);
        //doBehaviors(infoDict[name].activeBehavior);
        infoDict[name].state = QuestState.active;
        //questController.UpdateQuest();

        EventPool.Trigger("updateQuest");
        DialogueLua.SetQuestField(name, "State", "active");

        var returnNPC = infoDict[name].returnNPC;
        if (returnNPC != null)
        {
            NPCManager.Instance.npcScriptDict[returnNPC].hideAllQuestMarkers();
        }
    }

    public void doBehaviors(GeneralTypeAmount[] behaviors)
    {
        if (behaviors == null)
        {
            return;
        }
        foreach (var behavior in behaviors)
        {
            switch (behavior.type)
            {
                case "showItem":
                    if (!itemsDict.ContainsKey(behavior.subtype))
                    {
                        Debug.LogError(behavior.subtype + " does not existed in itemsDict");
                    }
                    itemsDict[behavior.subtype].SetActive(true);
                    //GameObject.Find(behavior.subtype).SetActive(true);
                    break;
                case "showNPC":
                    if (!NPCManager.Instance.npcScriptDict.ContainsKey(behavior.subtype))
                    {
                        Debug.LogError(behavior.subtype + " does not existed in npc manager");
                    }
                    NPCManager.Instance.npcScriptDict[behavior.subtype].activate();

                    break;

            }

        }
    }

    public void grantQuest(string name)
    {
        //doBehaviors(infoDict[name].grantBehavior);
        infoDict[name].state = QuestState.grantable;
        DialogueLua.SetQuestField(name, "State", "grantable");

        var returnNPC = infoDict[name].returnNPC;
        if (returnNPC != null)
        {

            NPCManager.Instance.npcScriptDict[returnNPC].willGrantQuest();
        }
    }
    public void finishQuest(string name)
    {
        GameManager.Instance.player.playSuccessAnim();
        //MusicManager.Instance.playSound(finishQuestSFX);
        var info = infoDict[name];
        info.state = QuestState.success;

        //EventPool.Trigger("updateQuest");
        //questController.UpdateQuest();
        DialogueLua.SetQuestField(name, "State", "success");

        getReward(info);
        startNextQuest(info);

        var returnNPC = infoDict[name].returnNPC;
        if (returnNPC != null)
        {

            NPCManager.Instance.npcScriptDict[returnNPC].hideAllQuestMarkers();
        }

        updateQuestFromNoWhere();
    }

    public void updateQuestController()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
