using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Button button;
    // Start is called before the first frame update
    private void Awake() {
        button.onClick.AddListener(OnButtonMainMenuClick);
    }

    private void OnButtonMainMenuClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void OnDestroy(){
        button.onClick.RemoveListener(OnButtonMainMenuClick);
    }
}
