using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] GameObject _spawnerTop;

    // public Enemy enemy;

    private int _offset = 1;
    private int _numberOfObjects = 1;
    private bool _stopMoving = false;

    private Vector2 _startPosition = new Vector2(-5, 0);

    private List<Enemy> enemies = new List<Enemy>();

    public static event Action<Collider2D, Enemy> OnEnemyCollision;


    void OnEnable()
    {
        Enemy.OnCollision += OnEnemyCollisionHandler;
    }

    void OnDisable()
    {
        Enemy.OnCollision -= OnEnemyCollisionHandler;
    }

    public void StartGenerate()
    {
        _stopMoving = false;

        for (int i = 0; i < _numberOfObjects; i++)
        {
            Vector2 spawnPosition = (Vector2)_spawnerTop.gameObject.transform.position + _startPosition + new Vector2(_offset * i, 0);

            Enemy enemy = Enemy.Create(_enemyPrefab, spawnPosition);

            enemy.CalcFallSpeed(i);

            enemies.Add(enemy);
        }
    }


    void FixedUpdate()
    {
        foreach (Enemy enemy in enemies)
        {
            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            float fallSpeed = enemy.GetFallSpeed();

            // todo перенести в сам класс enemy
            rb.velocity = new Vector2(0, _stopMoving ? 0 : -fallSpeed);

            // или через MovePosition, но не будет velocity у объекта
            // Vector2 velocity = Vector2.down * _fallSpeed;
            // rb.velocity = velocity * Time.fixedDeltaTime;

            // rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime)
        }

    }


    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
        enemies.Remove(enemy);
    }

    public void RemoveAllEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        enemies = new List<Enemy>();
    }

    public void StopMoving()
    {
        _stopMoving = true;
    }

    public void OnEnemyCollisionHandler(Collider2D collision, Enemy enemy)
    {
        OnEnemyCollision?.Invoke(collision, enemy);
    }
}
