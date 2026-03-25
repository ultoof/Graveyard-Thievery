using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
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
        flashlight.canFlash = false;
        taser.canStun = false;
        canvas.enabled = false;
    }

    public void AdvanceCutscene()
    {
        currentScene++;

        if (currentScene <= 13)
        {
            image.sprite = cutsceneSprites[currentScene];
        }
        else
        {
            canvas.enabled = true;
            gameObject.SetActive(false);
        }
    }

    public void AdvanceText()
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
            SceneManager.LoadSceneAsync("MenuScene");
        }
    }
}