using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] GameObject[] walls;

    void OnEnable()
    {
        EnemiesManager.OnEnemyCollision += OnEnemyCollisionHandler;
    }

    void OnDisable()
    {
        EnemiesManager.OnEnemyCollision -= OnEnemyCollisionHandler;
    }

    public void OnEnemyCollisionHandler(Collider2D collision, Enemy enemy)
    {
        GameObject wall = Array.Find(walls, (obj) => obj == collision.gameObject);

        if (wall)
        {
            Destroy(wall);
        };
    }
}
