using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    // Public method to stop the music (called by the button)
    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // Stop the music
            // Optional: Destroy the object if no longer needed
            Destroy(gameObject);
        }
    }
}