using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTitleBehavior: MonoBehaviour
{
    public string titleName;

    public virtual void changeTitleTo(string str)
    {
        if(str == "")
        {
            return;
        }
        if(titleName .Contains( str))
        {
            changeTitleTo_internal(str);
        }
    }

    protected virtual void changeTitleTo_internal(string str)
    {

    }
    public virtual void changeTitleFrom(string str)
    {
        if (str == "")
        {
            return;
        }

        if (titleName .Contains( str))
        {
            changeTitleFrom_internal(str);
        }
    }

    protected virtual void changeTitleFrom_internal(string str)
    {

    }
}