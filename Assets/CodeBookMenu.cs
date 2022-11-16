using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBookMenu : MonoBehaviour
{
    public GameObject codeBookOB;
    public GameObject tokenTetrisCellPrefab;

    public Text title;

    TokenInventoryCell[] emptyGridCells;
    public Button generateButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
        EventPool.OptIn("selectInteractiveItem", selectInteractiveItem);
        //EventPool.OptIn<string>("titleChange", titleChange);
        generateButton.interactable = false;

        emptyGridCells = GetComponentsInChildren<TokenInventoryCell>(true);
        selectInteractiveItem();
    }
    //void titleChange(string t)
    //{
    //    title.text = t;
    //}
    IEnumerator test()
    {
        yield return new WaitForSeconds(0.1f);
        codeBookOB.SetActive(false);
    }


    public void generate()
    {
        MouseInputManager.Instance.selectedItem.generate();
    }


    void selectInteractiveItem()
    {

        if (MouseInputManager.Instance.selectedItem)
        {

            codeBookOB.SetActive(true);

            var selectItem = MouseInputManager.Instance.selectedItem;
            var name = selectItem.name;

            title.text = selectItem.fullTitle();

            //if (selectItem.canGeneration())
            {
                generateButton.interactable = selectItem.canGeneration();
            }


            int i = 0;
            for (; i < selectItem.tokens.Count; i++)
            {
                emptyGridCells[i].init(selectItem.tokens[i],false);
            }
            for (; i < emptyGridCells.Length; i++)
            {
                emptyGridCells[i].init("", false);
            }

        } 
        else
        {

            codeBookOB.SetActive(false);
        }

    }


    //public GameObject findClosestSlot(Vector3 pos, TokenTetrisCell cell)
    //{
    //    float closestDistance = 10000;
    //    GameObject res = null;
    //    foreach(var s in emptyGridCells)
    //    {
    //        var dis = Vector2.Distance(pos, (Vector2)s.transform.position);
    //        if (dis <= closestDistance){
    //            res = s.gameObject;
    //            closestDistance = dis;
    //        }
    //    }

    //    foreach(var s in tokenInventoryMenu.emptyInventoryCells)
    //    {
    //        var dis = Vector2.Distance(pos, (Vector2)s.transform.position);
    //        if (dis <= closestDistance)
    //        {
    //            res = s.gameObject;
    //            closestDistance = dis;
    //        }
    //    }

    //    return res;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
