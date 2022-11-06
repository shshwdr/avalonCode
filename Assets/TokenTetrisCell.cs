using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TokenTetrisCell : EventTrigger
{
    public GameObject tokenCellPrefab;
    bool dragging;
    CodeBookMenu menu;
    GameObject closestSlot;
    Token token;
    public void init(Token _token, CodeBookMenu _menu)
    {
        token = _token;
        var go = Instantiate(tokenCellPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TokenCell>().image .sprite = token.info.image;
        menu = _menu;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Update()
    {
        if (dragging)
        {
            var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            closestSlot =  menu.findClosestSlot(pos, this);
            transform.position = closestSlot.transform.position;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
        //update token position info
        if (closestSlot.GetComponent<TokenGridCellEmpty>())
        {

            token.updateGridPosition(closestSlot.GetComponent<TokenGridCellEmpty>().index, MouseInputManager.Instance.selectedItem);
        }else if (closestSlot.GetComponent<TokenInventoryCellEmpty>())
        {
            token.updateInventoryPosition(closestSlot.GetComponent<TokenInventoryCellEmpty>().index);
        }

    }
}
