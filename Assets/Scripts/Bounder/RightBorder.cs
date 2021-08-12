using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Transform transformOther;
        transformOther = other.GetComponent<Transform>();
        transformOther.position = new Vector2(transformOther.position.x + -18.8f, transform.position.y);
        
    }
}
