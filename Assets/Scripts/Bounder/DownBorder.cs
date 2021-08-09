using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBorder : MonoBehaviour
{
    public bool b = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform character;
        Transform enemy;
        Transform ufo;
        if (other.CompareTag("Player"))
        {
            b = true;
            character = other.GetComponent<Transform>();
            character.position = new Vector2(character.position.x, transform.position.y + 11f);
        }
        else if (other.CompareTag("Asteroid"))
        {
            enemy = other.GetComponent<Transform>();
            enemy.position = new Vector2(enemy.position.x, transform.position.y + 11f);
        }
        else if (other.CompareTag("Ufo"))
        {
            ufo = other.GetComponent<Transform>();
            ufo.position = new Vector2(ufo.position.x, transform.position.y + 11f);
        }
    }
}
