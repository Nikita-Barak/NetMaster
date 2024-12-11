using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] string level;
    [SerializeField] float nextLevelDelay = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke(nameof(NextLevel), nextLevelDelay);
        }
    }

    void NextLevel()
    {
        SceneManager.LoadScene(level);
    }
}