using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vanish : MonoBehaviour
{
    // Start is called before the first frame update

   private Coroutine vanishCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vanish"))
        {
            if (vanishCoroutine != null)
            {
                StopCoroutine(vanishCoroutine);
            }
            vanishCoroutine = StartCoroutine(DestroyAfterDelay(collision.gameObject, 1.3f));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Vanish"))
        {
            if (vanishCoroutine != null)
            {
                StopCoroutine(vanishCoroutine);
            }
        }
    }

    IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }
}
