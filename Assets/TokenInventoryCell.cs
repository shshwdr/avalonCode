using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TokenInventoryCell : MonoBehaviour
{
    public string name;
    public Image renderer;
    bool isInventory = false;

    Button button;
    public Text titleLabel;

    public GameObject fixedFrame;

    private void Start()
    {
        button = GetComponentInChildren<Button>();
        if (!button)
        {
            return;
        }
        button.onClick.AddListener(delegate
        {
            if (MouseInputManager.Instance.selectedItem)
            {
                var tempName = name;
                if (isInventory)
                {
                    TokenInventoryManager.Instance.removeToken(tempName);
                    MouseInputManager.Instance.selectedItem.addToken(tempName);
                }
                else
                {
                    TokenInventoryManager.Instance.addToken(tempName);
                    MouseInputManager.Instance.selectedItem.removeToken(tempName);
                }

            }
        });

    }

    public void init(string n, bool inv)
    {
        button = GetComponentInChildren<Button>();
        //button = GetComponentInChildren<Button>();
        //button.onClick.RemoveAllListeners();
        if (n == "")
        {
            renderer.sprite = null;
            titleLabel.text = "";
            if (button)
            {

                button.interactable = false;
            }
            return;
        }
        if (button)
        {
            button.interactable = true;
        }
        name = n;
        var info = TokenManager.Instance.getInfo(name);
        isInventory = inv;
        renderer.sprite = info.image;
        titleLabel.text = info.title;
        if (info.state == "fixed")
        {
            fixedFrame.SetActive(true);
            if (button)
            {
                button.interactable = false;
            }

        }
        else
        {
            fixedFrame.SetActive(false);
        }

    }

    public GameObject highLight;
    float moveUpLength = 90;
    float moveTime = 0.3f;
    float originalY = 0;
    public void onHover()
    {
        //if (originalY == -1000)
        //{
        //    originalY = transform.position.y;
        //}
        highLight.SetActive(true);
        transform.DOLocalMoveY(originalY+moveUpLength, moveTime);
    }
    public void onExit()
    {

        highLight.SetActive(false);
        transform.DOLocalMoveY(originalY, moveTime);
    }
    //UnityEditor.TransformWorldPlacementJSON:{"position":{"x":867.75,"y":0.0,"z":0.0},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}}
    //UnityEditor.TransformWorldPlacementJSON:{"position":{"x":-179.25,"y":-90.0,"z":0.0},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}}
}
