using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ESC : UiPanel<ESC> {


    public Button restart;
    public Button quit;

    private void OnEnable()
    {
        restart.onClick.AddListener(RestartGame);
        quit.onClick.AddListener(QuitGame);
    }
    private void OnDisable()
    {
        restart.onClick.RemoveListener(RestartGame);
        quit.onClick.RemoveListener(QuitGame);
    }
    void RestartGame()
    {
        Time.timeScale = 1;
        Hide();
        GameController.Instance.LoadScene("MainMenue");

    }
    void QuitGame()
    {
        Application.Quit();
    }
}
