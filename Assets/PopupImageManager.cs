using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupImageManager : Singleton<PopupImageManager>
{
    Animation animation;
    public Text title;
    public Image image;
    GameObject animObject;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponentInChildren<Animation>(true);
        animObject = animation.gameObject;
    }

    public void showAnim(string name)
    {
        animObject.SetActive(true);
        animation.Play();
        var info = ItemManager.Instance.getInfo(name);
        title.text = info.title;
        image.sprite = info.image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
