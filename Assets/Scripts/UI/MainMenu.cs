using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject startButton;
    public GameObject exitButton;
    public GameObject creditsButton;
    public GameObject creditsPanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
    }

    public void Exit_from_game()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void credtis()
    {
        startButton.SetActive(false);
        exitButton.SetActive(false);
        creditsButton.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void buttonReturn()
    {
        startButton.SetActive(true);
        exitButton.SetActive(true);
        creditsButton.SetActive(true);
        creditsPanel.SetActive(false);
    }
}
