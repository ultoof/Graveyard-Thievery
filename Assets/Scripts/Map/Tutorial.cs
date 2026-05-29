using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Properties 
    public Sprite[] cutsceneSprites;
    public String[] tutorialText;
    public String[] cutsceneDialouge;
    public AudioSource[] voiceLines;
    public TextMeshProUGUI text;
    public TextMeshProUGUI cutSceneText;
    public Button button;
    public Canvas canvas;
    public Image image;
    public Flashlight flashlight;
    public Taser taser;
    public EventSystem eventSystem;
    public GameObject secondButton;

    private int currentScene = 0;
    private int textScene = 0;
    private bool hasFlashed = false;
    private bool hasTazed = false;

    void Awake()
    {
        // Turns of uppgrades 
        DataManager.instance.canFlash = true;
        DataManager.instance.canStun = true;
        taser.AddIcon();
        flashlight.AddIcon();
        canvas.enabled = false;
    }

    private void Start() {
        voiceLines[0].Play();
        cutSceneText.text = cutsceneDialouge[0];
    }

    public void AdvanceCutscene() // Plays the cutscene
    { 
        currentScene++;

        if (currentScene <= 12) voiceLines[currentScene+1].Play();

        if (currentScene <= 13)
        {
            image.sprite = cutsceneSprites[currentScene]; // Plays thrue the cutscene
            cutSceneText.text = cutsceneDialouge[currentScene];
        }
        else
        {
            eventSystem.SetSelectedGameObject(secondButton);
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