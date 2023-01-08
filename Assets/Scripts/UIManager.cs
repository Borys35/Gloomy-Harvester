using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject tooltip;
    public TextMeshProUGUI text;
    public TextMeshProUGUI key;

    // Start is called before the first frame update
    void Start()
    {
        tooltip.SetActive(false);
    }

    // Update is called once per frame
    public void ShowTooltip(string textContent = "", string keyContent = "") {
        text.SetText(textContent);
        key.SetText(keyContent);
        tooltip.SetActive(true);
    }

    public void HideTooltip() {
        tooltip.SetActive(false);
    }
}
