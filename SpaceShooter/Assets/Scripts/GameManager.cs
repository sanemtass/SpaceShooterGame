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
        // Oyun zamanını normale çevir
        Time.timeScale = 1f;
        // İsterseniz başka başlangıç işlemleri ekleyebilirsiniz
        SceneManager.LoadScene("SampleScene"); // Oyun sahnesini yeniden yükleme örneği
    }

    public void PauseButton()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f; // Devam ettir
        }
        else
        {
            Time.timeScale = 0f; // Duraklat
        }
    }
}
