using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCell : MonoBehaviour
{
    public Text recipeResultLabel;
    public Image recipeResultImage;
    ItemTokenInfo recipeInfo;
    public GameObject recipeIngredients;
    TokenInventoryCell[] tokenCells;
    // Start is called before the first frame update
    void Start()
    {
        tokenCells = GetComponentsInChildren<TokenInventoryCell>(true);
        OnMouseExit();
    }
    public void init(ItemTokenInfo r)
    {
        tokenCells = GetComponentsInChildren<TokenInventoryCell>(true);
        recipeInfo = r;
        if (recipeInfo.itemChange != "")
        {
            var recipeResultName = recipeInfo.itemChange;
            var item = ItemManager.Instance.getInfo(recipeResultName);
            recipeResultLabel.text = item.title;
            recipeResultImage.sprite = item.image;
        }
        else if (recipeInfo.generateInventory != "")
        {
            var recipeResultName = recipeInfo.generateInventory;
            var item = ItemManager.Instance.getInfo(recipeResultName);
            recipeResultLabel.text = item.title;
            recipeResultImage.sprite = item.image;
        }
        else if(recipeInfo.generateToken!="")
        {

            var recipeResultName = recipeInfo.generateToken;
            var token = TokenManager.Instance.getInfo(recipeResultName);
            recipeResultLabel.text = token.title;
            recipeResultImage.sprite = token.image;
        }
        int i = 0;
        foreach(var t in recipeInfo.token)
        {
            tokenCells[i].gameObject.SetActive(true);
            tokenCells[i].init(t, false);
            i++;
        }
        for(;i< tokenCells.Length; i++)
        {
            tokenCells[i].gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMouseEnter()
    {
        recipeIngredients.SetActive(true);
    }

    public void OnMouseExit()
    {
        recipeIngredients.SetActive(false);
    }
}
