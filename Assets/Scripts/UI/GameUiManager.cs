using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameUiManager : MonoBehaviour
{
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] GameObject _startGamePanel;

    public static event Action OnStartButtonClick;
    public static event Action OnRestartButtonClick;

    public void ShowGameOverPanel()
    {
        _gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        _gameOverPanel.SetActive(false);
    }

    public void HideStartGamePanel()
    {
        _startGamePanel.SetActive(false);
    }

    public void OnStartButtonClickHandler()
    {
        OnStartButtonClick?.Invoke();
    }

    public void OnRestartButtonClickHandler()
    {
        OnRestartButtonClick?.Invoke();
    }
}
