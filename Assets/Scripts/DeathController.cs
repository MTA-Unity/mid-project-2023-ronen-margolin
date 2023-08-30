using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Button button;
    [SerializeField] private GameObject text;

    private BallManager manager;
    // Start is called before the first frame update
    private void Awake() {
        button.onClick.AddListener(OnButtonMainMenuClick);
        manager = FindObjectOfType<BallManager>();
    }

    private void OnButtonMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void OnDestroy(){
        button.onClick.RemoveListener(OnButtonMainMenuClick);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu(bool lose)
    {
        gameObject.SetActive(true);
        manager.gameActive = false;
        manager.destroyBalls();
        manager.freezePuddle();
        if (lose)
        {
            text.GetComponent<TextMeshProUGUI>().text = "You Lost!";
        }
        else
        {
            text.GetComponent<TextMeshProUGUI>().text = "You Won!";
        }
    }
}
