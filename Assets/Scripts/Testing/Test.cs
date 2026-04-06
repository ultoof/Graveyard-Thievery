using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public GameObject ExitCutsceneButton;
    
    public void MyEventFunction()
    {
        ExitCutsceneButton.SetActive(true);
    } 
}
