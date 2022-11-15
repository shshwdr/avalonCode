using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo:BaseInfo
{
    public List<string> start;

}

public class ItemManager : InfoManager<ItemManager, ItemInfo>
{
    // Start is called before the first frame update
    public GameObject interactiveItemPrefab;
    public void init()
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
