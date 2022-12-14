using DG.Tweening;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveItem : MonoBehaviour
{
    public GameObject pickUI;
    public Text interactiveText;
    public Text interactiveSubText;
    public bool isInteractiveDisabled;
    PlayerPickup playerPickup;
    protected SpriteRenderer renderer;
    public GameObject compressObject;
    //public GameObject pickingUpBar;
    // Start is called before the first frame update
    public virtual void Start()
    {
        hidePickupUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (!canShowInteractUI())
        {
            return;
        }
        if (isInteractiveDisabled)
        {
            return;
        }
        var player = collision.GetComponent<PlayerPickup>();

        if (player)
        {
            playerPickup = player;
            player.addCanPickup(this);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        var player = collision.GetComponent<PlayerPickup>();
        if (player)
        {
            player.removeCanPickup(this);
        }
    }

    private void OnDestroy()
    {

        GameObject.FindObjectOfType<PlayerPickup>().removeCanPickup(this);
    }

    protected virtual bool canInteract()
    {
        return true;
    }

    protected virtual bool canShowInteractUI()
    {
        return true;
    }

    public virtual void interact(PlayerPickup player)
    {

        if (!canInteract())
        {
            player.failedPickup();
            DialogueManager.ShowAlert("Need extra equipment!");
            return;
        }
        if (isInteractiveDisabled)
        {
            return;
        }
        pickUI.SetActive(false);

        TutorialText.Instance.triggerSpace();
    }
    public virtual void prepareUI() { }
    public virtual void showPickupUI()
    {
        if (!canShowInteractUI())
        {
            return;
        }
        if (isInteractiveDisabled)
        {
            return;
        }
        prepareUI();
        //show pick up
        pickUI.SetActive(true);

        if (!compressObject)
        {
            //Debug.LogError("cant hide " + transform.parent);
            return;
        }
        compressObject.SetActive(false);

        TutorialText.Instance.showSpaceText();
    }
    public virtual void hidePickupUI()
    {

        //pickingUpBar.SetActive(false);
        //show pick up
        pickUI.SetActive(false);

        if (!compressObject)
        {
            //Debug.LogError("cant hide "+transform.parent);
            return;
        }
        compressObject.SetActive(true);
        TutorialText.Instance.hideSpaceText();
    }

    public void disableInteractive()
    {
        isInteractiveDisabled = true;
        if (playerPickup)
        {
            playerPickup.removeCanPickup(this);
        }
        hidePickupUI();
    }

    public void enableInteractive()
    {
        isInteractiveDisabled = false;
    }
}
