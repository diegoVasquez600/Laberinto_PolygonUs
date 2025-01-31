using UnityEngine;

public class MazeExit : MonoBehaviour
{
    public GameOverManager gameOverManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the exit!");
            gameOverManager.ShowGameOver();
        }
    }
}
