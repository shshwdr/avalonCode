using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeBookMenu : MonoBehaviour
{
    public GameObject codeBookOB;

    public Text title;
    // Start is called before the first frame update
    void Start()
    {
        codeBookOB.SetActive(false);
        EventPool.OptIn("selectInteractiveItem", selectInteractiveItem);
    }

    void selectInteractiveItem()
    {
        if (MouseInputManager.Instance.selectedItem)
        {

            codeBookOB.SetActive(true);

            var name = MouseInputManager.Instance.selectedItem.name;

            title.text = MouseInputManager.Instance.selectedItem.fullTitle();

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
