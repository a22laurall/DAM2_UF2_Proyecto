using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI txtScore;
    int score;
    int maxScore;
    public static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void GameOver()
    {
        Debug.Log("¡Juego terminado!");
        Time.timeScale = 0;
        //SceneManager.LoadScene(3);

        StartCoroutine(WaitBeforeSceneChange(2f)); 
    }

    private void OnGUI()
    {
        if (txtScore != null)
        {
            txtScore.text = string.Format("{0,4:D4}", score);
        }
    }

    public void IncreaseScore(int amount)
    {
        if (txtScore != null)
        {
            score += amount;
            txtScore.text = string.Format("{0,4:D4}", score);
        }
    }

    private IEnumerator WaitBeforeSceneChange(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);  // Espera sin afectar el Time.timeScale (usando WaitForSecondsRealtime)
        Time.timeScale = 1; // Reiniciar el tiempo
        SceneManager.LoadScene(3);  // Cargar la escena del Game Over (por ejemplo, escena 3)
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        Destroy(instance.gameObject); 
        SceneManager.LoadScene(2);
    }

    void ResetGame()
    {
        score = 0;  // Reiniciar puntaje
    }
}
