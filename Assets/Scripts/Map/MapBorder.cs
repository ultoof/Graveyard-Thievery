using TMPro;
using UnityEngine;

public class MapBorder : MonoBehaviour {
    public TextMeshProUGUI textObject;

    void OnCollisionStay2D(Collision2D collision)
    {
        textObject.text = "You can't leave, you have to save your parents!";
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        textObject.text = "You can't leave, you have to save your parents!";
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        textObject.text = "";
    }
}