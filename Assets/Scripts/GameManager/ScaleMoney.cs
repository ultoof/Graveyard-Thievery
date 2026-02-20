using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMoney : MonoBehaviour
{
    public TextMeshProUGUI jordanDisplay;
        void Update()
        {
            if(jordanDisplay != null)
            jordanDisplay.text = $"$: {DataManager.instance.money}";
        }
}
