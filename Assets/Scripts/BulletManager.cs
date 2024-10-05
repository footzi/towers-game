using UnityEngine;
using System;

public class BulletManager : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    public static event Action<GameObject, GameObject> OnBulletHit;

    private float _bulletSpeed = 5f;


    public void Fire(Transform target, Vector2 startPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, startPosition, Quaternion.identity);

        Vector2 bulletPosition = (Vector2)bullet.transform.position;
        Vector2 targetPosition = PredictTargetPosition(target, bulletPosition);
        Vector2 direction = (targetPosition - bulletPosition).normalized;


        // Добавляем скорость к пуле для движения по прямой траектории
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * _bulletSpeed; // Устанавливаем скорость пули
    }

    Vector2 PredictTargetPosition(Transform target, Vector2 bulletPosition)
    {
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();

        Vector2 p0 = bulletPosition; // начальная позиция пули
        Vector2 p1 = target.position; // начальная позиция врага
        Vector2 v1 = targetRb.velocity; // скорость врага

        Vector2 toTarget = p1 - p0; // Вектор от пули до врага
        float a = v1.sqrMagnitude - (_bulletSpeed * _bulletSpeed);
        float b = 2 * Vector2.Dot(toTarget, v1);
        float c = toTarget.sqrMagnitude;

        // Решаем квадратное уравнение для нахождения времени t
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            // Если дискриминант меньше 0, цель недостижима, стреляем в текущую позицию
            return target.position;
        }

        // Находим время столкновения
        float t1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
        float t2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

        // Выбираем наименьшее положительное значение времени t
        float t = Mathf.Min(t1, t2);
        if (t < 0) t = Mathf.Max(t1, t2);
        if (t < 0) return target.position; // Если нет положительных значений, стреляем в текущую позицию

        // Предсказанная позиция врага через время t
        return (Vector2)target.position + v1 * t;
    }

    public static void BulletHit(GameObject bullet, GameObject target)
    {
        if (target.CompareTag("Boundary"))
        {
            Destroy(bullet);
            return;
        }

        OnBulletHit?.Invoke(bullet, target);

        Destroy(bullet);
    }
}
