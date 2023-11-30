using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public AsteroidSpawner asteroidSpawner;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("SampleScene");
        asteroidSpawner.gameStarted = true;
        UIManager.Instance.startPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }

    public void PauseButton()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; 
        }
        else
        {
            Time.timeScale = 0f; 
        }
    }
}
