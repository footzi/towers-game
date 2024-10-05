using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public static event Action<Vector2> OnClickByGround;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем нажатие левой кнопки мыши
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Получаем позицию мыши в мировых координатах

            // Проверяем попадание клика на землю
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
            {
                OnClickByGround?.Invoke(mousePosition);
            }
        }
    }
}
