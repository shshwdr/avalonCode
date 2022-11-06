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

    List<GameObject> tetrisCells = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
        EventPool.OptIn("selectInteractiveItem", selectInteractiveItem);
        emptyGridCells = GetComponentsInChildren<TokenGridCellEmpty>(true);

        int k  = 0;
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                emptyGridCellDict[new Vector2Int(i, j)] = emptyGridCells[k];
                k++;
            }
        }
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(0.1f);
        codeBookOB.SetActive(false);
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

            //show current tokens
            foreach(var token in selectItem.tokens)
            {
                var go = Instantiate(tokenTetrisCellPrefab, transform);
                go.GetComponent<TokenTetrisCell>().init(token);
                var position = emptyGridCellDict[token.index].GetComponent<RectTransform>().position;
                go.GetComponent<RectTransform>().position = position;
                tetrisCells.Add(go);
            }
        } 
        else
        {

            codeBookOB.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
