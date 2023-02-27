using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    [SerializeField] Canvas Options;
    [SerializeField] Canvas Home;
    [SerializeField] GameObject afterPanel;
    [SerializeField] GameObject beforePanel;
    [SerializeField] GameObject gameover;
    [SerializeField]EnemyFollow enemy;

    private void Start()
    {
        //gameover = GameObject.Find("PlayUIPanel");
        //enemy = GetComponent<EnemyFollow>();
    }
    public void NextScence()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void OptionsClick()
    {
        Options.gameObject.SetActive(true);
        Home.gameObject.SetActive(false);
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        afterPanel.SetActive(true);
        beforePanel.SetActive(false);
        gameover.SetActive(false);
    }

    public void Back()
    {
        Time.timeScale = 1;
        afterPanel.SetActive(false);
        beforePanel.SetActive(true);
        if (enemy.gameover)
        {
            gameover.SetActive(true);
        }

    }
}
