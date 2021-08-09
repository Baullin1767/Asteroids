using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Обрабатывает движение и столкновение астероида.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    /// <summary>
    /// Массив спрайтов, один из которых случайным образом присваивается астероиду.
    /// </summary>
    [Tooltip("An array of sprites of which one is randomly assigned to the asteroid.")]
    public Sprite[] sprites;

    /// <summary>
    /// Текущий назначенный размер астероида.
    /// </summary>
    [HideInInspector]
    public float size = 1.0f;

    /// <summary>
    /// Минимальный размер, который может быть присвоен астероиду.
    /// </summary>
    [Tooltip("The minimum size that can be assigned to the asteroid.")]
    public float minSize = 0.35f;

    /// <summary>
    /// Максимальный размер, который может быть присвоен астероиду.
    /// </summary>
    [Tooltip("The maximum size that can be assigned to the asteroid.")]
    public float maxSize = 1.65f;

    /// <summary>
    /// Как быстро астероид движется по своей траектории.
    /// </summary>
    [Tooltip("How quickly the asteroid moves along its trajectory.")]
    public float movementSpeed = 50.0f;

    
    /// <summary>
    /// Компонент визуализации спрайтов, прикрепленный к астероиду.
    /// </summary>
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// Rigidbody component для астероида.
    /// </summary>
    new Rigidbody2D rigidbody;
    

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Присвоение уникальных свойств
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        // Установление масштаба и массы астероида на основе заданного размера, чтобы
        // физика была более реалистичной
        transform.localScale = Vector3.one * size;
        rigidbody.mass = size;

        
    }

    public void SetTrajectory(Vector2 direction)
    {
        //AddForce для астероида
        rigidbody.AddForce(direction * movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "RedBullet")
        {
            // Проверка размера астероида, чтобы разделить его пополам (обе
            // части должны быть больше минимального размера)
            if ((size * 0.5f) >= minSize)
            {
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            // Уничтожение астероида и удаление его из списка, так как он либо заменен двумя
            // новыми астероидами или достаточно маленький, чтобы быть уничтоженным пулей
            Destroy(gameObject);
        }
    }

    private Asteroid[] CreateSplit()
    {
        // Новое положение астероида, как у текущего астероида
        // но с небольшим смещением, чтобы они не появлялись друг в друге
        Vector2 position = transform.position;
        

        // Создание двух новых астероидов размером в половину текущего
                
        Vector3 one = transform.TransformDirection(Vector2.one);
        Asteroid asteroid1 = Instantiate(this, position + new Vector2(0.5f, 0), transform.rotation);
        asteroid1.SetTrajectory(one);
        asteroid1.size = size * 0.5f;

        Asteroid asteroid2 = Instantiate(this, position + new Vector2(-0.5f ,0), transform.rotation);
        asteroid2.SetTrajectory(one);
        asteroid2.size = size * 0.5f;

        Asteroid[] asteroids = new Asteroid[2];
        asteroids[0] = asteroid1;
        asteroids[1] = asteroid2;
        
        return asteroids;
    }

}
