using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManeController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button playButton;
    [SerializeField] private UnityEngine.UI.Button HowToPlayButton;
    [SerializeField] private UnityEngine.UI.Button ExitButton;

    private void Awake() {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        HowToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        ExitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnPlayButtonClicked() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void OnHowToPlayButtonClicked() {
        
    }

    private void OnExitButtonClicked() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void OnDestroy() {
        playButton.onClick.RemoveListener(OnPlayButtonClicked);
        HowToPlayButton.onClick.RemoveListener(OnHowToPlayButtonClicked);
        ExitButton.onClick.RemoveListener(OnExitButtonClicked);
    }
}
