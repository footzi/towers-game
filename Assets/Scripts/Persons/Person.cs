using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Person : MonoBehaviour
{
  [SerializeField] GameObject selectionBorder;

  public static event Action<Person> OnPersonClick;

  private float _moveSpeed = 5f;
  private Vector2 _movingTargetPosition;
  private bool _isMoving;

  void FixedUpdate()
  {
    if (_isMoving)
    {
      transform.position = Vector2.MoveTowards(transform.position, _movingTargetPosition, _moveSpeed * Time.deltaTime);
    }

    if (Vector2.Distance(transform.position, _movingTargetPosition) < 0.1f)
    {
      _isMoving = false;
    }
  }

  void OnMouseDown()
  {
    OnPersonClick?.Invoke(this);
  }

  public void SetSelection()
  {
    selectionBorder.SetActive(true);
  }

  public void ClearSelection()
  {
    selectionBorder.SetActive(false);
  }

  public void MoveTo(Vector2 vector)
  {
    _movingTargetPosition = vector;
    _isMoving = true;
  }
}
