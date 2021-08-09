using System.Collections;
using UnityEngine;

/// <summary>
/// Спавнит волны астероидов и нло
/// </summary>
public class Spawner : MonoBehaviour
{
    /// <summary>
    /// Объект астероида
    /// </summary>
    [Tooltip("The object that is cloned when spawning an asteroid.")]
    public Asteroid asteroidPrefab;

    /// <summary>
    /// Объект Нло
    /// </summary>
    [Tooltip("The object that is cloned when spawning an ufo.")]
    public Ufo Ufo;

    /// <summary>
    /// Расстояние, на котором астероиды появляются от спавнера.
    /// </summary>
    [Tooltip("The distance the asteroids spawn from the spawner.")]
    public float spawnDistance = 12.0f;

    /// <summary>
    /// Количество секунд между циклами появления.
    /// </summary>
    [Tooltip("The amount of seconds between spawn cycles.")]
    public float spawnRate = 1.0f;
    /// <summary>
    /// Количество секунд между циклами появления нло.
    /// </summary>
    [Tooltip("The amount of seconds between spawn cycles ufo.")]
    public float spawnRateUfo;

    /// <summary>
    /// Количество астероидов, порождаемых каждым циклом.
    /// </summary>
    [Tooltip("The amount of asteroids spawned each cycle.")]
    public int amountPerSpawn = 2;

    /// <summary>
    /// Максимальный угол в градусах, под которым астероид будет отклоняться от своей начальной
    /// траектории.
    /// </summary>
    [Tooltip("The maximum angle in degrees the asteroid will steer from its initial trajectory.")]
    [Range(0.0f, 45.0f)]
    public float trajectoryVariance = 15.0f;

    /// <summary>
    /// Transform границ
    /// </summary>
    [Tooltip("Components transform of borders.")]
    public Transform borderLeft;
    public Transform borderRight;

    /// <summary>
    /// Делегат поиска
    /// </summary>
    /// <returns>Возвращает есть/нет искомое</returns>
    delegate bool Find(string f);
    /// <summary>
    /// Экземпляр поиска
    /// </summary>
    Find f;
    
    private void Update()
    {
        #region Поиск и спавн нло
        f = FindUfo;

        if (f("Ufo") == true)
        {
            StartCoroutine("CreateUfo");
        }
        else
        {
            StopCoroutine("CreateUfo");
        } 
        #endregion

        #region Поиск и спавн астероида
        f = FindAsteroids;

        if (f("Asteroid") == true)
        {
            StartCoroutine("CreateAsteroids");
        }
        else
        {
            StopCoroutine("CreateAsteroids");
        } 
        #endregion
    }

    IEnumerator CreateAsteroids()
    {
        yield return new WaitForSeconds(2.0f);
        SpawnAsteroid();
        IncSpawnCount();
    }

    bool FindAsteroids(string tag)
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag(tag);
        bool allAsteroidsAreDestroy = true;
        for (int i = 0; i < asteroids.Length; i++)
        {
            if (asteroids[i] != null)
            {
                allAsteroidsAreDestroy = false;
            }
        }

        return allAsteroidsAreDestroy;
    }

    void SpawnAsteroid()
    {
        for (int i = 0; i <= amountPerSpawn; i++)
        {
            // Выберается случайное направление от центра спавнера и
            // Создаётся астероид на некотором расстоянии
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * spawnDistance;

            //Смещение точки спавна
            spawnPoint += transform.position;

            // Вычисление случайного отклонения во вращении астероида, которое приведет к изменению его траектории
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            // Создайте новый астероид, клонировав префаб и установив случайный
            // размер в пределах диапазона
            Asteroid asteroid = Instantiate(asteroidPrefab, spawnPoint, rotation);
            asteroid.size = asteroid.maxSize;

            
            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.SetTrajectory(trajectory);
            
        }
    }

    void IncSpawnCount() 
    {
        amountPerSpawn++;
    }
    
    bool FindUfo(string tag)
    {
        GameObject ufo = GameObject.FindGameObjectWithTag(tag);
        bool ufoIsDestroy = true;

        if (ufo != null)
        {
            ufoIsDestroy = false;
        }
        return ufoIsDestroy;
    }

    IEnumerator CreateUfo() 
    {
        
        yield return new WaitForSeconds(Random.Range(20.0f, 40.0f));
        SpawnUfo();
    }

    void SpawnUfo()
    {
        Vector2 border;
        Vector2 trajectory;
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            border = borderLeft.transform.position;
            trajectory = Vector2.right;
        }
        else
        {
            border = borderRight.transform.position;
            trajectory = Vector2.left;
        }

        Vector3 spawnPoint = border + new Vector2(0, Random.Range(3, -3));

        Ufo ufo = Instantiate(Ufo, spawnPoint, Ufo.transform.rotation);

        ufo.SetTrajectory(trajectory * border);

    }

}
