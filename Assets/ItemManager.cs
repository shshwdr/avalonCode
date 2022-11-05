using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public string name;

    public string desc;
    public string title;
    public string start;

}

public class ItemManager : InfoManager<ItemManager, ItemInfo>
{
    // Start is called before the first frame update
    void Start()
    {
        infoList = CsvUtil.LoadObjects<ItemInfo>("item");
        foreach (var info in infoList)
        {
            infoDict[info.name] = info;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
