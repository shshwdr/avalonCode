using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateItem:MonoBehaviour
{

    public string name;
    public int state;

    public void setState(int s)
    {
        state = s;
        GetComponent<IStatable>().stateChange();
    }
}