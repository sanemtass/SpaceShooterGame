using UnityEngine;

public class AsteroidDestroyed : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            ObjectTypeScript objectTypeScript = GetComponent<ObjectTypeScript>();

            if (objectTypeScript != null)
            {
                int asteroidType = objectTypeScript.objectType;
                ReturnToPool(asteroidType);
            }
            else
            {
                Debug.LogError("ObjectTypeScript bulunamadı!");
            }
        }
    }

    private void ReturnToPool(int asteroidType)
    {
        gameObject.SetActive(false);

        ObjectPooler.Instance.SetPoolObject(gameObject, asteroidType);
    }
}
