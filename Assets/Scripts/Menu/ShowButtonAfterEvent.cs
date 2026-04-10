using UnityEngine;
using UnityEngine.UI;

public class ShowButtonAfterEvent : MonoBehaviour
{
    public GameObject ExitCutsceneButton;
    
    public void MyEventFunction()
    {
        ExitCutsceneButton.SetActive(true);
    } 
}
