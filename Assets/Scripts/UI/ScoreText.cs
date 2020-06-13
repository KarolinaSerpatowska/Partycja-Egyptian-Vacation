using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour
{
    public int counter;
    [SerializeField] Text text;
    public GameObject winPanel;

    // Start is called before the first frame update
    void Start()
    {
        counter = 5;
        text.text = "Enemies remain: " + counter.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter == 0)
        {
            StartCoroutine(Wait());
            //MainCanvas.canBePause = false;
            //winPanel.SetActive(true);
            //Time.timeScale = 0;
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        }
        if (counter < 0) counter = 0;
        text.text = "Enemies remain: " + counter.ToString();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        MainCanvas.canBePause = false;
        winPanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
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
