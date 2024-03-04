// SavePoint.cs
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public string scoreKey = "PlayerScore";

    // Function to save the player's score
    public void SavePlayerScore(int score)
    {
        // Save the score to PlayerPrefs
        PlayerPrefs.SetInt(scoreKey, score);

        // Save PlayerPrefs immediately
        PlayerPrefs.Save();
    }

    // Function to load the player's score
    public int LoadPlayerScore()
    {
        // Load the score from PlayerPrefs
        int score = PlayerPrefs.GetInt(scoreKey);

        // Return the score
        return score;
    }

    // Function to delete the saved player score
    public void DeleteSavedScore()
    {
        PlayerPrefs.DeleteKey(scoreKey);
        PlayerPrefs.Save();
    }
}