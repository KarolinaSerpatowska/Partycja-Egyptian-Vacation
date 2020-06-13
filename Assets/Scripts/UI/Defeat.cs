using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Defeat : MonoBehaviour
{
    public GameObject player;
    Stats playerStats;
    public GameObject defeatPanel;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = player.GetComponent<Player>().getStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.health <= 0)
        {
            MainCanvas.canBePause = false;

            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        defeatPanel.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit_from_game()
    {
        Application.Quit();
    }
}
