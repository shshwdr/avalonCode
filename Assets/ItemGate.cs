using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGate : MonoBehaviour,IStatable
{
    public Sprite openGateSprite;
    public SpriteRenderer renderer;
    BoxCollider collider;
    public void stateChange()
    {
        if(GetComponent<StateItem>().state == 1)
        {
            Destroy( GetComponentInChildren<Talkable>().gameObject);
            renderer.sprite = openGateSprite;
            collider.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
