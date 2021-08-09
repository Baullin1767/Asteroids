using UnityEngine;

/// <summary>
/// Управляет движением и стрельбой корабля игрока
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Скорость дижения
    /// </summary>
    [Tooltip("How quickly the player is able to move forward.")]
    public float thrustSpeed = 1.0f;

    /// <summary>
    /// Скорость вращения
    /// </summary>
    [Tooltip("How quickly the player is able to turn.")]
    public float rotationSpeed = 0.1f;

    /// <summary>
    /// Количество секунд, необходимое игроку для возрождения после смерти.
    /// </summary>
    [Tooltip("The amount of seconds it takes for the player to respawn after dying.")]
    public float respawnDelay = 3.0f;

    /// <summary>
    /// Время бессмертия пеосонажа
    /// </summary>
    [Tooltip("The amount of seconds the player has invulnerability after respawning. This is to prevent the player from instantly dying if spawning into an asteroid.")]
    public float respawnInvulnerability = 3.0f;

    /// <summary>
    /// Объект пули
    /// </summary>
    [Tooltip("The object that is cloned when creating a bullet.")]
    public Bullet bulletPrefab;

    /// <summary>
    /// Направление поворота. 1=налево, -1=направо, 0=прямо
    /// </summary>
    public float turnDirection = 0.0f;

    /// <summary>
    /// Активированы ли двигатели корабля, заставляющие его двигаться вперед.
    /// </summary>
    public bool thrusting;

    /// <summary>
    /// Rigidbody component игрока
    /// </summary>
    new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        
        gameObject.layer = LayerMask.NameToLayer("Ignore Collisions");

        Invoke(nameof(TurnOnCollisions), respawnInvulnerability);
    }

    private void Update()
    {
        this.thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            turnDirection = -1.0f;
        } else {
            turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Add force для движения вперёд
        if (thrusting) {
            rigidbody.AddForce(transform.up * thrustSpeed);
        }

        // Add torque для поворота игрока
        if (turnDirection != 0.0f) {
            rigidbody.AddTorque(rotationSpeed * turnDirection);
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Project(transform.up);
    }

    private void TurnOnCollisions()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "RedBullet" || collision.gameObject.tag == "Ufo")
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = 0.0f;
            gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDeath(this);
        }
    }

}
