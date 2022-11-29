using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestCell : MonoBehaviour
{
    public QuestTextCellCurrent currentCell;
    public Text title;
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
        var currentText = "❖"+info.title;
        if(info.state == QuestState.returnToNPC)
        {
            //    text.color = Color.green;
            var s = "❖Talk to " + info.returnNPC;
            currentText = s;
            //var strikethrough = "";
            //foreach (char c in s)
            //{
            //    strikethrough = strikethrough + c + '\u0336';
            //}
            //text.text = strikethrough;
        }
        //else
        //{
        //    text.color = Color.black;
        //}

        currentCell.GetComponentInChildren<Text>().text = currentText;
        title.text = info.serialQuestName;
    }
}
