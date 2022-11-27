using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenu : MonoBehaviour
{
    QuestCell[] cells;
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        cells = GetComponentsInChildren<QuestCell>();
        UpdateQuest();
        EventPool.OptIn("updateQuest", UpdateQuest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleMenu()
    {
        menu.SetActive( !menu.active);
    }

    public void UpdateQuest()
    {
        if (DialogueUtils.Instance.isInDialogue)
        {
            return;
        }
        int i = 0;
        foreach (var info in QuestManager.Instance.activeQuests())
        {
            if (i >= cells.Length)
            {
                Debug.LogError("out of index");
            }
            QuestCell cell = cells[i];
            cell.updateCell(info);
            cell.gameObject.SetActive(true);
            i++;
        }
        for (; i < cells.Length; i++)
        {

            cells[i].gameObject.SetActive(false);
        }
    }
}
