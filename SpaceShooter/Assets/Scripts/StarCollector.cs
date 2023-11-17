using System.Collections;
using UnityEngine;

public class StarCollector : MonoBehaviour
{
    public GameObject spaceObject;  // Yıldızın SpriteRenderer bileşeni
    public float collectFlashDuration = 0.2f;  // Yıldız toplandığında renk değişikliğinin süresi

    private Material spaceMaterial;

    private void Start()
    {
        spaceMaterial = spaceObject.GetComponent<Renderer>().material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Star"))
        {
            CollectStar(other.gameObject);
            Debug.Log("star");
        }
    }

    private void CollectStar(GameObject starObject)
    {
        ObjectTypeScript objectTypeScript = starObject.GetComponent<ObjectTypeScript>();

        if (objectTypeScript != null)
        {
            UIManager.Instance.UpdateStarCount(1); // UIManager üzerinden yıldız sayısını güncelle
            UIManager.Instance.UpdateScore(5); // UIManager üzerinden skoru güncelle

            // Yıldızı ObjectPooler ile havuza geri ekleyin
            ObjectPooler.Instance.SetPoolObject(starObject, objectTypeScript.objectType);

            // Yıldız toplandığında rengi değiştir ve belirli bir süre sonra eski rengine dön
            StartCoroutine(ChangeColorForDuration(Color.yellow, 0.3f));
        }
        else
        {
            Debug.LogError("ObjectTypeScript bulunamadı!");
        }
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
