using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCell : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCell(QuestInfo info)
    {
        text.text = info.title;
        if(info.state == QuestState.returnToNPC)
        {
            text.color = Color.green;
        }
        else
        {
            text.color = Color.black;
        }
    }
}
