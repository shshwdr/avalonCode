using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemTokenInfo
{
    public string item;
    public List<string> token;
    public string itemChange;
    //public int isOpposite;
    public string generateToken;
    public string generateInventory;
    public int destroyCombine;
    public int stateChange;
    public bool shouldDestoryWhenCombine { get { return destroyCombine == 1; } }
    //public bool IsOpposite { get { return isOpposite == 1; } }

}
public class ItemTokenCombination : Singleton<ItemTokenCombination>
{

    public Dictionary<string, List<ItemTokenInfo>> infoDict = new Dictionary<string, List<ItemTokenInfo>>();
    public List<ItemTokenInfo> infoList;

    public void init()
    {
        infoList = CsvUtil.LoadObjects<ItemTokenInfo>("itemToken");
        foreach (var info in infoList)
        {
            if (!infoDict.ContainsKey(info.item))
            {
                infoDict[info.item] = new List<ItemTokenInfo>();
            }
            infoDict[info.item].Add( info);
        }
    }
    public List<ItemTokenInfo> getInfo(string infoName)
    {
        if (!infoDict.ContainsKey(infoName))
        {
           // Debug.LogError("no info " + infoName);
            return default(List<ItemTokenInfo>);
        }
        return infoDict[infoName];
    }


    //public List<ItemTokenInfo> getInfo(string infoName,string tokenName)
    //{
    //    if (!infoDict.ContainsKey(infoName))
    //    {
    //        Debug.LogError("no info " + infoName);
    //        return default(List<ItemTokenInfo>);
    //    }
    //    List<ItemTokenInfo> res = new List<ItemTokenInfo>();
    //    foreach (var i in infoDict[infoName])
    //    {
    //        if(i.token == tokenName)
    //        {
    //            res.Add(i);
    //        }
    //    }
    //    return res;
    //}
    // Update is called once per frame
    void Update()
    {
        
    }
}
