using UnityEngine;

/// <summary>
/// Обрабатывает физику/движение снаряда пули.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class RedBullet : MonoBehaviour
{
    /// <summary>
    /// Скорость пули
    /// </summary>
    [Tooltip("How fast the bullet travels.")]
    public float speed = 500.0f;

    /// <summary>
    /// Максимальное время жизни пули
    /// </summary>
    [Tooltip("The maximum amount of time the bullet will stay alive after being projected.")]
    public float maxLifetime = 10.0f;

    /// <summary>
    /// Rigidbody component для пули
    /// </summary>
    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        
        rigidbody.AddForce(direction * speed);

        // Уничтожение пули верез опредёлённое время
        Destroy(gameObject, maxLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Уничтожение пули после касания чего-нибудь
        Destroy(gameObject);
    }

}
