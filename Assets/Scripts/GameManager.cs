using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject TitleBG_Img;
    public GameObject TitleStart_Bttn;
    public GameObject Title_Txt;

    public GameObject Camera;
    public GameObject Player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStartMenuElems();
        SetCamera();
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
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
