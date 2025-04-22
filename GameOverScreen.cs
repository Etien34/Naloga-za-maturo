using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text gameOverText;

    public void Setup()
    {
        gameObject.SetActive(true);

        
        if (gameOverText != null)
        {
            gameOverText.text = "Game Over!"; 
        }

        Debug.Log("Game Over screen je prikazan");
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
        Music.DontDestroyOnLoad(this);
    }
}