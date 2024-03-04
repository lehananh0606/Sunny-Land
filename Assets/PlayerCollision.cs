using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
   private HeartSystem heartSystem;

    private void Start()
    {
        heartSystem = GetComponent<HeartSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            heartSystem.TakeDamage();
        }
    }
}
