using UnityEngine;
using UnityEngine.SceneManagement;

public class ManeController : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button playButton;

    [SerializeField] private UnityEngine.UI.Button settingsButton;
    [SerializeField] private UnityEngine.UI.Button HowToPlayButton;
    [SerializeField] private UnityEngine.UI.Button ExitButton;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private UnityEngine.UI.Slider rowSlider;
    [SerializeField] private UnityEngine.UI.Slider colSlider;
    [SerializeField] private UnityEngine.UI.Button ReturnButton;
    [SerializeField] private GameObject HowToPlayMenu;

    private int blocksInRow;
    private int blocksInCol;

    private int blocksInRowTopLimit;
    private int blocksInColTopLimit;
    private int blocksInRowBottomLimit;
    private int blocksInColBottomLimit;

    private void Awake() {
        playButton.onClick.AddListener(OnPlayButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        HowToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        ExitButton.onClick.AddListener(OnExitButtonClicked);
        ReturnButton.onClick.AddListener(OnReturnButtonClicked);
        calcBlockLimits();
        settingsMenu.SetActive(false);
        colSlider.minValue = blocksInColBottomLimit;
        colSlider.maxValue = blocksInColTopLimit;
        colSlider.value = blocksInCol;
        rowSlider.minValue = blocksInRowBottomLimit;
        rowSlider.maxValue = blocksInRowTopLimit;
        rowSlider.value = blocksInCol;
        HowToPlayMenu.SetActive(false);
    }

    private void OnPlayButtonClicked() {
        blocksInCol = Mathf.RoundToInt(colSlider.value);
        blocksInRow = Mathf.RoundToInt(rowSlider.value);
        SceneDto.instance.startGame(blocksInCol, blocksInRow);
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void OnHowToPlayButtonClicked() {
        HowToPlayMenu.SetActive(true);
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
        settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        HowToPlayButton.onClick.RemoveListener(OnHowToPlayButtonClicked);
        ExitButton.onClick.RemoveListener(OnExitButtonClicked);
        ReturnButton.onClick.RemoveListener(OnReturnButtonClicked);
    }

    private void OnReturnButtonClicked()
    {
        HowToPlayMenu.SetActive(false);
    }

    private void calcBlockLimits()
    {
        float dpi = Screen.dpi;
        // get width and height in centimeters
        float width = Screen.safeArea.width/dpi*2.54f;
        float height = Screen.safeArea.height/dpi*2.54f;
        // every block will be at minimum 0.5cm in width
        blocksInRowTopLimit = width<0.5? 1: (int)Mathf.Floor(width/0.5f);
        blocksInRowBottomLimit = 1;
        // every block will be at minimum 0.3cm in height
        blocksInColTopLimit = height<0.3? 1: (int)Mathf.Floor(height/0.3f);
        blocksInColBottomLimit = 1;
        blocksInCol = (blocksInColBottomLimit+blocksInColTopLimit)/2;
        blocksInRow = (blocksInRowBottomLimit+blocksInRowTopLimit)/2;
    }

    private void OnSettingsButtonClicked()
    {
        settingsMenu.SetActive(true);
    }
}
