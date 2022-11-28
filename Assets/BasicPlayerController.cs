using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BasicPlayerController : MonoBehaviour
{
    public float speed;
    CharacterController controller;
    Rigidbody rb;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
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

        //controller.Move(speed * Time.deltaTime * move);
    }
}