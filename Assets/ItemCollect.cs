// ItemCollect.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{
    public int cherries = 0;
    [SerializeField] private Text cherriesText;

    private const string cherriesKey = "PlayerCherries";

    private void Start()
    {
        // Load cherries from PlayerPrefs when the scene starts
        cherries = PlayerPrefs.GetInt(cherriesKey, 0);
        UpdateCherriesText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherries++;
            UpdateCherriesText();

            // Save cherries to PlayerPrefs
            PlayerPrefs.SetInt(cherriesKey, cherries);
            PlayerPrefs.Save();
        }
    }

    private void UpdateCherriesText()
    {
        cherriesText.text = "Point: " + cherries;
    }

    public void ResetCherries()
    {
        PlayerPrefs.SetInt(cherriesKey, 0);
        cherries = 0;
        UpdateCherriesText();
        PlayerPrefs.Save();
    }
}