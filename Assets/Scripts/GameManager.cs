using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Canvas Elems
    public GameObject TitleBG_Img;
    public GameObject TitleStart_Bttn;
    public GameObject Title_Txt;
    // ^^These^^
    public GameObject SMECanvas;

    //Life counter
    public GameObject Lives_Txt;

    //Main Camera
    public GameObject Camera;

    //Player
    public GameObject Player;

    //Player PreFab
    public GameObject PlayerPreFab;

    //FSM State
    public string GameState = "Menu";

    //Lives Value
    public int Lives = 3;

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

    //Inspector insurance
    private void SetCamera()
    {
        if (Camera == null)
        {
            Camera = GameObject.Find("Main Camera");
        }
    }

    //Turns on menu elems
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

        if (GameState == "Menu")
        {
            Menu();

            if (Input.GetKeyDown(KeyCode.P))
            {
                ChangeGameState("Play");
            }
        }

        else if (GameState == "Play")
        {
            Play();

            if (Input.GetKeyDown(KeyCode.M))
            {
                ChangeGameState("Menu");
            }
        }

        //Writes in the Lives counter
        Lives_Txt.GetComponent<Text>().text = "Extra Lives: " + Lives;

        if (Player == null)
        {
            if (Lives != 0)
            {
                Instantiate(PlayerPreFab);
                Lives--;
                Player = GameObject.Find("Player_Obj(Clone)");
            }

            else if (Lives == 0)
            {
                Instantiate(PlayerPreFab);
                Player = GameObject.Find("Player_Obj(Clone)");
                TitleStart_Bttn.SetActive(false);
            }

            ChangeGameState("Menu");
        }
    }
    
    private void Play()
    {
        SMECanvas.SetActive(false);

        if (Player != null)
        {
            Player.GetComponent<Player_Scr>().enabled = true;
        }
    }

    private void Menu()
    {
        SMECanvas.SetActive(true);

        if (Player != null)
        {
            Player.GetComponent<Player_Scr>().enabled = false;
        }
    }

    public void ChangeGameState(string newState)
    {
        GameState = newState;
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
