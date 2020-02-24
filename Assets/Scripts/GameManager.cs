using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject TitleBG_Img;
    public GameObject TitleStart_Bttn;
    public GameObject Title_Txt;

    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        SetStartMenuElems();
        SetCamera();
        DisableSME();
    }

    private void SetCamera()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera");
        }
    }

    private void DisableSME()
    {
        Title_Txt.SetActive(false);
        TitleBG_Img.SetActive(false);
        TitleStart_Bttn.SetActive(false);
    }

    private void SetStartMenuElems()
    {
        if (TitleBG_Img == null)
        {
            TitleBG_Img = GameObject.Find("TitleBG_Img");
        }
        if (TitleStart_Bttn == null)
        {
            TitleStart_Bttn = GameObject.Find("TitleStart_Bttn");
        }
        if (Title_Txt == null)
        {
            Title_Txt = GameObject.Find("Title_Txt");
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputHandler();
        //FollowCamera();
    }

    private void FollowCamera()
    {
        //if (Camera.transform.position.x > -16 && Camera.transform.position.x < 16)
        //{
        //TitleBG_Img.transform.position = new Vector3(Camera.transform.position.x, TitleBG_Img.transform.position.y, -10);
        //}
        //if (Camera.transform.position.y > -16.5 && Camera.transform.position.y < 16.5)
        //{
        //TitleBG_Img.transform.position = new Vector3(TitleBG_Img.transform.position.x, Camera.transform.position.y, -10);
        //}
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
