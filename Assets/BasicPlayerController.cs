using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicPlayerController : MonoBehaviour
{
    public float speed;
    public Sprite left;
    public Sprite right;
    public Sprite up;
    public Sprite down;
    public SpriteRenderer renderer;
    CharacterController controller;
    Rigidbody rb;
    Animator anim;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogueUtils.Instance.isInDialogue)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward*z;
        rb.AddForce(move * speed*Time.deltaTime);
        var vel = new Vector3(x,0,z);
        if (Mathf.Abs(vel.x)> Mathf.Abs(vel.z))
        {
            if (vel.x > 0.01f)
            {
                renderer.sprite = right;
            }else if (vel.x < -0.01f)
            {
                renderer.sprite = left;
            }
        }
        else
        {
            if (vel.z > 0.01f)
            {
                renderer.sprite = up;
            }
            else if (vel.z < -0.01f)
            {
                renderer.sprite = down;
            }

        }

        if (vel.magnitude > 0.01f)
        {
            // renderer.transform.DORotate(new Vector3()
            anim.SetBool("walking",true);
        }
        else
        {

            anim.SetBool("walking", false);
        }

        //controller.Move(speed * Time.deltaTime * move);
    }
}