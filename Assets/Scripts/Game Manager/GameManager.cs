using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Управляет состоянием игры, таким как подсчет очков, смерть и окончание игры.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Игрок
    /// </summary>
    [Tooltip("The player component.")]
    public Player player;

    /// <summary>
    /// Эффект взрыва
    /// </summary>
    [Tooltip("The particle effect that is played when an asteroid is destroyed and when the player dies.")]
    public ParticleSystem explosionEffect;

    /// <summary>
    /// Пользовательский интерфейс, отображаемый во время состояния "Game over".
    /// </summary>
    [Tooltip("The UI displayed during the game over state.")]
    public GameObject gameOverUI;
   

    /// <summary>
    /// Текущий счет игрока.
    /// </summary>
    [Tooltip("The player's current score")]
    public int score;

    /// <summary>
    /// Текст пользовательского интерфейса, отображающий счет игрока.
    /// </summary>
    [Tooltip("The UI text that displays the player's score.")]
    public Text scoreText;

    /// <summary>
    /// Текущее количество жизней игрока.
    /// </summary>
    [Tooltip("The player's current quantity lives.")]
    public int lives;

    /// <summary>
    /// Текст пользовательского интерфейса, отображающий жизни игрока.
    /// </summary>
    [Tooltip("The UI text that displays the player's lives.")]
    public Text livesText;

    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        if (lives <= 0)
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        // Очищает сцену от астероидов, чтобы начать сначала
        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }

        gameOverUI.SetActive(false);
        
        SetScore(0);
        SetLives(3);
        Respawn();
    }

    public void Respawn()
    {
        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosionEffect.transform.position = asteroid.transform.position;
        explosionEffect.Play();

        // Оценка увеличивается в зависимости от размера астероида
        if (asteroid.size < 0.7f) {
            SetScore(score + 100); // Маленикий астероид
        } else if (asteroid.size < 1.4f) {
            SetScore(score + 50); // средний астероид
        } else {
            SetScore(score + 20); // Большой астероид
        }
    }

    public void UfoDestroyed(Ufo ufo)
    {
        explosionEffect.transform.position = ufo.transform.position;
        explosionEffect.Play();

        SetScore(score + 200);
    }

    public void PlayerDeath(Player player)
    {
        explosionEffect.transform.position = player.transform.position;
        explosionEffect.Play();

        SetLives(lives - 1);

        if (lives <= 0) {
            GameOver();
        } else {
            Invoke(nameof(Respawn), player.respawnDelay);
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives += lives;
        livesText.text = lives.ToString();
    }

}
