using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Shooter : MonoBehaviour
{
    public EnemiesManager enemiesManager;
    public BulletManager bulletManager;

    private float _fireRate = 2f;    // Скорострельность башни
    private float _fireCountdown = 0f;

    void OnEnable()
    {
        // Подписка на событие
        BulletManager.OnBulletHit += OnBulletHitHandler;
    }

    void OnDisable()
    {
        // Отписка от события
        BulletManager.OnBulletHit -= OnBulletHitHandler;
    }

    void Update()
    {
        List<Enemy> enemies = enemiesManager.GetEnemies().Where(enemy => enemy.lives > 0).ToList();

        if (enemies.Count == 0) return;

        int randomIndex = Random.Range(0, enemies.Count + 1);
        Enemy enemy = enemies[randomIndex];

        if (enemy != null && _fireCountdown <= 0f)
        {
            Shoot(enemy);
            _fireCountdown = 1f / _fireRate;
            enemy.lives -= 1;
        }

        _fireCountdown -= Time.deltaTime;
    }

    void Shoot(Enemy enemy)
    {
        Vector2 towerPosition = (Vector2)transform.position;

        bulletManager.Fire(enemy.transform, towerPosition);
    }

    private void OnBulletHitHandler(GameObject bullet, GameObject target)
    {
        List<Enemy> enemies = enemiesManager.GetEnemies();
        Enemy enemy = enemies.Find((enemy) => enemy.gameObject == target);

        if (enemy != null)
        {

            enemiesManager.RemoveEnemy(enemy);
        }
    }
}
