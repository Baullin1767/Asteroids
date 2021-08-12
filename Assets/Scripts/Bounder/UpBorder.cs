using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpBorder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Transform transformOther;
        transformOther = other.GetComponent<Transform>();
        transformOther.position = new Vector2(transformOther.position.x, transform.position.y + -11f);
        
    }

    
}
