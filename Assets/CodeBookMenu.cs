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

    public Dictionary<Vector2Int, TokenGridCellEmpty> emptyGridCellDict = new Dictionary<Vector2Int, TokenGridCellEmpty>();
    TokenGridCellEmpty[] emptyGridCells;
    public Button generateButton;

    TokenInventoryCellEmpty[] emptyInventoryCells;

    List<GameObject> tetrisCells = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
        EventPool.OptIn("selectInteractiveItem", selectInteractiveItem);
        //EventPool.OptIn<string>("titleChange", titleChange);
        EventPool.OptIn("updateTokenInventory", inventoryChange);
        emptyGridCells = GetComponentsInChildren<TokenGridCellEmpty>(true);
        generateButton.interactable = false;
        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var index = new Vector2Int(i, j);
                emptyGridCellDict[index] = emptyGridCells[k];
                emptyGridCells[k].index = index;
                k++;
            }
        }

        emptyInventoryCells = GetComponentsInChildren<TokenInventoryCellEmpty>(true);
        for (int i = 0; i < emptyInventoryCells.Length; i++)
        {
            emptyInventoryCells[i].index = i;
        }


    }
    //void titleChange(string t)
    //{
    //    title.text = t;
    //}
    void inventoryChange()
    {
        for(int i = 0; i < TokenInventoryManager.Instance.tokens.Length; i++)
        {
            if (TokenInventoryManager.Instance.tokens[i] != null)
            {
                createToken(TokenInventoryManager.Instance.tokens[i]);
            }
        }
    }
    IEnumerator test()
    {
        yield return new WaitForSeconds(0.1f);
        codeBookOB.SetActive(false);
    }

    GameObject createToken(Token token)
    {
        var go = Instantiate(tokenTetrisCellPrefab, transform);
        go.GetComponent<TokenTetrisCell>().init(token, this);
        var position = Vector3.zero;
        if (token.isInventory)
        {
            position = emptyInventoryCells[token.indexInt].transform.position;
        }
        else
        {
            position = emptyGridCellDict[token.index].transform.position;
        }
        go.GetComponent<RectTransform>().position = position;
        tetrisCells.Add(go);
        return go;
    }

    public void generate()
    {
        MouseInputManager.Instance.selectedItem.generate();
    }


    void selectInteractiveItem()
    {
        foreach (var c in tetrisCells)
        {
            Destroy(c);
        }
        tetrisCells.Clear();


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

            //show current tokens
            foreach(var token in selectItem.tokens)
            {
                var go = createToken(token);
                //tetrisCells.Add(go);
            }
            inventoryChange();
        } 
        else
        {

            codeBookOB.SetActive(false);
        }

    }


    public GameObject findClosestSlot(Vector3 pos, TokenTetrisCell cell)
    {
        float closestDistance = 10000;
        GameObject res = null;
        foreach(var s in emptyGridCells)
        {
            var dis = Vector2.Distance(pos, (Vector2)s.transform.position);
            if (dis <= closestDistance){
                res = s.gameObject;
                closestDistance = dis;
            }
        }

        foreach(var s in emptyInventoryCells)
        {
            var dis = Vector2.Distance(pos, (Vector2)s.transform.position);
            if (dis <= closestDistance)
            {
                res = s.gameObject;
                closestDistance = dis;
            }
        }

        return res;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
