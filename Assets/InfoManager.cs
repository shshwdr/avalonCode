using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseInfo
{
    public string name;

    public string desc;
    public string title;
    public string start;

}
public class InfoManager<T,U> :   Singleton<T> where T : MonoBehaviour
{


    public Dictionary<string, U> infoDict = new Dictionary<string, U>();
    public List<U> infoList;
    // Start is called before the first frame update
    void Start()
    {

    }
    public U getInfo(string infoName)
    {
        if (!infoDict.ContainsKey(infoName)){
            Debug.LogError("no info " + infoName);
            return default(U);
        }
        return infoDict[infoName];
    }
    //void init(string csvName)
    //{

    //    infoList = CsvUtil.LoadObjects<U>(csvName);
    //    foreach (var info in upgradableInfos)
    //    {
    //        itemInfoDict[info.name] = info;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
