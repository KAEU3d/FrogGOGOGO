using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Text scoreText;
    public GameObject GameOverPanel;
    public GameObject leaderBoard;
    private void OnEnable()
    {
        Time.timeScale = 1;
        EventHandler.GetPointEvent += OnGetPointEvent;
        EventHandler.GameOverEvent += OnGameOverEvent;
    }

    private void OnGameOverEvent()
    {
        Debug.Log("Game Over Event!");
        GameOverPanel.SetActive(true);
        if (GameOverPanel.activeInHierarchy)
        {
            Time.timeScale = 0;
        }
    }

    private void OnGetPointEvent(int point)
    {
        scoreText.text = point.ToString();
    }

    private void OnDisable()
    {
        EventHandler.GetPointEvent -= OnGetPointEvent;
        EventHandler.GameOverEvent -= OnGameOverEvent;
    }


    public void RestartGame()
    {
        GameOverPanel.SetActive(false);
        TransitionManager.instance.Transition("gamePlay");
    }

    public void BackToMenu()
    {
        GameOverPanel.SetActive(false);
        TransitionManager.instance.Transition("title");
    }

    public void onLeaderBoardOpen()
    {
        leaderBoard.SetActive(true);
    }
}
