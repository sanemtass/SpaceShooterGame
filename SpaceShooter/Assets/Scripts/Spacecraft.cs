using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spacecraft : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public AsteroidSpawner asteroidSpawner;
    public Slider healthSlider; // Slider referansı
    public GameObject spaceObject; // Görsel obje referansı
    private Material spaceMaterial; // Görsel objenin materyal referansı

    private void Start()
    {
        currentHealth = maxHealth;

        // Slider referansını atayın
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        // Görsel objenin materyal referansını alın
        spaceMaterial = spaceObject.GetComponent<Renderer>().material;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        // Sağlık değerine göre Slider'ı güncelle
        healthSlider.value = currentHealth;

        // .2 saniye boyunca kırmızı renge dön
        StartCoroutine(ChangeColorForDuration(Color.red, 0.3f));
    }

    private void Die()
    {
        Debug.Log("Uzay Aracı Öldü");
        UIManager.Instance.endPanel.SetActive(true);
        //asteroidSpawner.gameStarted = false;
        Time.timeScale = 0f; //ne kadar mantikli bilmiyorum
    }

    private IEnumerator ChangeColorForDuration(Color newColor, float duration)
    {
        // Renk değişimi
        spaceMaterial.color = newColor;

        // Belirtilen süre boyunca bekleyin
        yield return new WaitForSeconds(duration);

        // Süre dolduktan sonra renki eski haline getirin (örneğin, beyaz)
        spaceMaterial.color = Color.white;
    }
}
