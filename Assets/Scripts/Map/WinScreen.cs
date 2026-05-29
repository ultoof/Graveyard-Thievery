using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    // Properties 
    public Sprite[] cutsceneSprites;
    public String[] cutsceneDialouge;
    public AudioSource[] voiceLines;
    public TextMeshProUGUI cutSceneText;
    public TextMeshProUGUI buttonText;
    public Canvas canvas;
    public Image image;
    public EventSystem eventSystem;
    public GameObject button;

    private int currentScene = 0;

    public void StartCutscene()
    {
        canvas.enabled = false;
        cutSceneText.text = cutsceneDialouge[0];
        image.sprite = cutsceneSprites[0];
        voiceLines[0].Play();
        eventSystem.SetSelectedGameObject(button);
    }

    public void AdvanceCutscene() // Plays the cutscene
    { 
        currentScene++;

        if (currentScene <= 4)
        {
            cutSceneText.text = cutsceneDialouge[currentScene];
            image.sprite = cutsceneSprites[currentScene];

            if (voiceLines[currentScene] != null)
            {
                voiceLines[currentScene].Play();
            }

            if (currentScene >= 4)
            {
                image.enabled = false;
                buttonText.text = "Main Menu";
            }
        }
        else
        {
            DataManager.instance.money = 0;
            DataManager.instance.totalMoney = 0;
            DataManager.instance.maxMoney = 100;
            DataManager.instance.difficulty = 0;
            DataManager.instance.health = 3;
            DataManager.instance.canFlash = false;
            DataManager.instance.canStun = false;
            DataManager.instance.soul = false;
            DataManager.instance.tutorial = false;
            SceneManager.LoadSceneAsync("MenuScene");
        }
    }
}