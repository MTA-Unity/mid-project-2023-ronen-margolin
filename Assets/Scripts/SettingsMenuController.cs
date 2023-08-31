using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public UnityEngine.UI.Button confirmButton;
    
    private void Awake() {
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);    
    }

    private void OnDestroy() {
        confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
    }

    private void OnConfirmButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
