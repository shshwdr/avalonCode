using DG.Tweening;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGate : MonoBehaviour,IStatable
{
    public Sprite openGateSprite;
    public SpriteRenderer renderer;
    BoxCollider collider;
    public GameObject leftWing;
    public GameObject rightWing;
    public float openTime = 2;
    public float openDegree = 45;
    public void stateChange()
    {
        if(GetComponent<StateItem>().state == 1)
        {
            action();
        }
    }

    public void action()
    {

        Destroy(GetComponentInChildren<Talkable>().gameObject);
        //renderer.sprite = openGateSprite;
        //collider.enabled = false;
        leftWing.transform.DORotate(new Vector3(0, openDegree, 0), openTime);
        rightWing.transform.DORotate(new Vector3(0, -openDegree, 0), openTime);
        ItemInventoryManager.Instance.removeTokenableItem("key");
        QuestManager.Instance.finishQuest("openGate");
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
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    action();
        //}
    }
}
