using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryCell:MonoBehaviour
{

    public string name;
    public Image renderer;

    public void init(string n)
    {
        if(n == "")
        {
            renderer.gameObject.SetActive(false);
            renderer.sprite = null;
            return;
        }
        renderer.gameObject.SetActive(true);
        name = n;
        renderer.sprite = Resources.Load<Sprite>("item/" + name);
    }
}