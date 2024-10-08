using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject towerPrefab;

    public BulletManager bulletManager;

    private EnemiesManager _enemiesManager;
    private GameUiManager _gameUiManager;
    private PersonsManager _personsManager;

    private bool _gameIsStarted = false;


    void Awake()
    {
        _enemiesManager = FindObjectOfType<EnemiesManager>();
        _gameUiManager = FindObjectOfType<GameUiManager>();
        _personsManager = FindObjectOfType<PersonsManager>();
    }

    void Start()
    {
        // temp for fast run
        OnStartButtonHandler();
    }


    void OnEnable()
    {
        PersonsManager.OnEnemyCollideWithPerson += OnEnemyCollideWithPersonHandler;

        GameUiManager.OnStartButtonClick += OnStartButtonHandler;
        GameUiManager.OnRestartButtonClick += OnRestartButtonHandler;

        Ground.OnClickByGround += OnClickByGroundHandler;
    }

    void OnDisable()
    {
        PersonsManager.OnEnemyCollideWithPerson -= OnEnemyCollideWithPersonHandler;

        GameUiManager.OnStartButtonClick -= OnStartButtonHandler;
        GameUiManager.OnRestartButtonClick -= OnRestartButtonHandler;

        Ground.OnClickByGround -= OnClickByGroundHandler;
    }

    private void OnClickByGroundHandler(Vector2 mousePosition)
    {
        if (!_gameIsStarted)
        {
            return;
        }

        bool isPersonSelected = _personsManager.GetIsPersonSelected();

        if (isPersonSelected)
        {
            _personsManager.MoveSelectedPersonTo(mousePosition);
            return;
        }

        GameObject tower = Instantiate(towerPrefab, mousePosition, Quaternion.identity);

        Shooter shooter = tower.AddComponent<Shooter>();
        shooter.enemiesManager = _enemiesManager;
        shooter.bulletManager = bulletManager;
    }

    private void OnEnemyCollideWithPersonHandler(Person person)
    {
        bool isGameOver = CheckIsGameOver();

        if (isGameOver)
        {
            GameOver();
        }
    }

    private bool CheckIsGameOver()
    {
        bool isEmptyPersons = _personsManager.GetPersons().Count == 0;

        return isEmptyPersons;
    }

    private void GameOver()
    {
        _gameUiManager.ShowGameOverPanel();
        _enemiesManager.StopMoving();

        _gameIsStarted = false;
    }

    private void OnStartButtonHandler()
    {
        _gameUiManager.HideStartGamePanel();
        _enemiesManager.StartGenerate();

        _gameIsStarted = true;
    }

    private void OnRestartButtonHandler()
    {
        _gameUiManager.HideGameOverPanel();
        _enemiesManager.RemoveAllEnemies();
        _enemiesManager.StartGenerate();

        _gameIsStarted = true;
    }

}
