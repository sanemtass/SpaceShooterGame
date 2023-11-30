using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spacecraft : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public AsteroidSpawner asteroidSpawner;
    public Slider healthSlider; 
    public GameObject spaceObject;
    private Material spaceMaterial;

    private void Start()
    {
        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

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

        healthSlider.value = currentHealth;

        StartCoroutine(ChangeColorForDuration(Color.red, 0.3f));
    }

    private void Die()
    {
        Debug.Log("Uzay Aracı Öldü");
        UIManager.Instance.endPanel.SetActive(true);
        Time.timeScale = 0f; //ne kadar mantikli bilmiyorum
    }

    private IEnumerator ChangeColorForDuration(Color newColor, float duration)
    {
        spaceMaterial.color = newColor;

        yield return new WaitForSeconds(duration);

        spaceMaterial.color = Color.white;
    }
}
