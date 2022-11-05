using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeBookMenu : MonoBehaviour
{
    public GameObject codeBookOB;
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
