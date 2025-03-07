using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.anyKeyDown && SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.anyKeyDown && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.R) && SceneManager.GetActiveScene().buildIndex == 3)
        {
            GameManager.gameOver = false;
            GameManager.GetInstance().RestartGame();
        }

    }
}