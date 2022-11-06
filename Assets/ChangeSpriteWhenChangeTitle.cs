using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteWhenChangeTitle : ChangeTitleBehavior
{
    Sprite originalSprite;
    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void changeTitleTo_internal(string str)
    {
        originalSprite = GetComponentInChildren<SpriteRenderer>().sprite;
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    protected override void changeTitleFrom_internal(string str)
    {
        GetComponentInChildren<SpriteRenderer>().sprite = originalSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
