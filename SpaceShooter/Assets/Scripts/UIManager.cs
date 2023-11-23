using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();

                if (instance == null)
                {
                    Debug.LogError("UIManager bulunamadı.");
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
                }
            }

            return instance;
        }
    }

    public TextMeshProUGUI starCountText; // Yıldız sayısını gösteren TextMeshPro
    public TextMeshProUGUI scoreText; // Skoru gösteren TextMeshPro
    public GameObject startPanel;
    public GameObject endPanel;

    private int starCount = 0; // Yıldız sayısı
    private int score = 0; // Skor

    private void Start()
    {
        UpdateStarCountText();
        UpdateScoreText();
    }

    public void UpdateStarCount(int amount)
    {
        starCount += amount;
        UpdateStarCountText();
    }

    // Skoru güncelle ve ekrana yansıt
    public void UpdateScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Yıldız sayısı TextMeshPro'yu güncelle
    private void UpdateStarCountText()
    {
        starCountText.text = " " + starCount.ToString();
    }

    // Skor TextMeshPro'yu güncelle
    private void UpdateScoreText()
    {
        scoreText.text = " " + score.ToString();
    }
}
