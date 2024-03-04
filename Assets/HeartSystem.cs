using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private int heartCount = 3;

    private const string heartCountKey = "HeartCount";

    private void Start()
    {
        // Load heartCount from PlayerPrefs when the scene starts
        heartCount = PlayerPrefs.GetInt(heartCountKey, 3);
        UpdateHeartsDisplay();
    }

    public void TakeDamage()
    {
        if (heartCount > 0)
        {
            heartCount--;
            UpdateHeartsDisplay();
        }
    }

    public void UpdateHeartsDisplay()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < heartCount)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private void OnDestroy()
    {
        // Save heartCount to PlayerPrefs when the object is destroyed or the scene changes
        PlayerPrefs.SetInt(heartCountKey, heartCount);
        PlayerPrefs.Save();
    }
}