using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    public string name;
    ItemInfo info;
    public List<Token> tokens = new List<Token>();
    string titleChange = "";
    // Start is called before the first frame update
    void Start()
    {
        info = ItemManager.Instance.getInfo(name);
        //add start token
        addToken(info.start);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToken(string tokenName)
    {
        //todo should have token position
        Token token = new Token(tokenName);
        tokens.Add(token);

        updateTitleChange();
    }

    public void updateTitleChange()
    {
        titleChange = "";
        //sort by size of token
        foreach (var token in tokens)
        {
            var combination = ItemTokenCombination.Instance.getInfo(name, token.name);
            if (combination.Count>0)
            {
                titleChange += combination[0].titleChange+" ";
            }
        }
    }

    public string fullTitle()
    {
        return titleChange + info.title;
    }
}
