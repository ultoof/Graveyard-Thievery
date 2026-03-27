using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMoney : MonoBehaviour
{
    public TextMeshProUGUI jordanDisplay; // Gets the display from the unity project
        void Update()
        {
            if(jordanDisplay != null)
            jordanDisplay.text = $"$: {DataManager.instance.money}"; // Shows the money.
        }
}
