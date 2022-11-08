using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
{
    public Dictionary<string, NPC> npcScriptDict = new Dictionary<string, NPC>();

    public NPC getNPC(string name)
    {
        return npcScriptDict[name];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
