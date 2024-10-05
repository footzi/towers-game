using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
  public int lives = 1;
  private float _fallSpeed = 2f;

  private float _currentFallSpeed = 0;


  public static event Action<Collider2D, Enemy> OnCollision;

  public static Enemy Create(GameObject _enemyPrefab, Vector2 position)
  {
    GameObject enemyObject = Instantiate(_enemyPrefab, position, Quaternion.identity);

    return enemyObject.GetComponent<Enemy>();
  }

  public void CalcFallSpeed(int maxValue)
  {
    int random = UnityEngine.Random.Range(1, maxValue + 2);

    _currentFallSpeed = _fallSpeed * random;
  }

  public float GetFallSpeed()
  {
    return _currentFallSpeed;
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    OnCollision?.Invoke(other, this);
  }
}
