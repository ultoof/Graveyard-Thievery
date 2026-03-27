using System;
using TMPro;
using UnityEngine;
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

    void Awake()
    {
        // Turns of uppgrades 
        flashlight.canFlash = true;
        taser.canStun = true;
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
            SceneManager.LoadSceneAsync("MenuScene"); // Sends you back to hte menu when you are done with the tutorial.
        }
    }
}