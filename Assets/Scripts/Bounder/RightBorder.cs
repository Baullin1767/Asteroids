using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform character;
        Transform enemy;
        Transform ufo;
        if (other.CompareTag("Player"))
        {
            character = other.GetComponent<Transform>();
            character.position = new Vector2(transform.position.x + -18.8f, character.position.y);
        }
        else if (other.CompareTag("Asteroid"))
        {
            enemy = other.GetComponent<Transform>();
            enemy.position = new Vector2(transform.position.x + -18.8f, enemy.position.y);
        }
        else if (other.CompareTag("Ufo"))
        {
            ufo = other.GetComponent<Transform>();
            ufo.position = new Vector2(transform.position.x + -18.8f, ufo.position.y);
        }
    }
}
