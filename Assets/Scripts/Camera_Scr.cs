﻿using System;
using System.Collections;
using System.Numerics;
using UnityEngine;

public class Camera_Scr : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player_Obj");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FallowPlayer();
    }

    private void FallowPlayer()
    {
        if (Player.transform.position.x > -16 && Player.transform.position.x < 16)
        {
            this.gameObject.transform.position = new UnityEngine.Vector3(Player.transform.position.x, this.transform.position.y, -10);
        }
        if (Player.transform.position.y > -16.5 && Player.transform.position.y < 16.5)
        {
            this.gameObject.transform.position = new UnityEngine.Vector3(this.transform.position.x, Player.transform.position.y, -10);
        }
    }
}