using System.Collections;
using UnityEngine;

public class StarCollector : MonoBehaviour
{
    public GameObject spaceObject; 
    public float collectFlashDuration = 0.2f; 

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
            UIManager.Instance.UpdateStarCount(1);
            UIManager.Instance.UpdateScore(5); 
            ObjectPooler.Instance.SetPoolObject(starObject, objectTypeScript.objectType);

            StartCoroutine(ChangeColorForDuration(Color.yellow, 0.3f));
        }
        else
        {
            Debug.LogError("ObjectTypeScript bulunamadı!");
        }
    }

    private IEnumerator ChangeColorForDuration(Color newColor, float duration)
    {
        spaceMaterial.color = newColor;

        yield return new WaitForSeconds(duration);

        spaceMaterial.color = Color.white;
    }
}
