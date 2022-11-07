using Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TokenableItem : MonoBehaviour
{
    public string name;
    public ItemInfo info;
    public List<Token> tokens = new List<Token>();
    string titleChange = "";
    // Start is called before the first frame update
    void Start()
    {
        info = ItemManager.Instance.getInfo(name);
        //add start token
        if (info.start!=null && info.start.Length>0)
        {
            addToken(info.start, new Vector2Int(0, 0));

        }
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

        updateTitleChange();
    }

    public void addToken(Token token)
    {
        tokens.Add(token);

        updateTitleChange();
    }

    public void removeToken(Token token)
    {
        tokens.Remove(token);
        updateTitleChange();
    }

    public void updateTitleChange()
    {
       var newTitleChange = "";
        //sort by size of token
        foreach (var token in tokens)
        {
            var combinations = ItemTokenCombination.Instance.getInfo(name, token.name);
            foreach(var com in combinations)
            {
                if(!com.IsOpposite)
                {

                    newTitleChange += com.titleChange + " ";
                    break;
                }
            }
        }

        List<string> properties = tokens.Select(o => o.name).ToList();
        foreach (var com in ItemTokenCombination.Instance.getInfo(name))
        {
            if (com.IsOpposite && !properties.Contains(com.token))
            {
                newTitleChange += com.titleChange + " ";
                continue;
            }
        }

        if(tokens.Count == 0)
        {

        }
        if (newTitleChange != titleChange)
        {
            updateOthersAfterUpdateTitle(titleChange, newTitleChange);
            titleChange = newTitleChange;




            EventPool.Trigger("titleChange",fullTitle());

            QuestManager.Instance.updateQuestFromNoWhere();
        }
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
        return titleChange + info.title;
    }
}
