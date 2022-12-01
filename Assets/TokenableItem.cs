using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class TokenableItem : MonoBehaviour
{
    public string name;
    public ItemInfo info;
    public List<string> tokens = new List<string>();
    //string titleChange = "";
    public SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        info = ItemManager.Instance.getInfo(name);
        //add start token
        if (info.start!=null && info.start.Count>0 && info.start[0] != "")
        {

            for (int i = 0; i < info.start.Count; i++)
            {
                addToken(info.start[i]);
            }
        }
        renderer.sprite = Resources.Load<Sprite>("item/" + name);
        //updateTitleChange();
    }
    public void deselect()
    {
        foreach(var t in tokens)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void addToken(string tokenName)
    {
        tokens.Add(tokenName);

        updateTokens();
    }


    public void removeToken(string token)
    {
        tokens.Remove(token);
        updateTokens();
    }

    public bool canGeneration()
    {

        bool res = generatableCombination != null;

        //if (res)
        //{
        //    if (generatableCombination.generateInventory != "" && ItemInventoryManager.Instance.hasTokenableItem(generatableCombination.generateInventory))
        //    {
        //        res = false;
        //    }
        //}

        return res;
    }

    public ItemTokenInfo getGeneratableCombination()
    {
        return generatableCombination;
    }

    public void generate()
    {
        if (generatableCombination != null)
        {

            tokens.Clear();
            bool shouldDestory = generatableCombination.shouldDestoryWhenCombine;
            if (generatableCombination.itemChange != "")
            {

                if (generatableCombination.stateChange != 0)
                {
                    GetComponent<StateItem>().setState(generatableCombination.stateChange);
                }
                if (shouldDestory)
                {
                    Destroy(gameObject);
                    var go = Instantiate(ItemManager.Instance.interactiveItemPrefab, transform.position, transform.rotation, transform.parent);
                    go.GetComponent<TokenableItem>().name = generatableCombination.itemChange;
                }
                else
                {
                    GetComponent<TokenableItem>().name = generatableCombination.itemChange;
                    Start();
                }
                //todo check if need to destroy
               // Destroy(gameObject);
                MouseInputManager.Instance.selectItem(null);
            }
            if (generatableCombination!=null && generatableCombination.generateInventory != "")
            {
                ItemInventoryManager.Instance.addTokenableItem(generatableCombination.generateInventory);
            }
            if (generatableCombination != null && generatableCombination.generateToken != "")
            {
                addToken(generatableCombination.generateToken);
            }
            //else
            //{
            //    Debug.LogError("no item change and no token generation for " + generatableCombination.item);
            //}

            //if (shouldDestory)
            //{
            //    Destroy(gameObject);
            //}
        }
        else
        {
            Debug.LogError("generation without generatableCombination");
        }
        generatableCombination = null;
        EventPool.Trigger("selectInteractiveItem");
        QuestManager.Instance.updateQuestFromNoWhere();
    }
    ItemTokenInfo generatableCombination;

    public void updateTokens()
    {
        generatableCombination = null;
        var itemName = name;
        if (GetComponent<NPC>())
        {
            itemName = "NPC";
        }
        //sort by size of token

        var combinations = ItemTokenCombination.Instance.getInfo(itemName);
        if(combinations == null)
        {
            EventPool.Trigger("selectInteractiveItem");
            return;
        }
        foreach(var comb in combinations)
        {
            var combTokens = comb.token;
            if (combTokens.Count != tokens.Count)
            {
                continue;
            }
            bool succeed = true;
           // List<string> properties = tokens.Select(o => o.name).ToList();
            foreach (var t in tokens)
            {
                if (!combTokens.Contains(t))
                {
                    succeed = false;
                    break;
                }
            }
            if (succeed)
            {
                generatableCombination = comb;
                EventPool.Trigger("selectInteractiveItem");
                return;
            }
        }

        EventPool.Trigger("selectInteractiveItem");
        //foreach (var token in tokens)
        //{
        //    var combinations = ItemTokenCombination.Instance.getInfo(itemName, token.name);
        //    foreach(var com in combinations)
        //    {
        //        if (com.generateToken != null && com.generateToken != "")
        //        {
        //            //generate token and consume token
        //            tokens.Remove(token);
        //            tokens.Add(new Token(com.generateToken, token.index, this));
        //            updateTitleChange();

        //            EventPool.Trigger("selectInteractiveItem");
        //            return;
        //        }
        //        //if (!com.IsOpposite)
        //        //{

        //        //    newTitleChange += com.titleChange + " ";
        //        //    break;
        //        //}
        //    }
        //}

        //List<string> properties = tokens.Select(o => o.name).ToList();
        //foreach (var com in ItemTokenCombination.Instance.getInfo(itemName))
        //{
        //    if (com.IsOpposite && !properties.Contains(com.token))
        //    {
        //        newTitleChange += com.titleChange + " ";
        //        continue;
        //    }
        //}

        //if(tokens.Count == 0)
        //{

        //}

        //should sort titles too
        //if (Regex.Replace(newTitleChange, @"\s", "") != Regex.Replace(titleChange, @"\s", ""))
        //{
        //    updateOthersAfterUpdateTitle(titleChange, newTitleChange);
        //    titleChange = newTitleChange;




        //    EventPool.Trigger("titleChange",fullTitle());

        //    QuestManager.Instance.updateQuestFromNoWhere();
        //}
    }

    void updateOthersAfterUpdateTitle(string old,string newT)
    {
        foreach(var b in GetComponents<ChangeTitleBehavior>())
        {
            b.changeTitleFrom(old);
        }

        foreach (var b in GetComponents<ChangeTitleBehavior>())
        {
            b.changeTitleTo(newT);
        }
    }

    public string fullTitle()
    {
        if(info == null)
        {
            //might be better way..
            return "";
            //Debug.LogError("?");
        }
        var res = info.title;

        if (canGeneration() && generatableCombination.itemChange!="")
        {
            res += " => " + ItemManager.Instance.getInfo( generatableCombination.itemChange).title;
        }

        return res;
    }
}
