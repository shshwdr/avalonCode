using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBridge : MonoBehaviour, IStatable
{
    public SpriteRenderer renderer;
    BoxCollider collider;

    public Sprite fix1;
    public Sprite fix2;

    public void stateChange()
    {
        if (GetComponent<StateItem>().state == 2)
        {
            action2();
        }
        //}else if (GetComponent<StateItem>().state == 1)
        //{
        //    action1();
        //}
    }

    //public void action1()
    //{

    //    renderer.sprite = fix1;
    //}
    public void action2()
    {

        //Destroy(GetComponentInChildren<Talkable>().gameObject);
        //renderer.sprite = fix2;
        collider.enabled = false;
        //ItemInventoryManager.Instance.removeTokenableItem("key");
        //QuestManager.Instance.finishQuest("openGate");
        this.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {

        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    action1();
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    action2();
        //}
    }
}
