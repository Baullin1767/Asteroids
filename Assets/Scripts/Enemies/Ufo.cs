using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Управляет движением и стрельбой Нло
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Ufo : MonoBehaviour
{
    /// <summary>
    /// Скорость дижения
    /// </summary>
    [Tooltip("How quickly the Ufo is able to move forward.")]
    public float movementSpeed = 10.0f;

    /// <summary>
    /// Количество секунд, необходимое игроку для возрождения после смерти.
    /// </summary>
    [Tooltip("The amount of seconds it takes for the Ufo to respawn after dying.")]
    public float respawnDelay = 3.0f;

    /// <summary>
    /// Объект пули
    /// </summary>
    [Tooltip("The object that is cloned when creating a bullet.")]
    public RedBullet redBulletPrefab;

    /// <summary>
    /// Частота стрельбы
    /// </summary>
    public float shooting;

    /// <summary>
    /// Rigidbody component Нло
    /// </summary>
    new Rigidbody2D rigidbody;
    /// <summary>
    /// Положение для игрока
    /// </summary>
    Transform transformPlayer;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        
    }
    private void Start()
    {
        shooting = Random.Range(2f, 5f);
        InvokeRepeating(nameof(Shooting), shooting, shooting);
    }

    void Shooting()
    {
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        shooting = Random.Range(2f, 5f);
        Shoot();
    }
    
    public void SetTrajectory(Vector2 direction)
    {
        //AddForce для Нло
        rigidbody.AddForce(direction * movementSpeed);
    }
    private void Shoot()
    {
        RedBullet radBullet = Instantiate(redBulletPrefab, transform.position, transform.rotation);
        radBullet.Project(transformPlayer.position - transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().UfoDestroyed(this);
            Destroy(gameObject);
        }

    }
}
