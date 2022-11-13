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
    public List<Token> tokens = new List<Token>();
    //string titleChange = "";
    public SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        info = ItemManager.Instance.getInfo(name);
        //add start token
        if (info.start!=null && info.start.Length>0)
        {
            addToken(info.start, new Vector2Int(0, 0));

        }
        renderer.sprite = Resources.Load<Sprite>("item/" + name);
        //updateTitleChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToken(string tokenName, Vector2Int index)
    {
        //todo should have token position 
        Token token = new Token(tokenName, index,this);
        tokens.Add(token);

        updateTokens();
    }

    public void addToken(Token token)
    {
        tokens.Add(token);

        updateTokens();
    }

    public void removeToken(Token token)
    {
        tokens.Remove(token);
        //updateTitleChange();
    }

    public bool canGeneration()
    {
        
        return generatableCombination!=null;
    }

    public ItemTokenInfo getGeneratableCombination()
    {
        return generatableCombination;
    }

    public void generate()
    {

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
                if (!combTokens.Contains(t.name))
                {
                    succeed = false;
                    break;
                }
            }
            if (succeed)
            {
                generatableCombination = comb;
                EventPool.Trigger("selectInteractiveItem");
            }
        }
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
            res += " => " + generatableCombination.itemChange;
        }

        return res;
    }
}
