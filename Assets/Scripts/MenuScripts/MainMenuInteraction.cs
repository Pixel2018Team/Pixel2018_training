using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInteraction : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject PlayerConfirmCanvas;
    public GameObject PlayButton;
    public GameObject ScoreButton;
    public GameObject QuitButton;

    // Use this for initialization
    void Start()
    {

        if (PlayButton != null)
        {
            PlayButton.GetComponent<Button>().onClick.AddListener(ClickPlayCallback);
        }

        if (QuitButton != null)
        {
            PlayButton.GetComponent<Button>().onClick.AddListener(ClickQuitCallback);
        }

        if (ScoreButton != null)
        {
            PlayButton.GetComponent<Button>().onClick.AddListener(ClickScoresCallback);
        }
    }


    public void ClickPlayCallback()
    {
        MenuCanvas.SetActive(false);
        PlayerConfirmCanvas.SetActive(true);
    }

    public void ClickQuitCallback()
    {
        Application.Quit();
    }

    public void ClickScoresCallback()
    {
        //Open scores scene
    }

    // Update is called once per frame
    void Update()
    {

    }
}
