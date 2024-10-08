using System;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public static event Action<Vector2> OnClickByGround;

    void OnMouseDown()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
        {
            OnClickByGround?.Invoke(mousePosition);
        }
    }
}
