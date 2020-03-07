using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scr : MonoBehaviour
{
    public Transform tf;
    public float movementSpeed = 4.0f;
    public float rotationSpeed = 90.0f;


    // Start is called before the first frame update
    void Start()
    {
        //Setting Transform
        tf = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //All inputs in one function
        InputHandler();
    }

    private void InputHandler()
    {
        //Movement
        if (Input.GetKey(KeyCode.UpArrow))
        {
            tf.position += tf.up * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            tf.position -= tf.up * movementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            tf.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            tf.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        //Reset position
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            this.tf.position = new Vector3(0, 0, -1);
        }
    }
}
