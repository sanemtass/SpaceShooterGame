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

    public TextMeshProUGUI starCountText; 
    public TextMeshProUGUI scoreText; 
    public GameObject startPanel;
    public GameObject endPanel;

    private int starCount = 0; 
    private int score = 0;

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

    public void UpdateScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateStarCountText()
    {
        starCountText.text = " " + starCount.ToString();
    }

    private void UpdateScoreText()
    {
        scoreText.text = " " + score.ToString();
    }
}
