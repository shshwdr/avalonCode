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

    RecipeCell[] recipeCells;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
        EventPool.OptIn("selectInteractiveItem", selectInteractiveItem);
        //EventPool.OptIn<string>("titleChange", titleChange);
        generateButton.interactable = false;

        emptyGridCells = GetComponentsInChildren<TokenInventoryCell>(true);
        selectInteractiveItem();

        recipeCells = GetComponentsInChildren<RecipeCell>(true);
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

    bool isOn;
    IEnumerator turnOnView()
    {
        yield return new WaitForSeconds(0.2f);
        isOn = true;
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
                emptyGridCells[i].gameObject.SetActive(true);
                emptyGridCells[i].init(selectItem.tokens[i], false);
            }
            for (; i < emptyGridCells.Length; i++)
            {
                emptyGridCells[i].gameObject.SetActive(false);
            }

            //show all posible results
            i = 0;
            if (ItemTokenCombination.Instance.getInfo(name)!=null)
            {
                foreach (var comb in ItemTokenCombination.Instance.getInfo(name))
                {
                    recipeCells[i].gameObject.SetActive(true);
                    recipeCells[i].init(comb);
                    i++;
                }
            }
            for(;i< recipeCells.Length; i++)
            {

                recipeCells[i].gameObject.SetActive(false);
            }

            StartCoroutine(turnOnView());
        }
        else
        {
            if (isOn)
            {
                codeBookOB.SetActive(false);

                isOn = false;
            }
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
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space)) && isOn)
        {
            codeBookOB.SetActive(false);
            isOn = false;
        }
    }
}
