using Sinbad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TokenInfo : BaseInfo
{
    public Sprite image { get { return Resources.Load<Sprite>("token/" + name); } }
    public string state;
}
public class TokenManager : InfoManager<TokenManager,TokenInfo>
{
    // Start is called before the first frame update


    public void init()
    {

        infoList = CsvUtil.LoadObjects<TokenInfo>("token");
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
