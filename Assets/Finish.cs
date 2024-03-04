using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null && collision.gameObject.name == ("Player"))
        {
            ItemCollect itemCollect = collision.gameObject.GetComponent<ItemCollect>();
            if (itemCollect != null)
            {
                int playerScore = itemCollect.cherries;
                SavePoint savePoint = collision.gameObject.GetComponent<SavePoint>();
                if (savePoint != null)
                {
                    savePoint.SavePlayerScore(playerScore);
                }
            }

            // Chuyển cảnh sau 2 giây
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel()
    {
        // Chuyển cảnh sang màn tiếp theo
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
