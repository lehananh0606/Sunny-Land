// LifeSpan.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeSpan : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private int remainingLives = 3;
    private bool isDead = false;
    public float knockbackForce = 100f; // Adjust as needed

    void Start()
    {
        anim = GetComponent<Animator>(); 
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead && collision.gameObject.CompareTag("Trap"))
        {
            Die();
            PushBack(collision.transform.position.x);
        }
    }

    private void Die()
    {
        remainingLives--;
        if (remainingLives <= 0)
        {
            SetDeathAnimation();
        }
        else
        {
            SetHitAnimation();
        }
    }

    private void SetHitAnimation()
    {
        anim.SetTrigger("hit");
    }

    private void SetDeathAnimation()
    {
        isDead = true;
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Invoke("RestartLevel1", 1f); 
    }

    private void RestartLevel1()
    {
        ItemCollect itemCollect = FindObjectOfType<ItemCollect>();
        if (itemCollect != null)
        {
            itemCollect.ResetCherries();
        }
        
        SceneManager.LoadScene(1); 
    }

    private void PushBack(float collisionPositionX)
    {
        Vector2 direction = transform.position.x < collisionPositionX ? Vector2.left : Vector2.right;
        rb.velocity = direction * knockbackForce;
    }
}