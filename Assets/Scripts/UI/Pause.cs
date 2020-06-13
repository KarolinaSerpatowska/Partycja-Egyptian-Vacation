using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject menuPanel;
    [SerializeField] bool isPause = false;

    // Update is called once per frame
    void Update()
    {
        if (MainCanvas.canBePause)
        {
            if ((Input.GetButtonDown("Options") || Input.GetKeyDown(KeyCode.Escape)) && !isPause)
            {
                menuPanel.SetActive(true);
                Time.timeScale = 0;
                isPause = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if ((Input.GetButtonDown("Options") || Input.GetKeyDown(KeyCode.Escape)) && isPause)
            {
                menuPanel.SetActive(false);
                Time.timeScale = 1;
                isPause = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void Continue()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Exit_from_game()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
