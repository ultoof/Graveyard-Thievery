using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Properties 
    public Sprite[] cutsceneSprites;
    public String[] tutorialText;
    public TextMeshProUGUI text;
    public Button button;
    public Canvas canvas;
    public Image image;
    public Flashlight flashlight;
    public Taser taser;

    private int currentScene = 0;
    private int textScene = 0;
    private bool hasFlashed = false;
    private bool hasTazed = false;

    void Awake()
    {
        // Turns of uppgrades 
        DataManager.instance.canFlash = true;
        DataManager.instance.canStun = true;
        canvas.enabled = false;
    }

    public void AdvanceCutscene() // Plays the cutscene
    {
        currentScene++;

        if (currentScene <= 13)
        {
            image.sprite = cutsceneSprites[currentScene]; // Plays thrue the cutscene
        }
        else
        {
            canvas.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void AdvanceText() // Text for the tutorial
    {
        if (textScene == 3 && !flashlight.hasBeenUsed) return;  
        if (textScene == 8 && !taser.hasBeenUsed) return;
        if (textScene == 4 && DataManager.instance.money <= 0) return;

        textScene++;
        if (textScene <= 11)
        {
            text.text = tutorialText[textScene];
        }
        else
        {
            text.enabled = false;
            button.enabled = false;
            DataManager.instance.tutorial = true;
            DataManager.instance.canFlash = false;
            DataManager.instance.canStun = false;
            DataManager.instance.money = 0;
            SceneManager.LoadSceneAsync("MenuScene"); // Sends you back to hte menu when you are done with the tutorial.
        }
    }
}