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
        button.interactable = true;
        name = n;
        var info = TokenManager.Instance.getInfo(name);
        isInventory = inv;
        renderer.sprite = info.image;
        titleLabel.text = info.title;
        if (info.state == "fixed")
        {
            fixedFrame.SetActive(true);
            button.interactable = false;

        }
        else
        {
            fixedFrame.SetActive(false);
        }

    }
}
