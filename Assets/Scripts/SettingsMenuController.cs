using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public UnityEngine.UI.Button confirmButton;
    public TMPro.TextMeshProUGUI rowText;
    public TMPro.TextMeshProUGUI colText;

    public UnityEngine.UI.Slider rowSlider;
    public UnityEngine.UI.Slider colSlider;

    private String rowString;
    private String colString;
    
    private void Awake() {
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        rowString = rowText.text;
        colString = colText.text;    
    }

    private void Update() {
        rowText.text = rowString + " " + Mathf.RoundToInt(rowSlider.value).ToString();
        colText.text = colString + " " + Mathf.RoundToInt(colSlider.value).ToString();    
    }

    private void OnDestroy() {
        confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
    }

    private void OnConfirmButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
