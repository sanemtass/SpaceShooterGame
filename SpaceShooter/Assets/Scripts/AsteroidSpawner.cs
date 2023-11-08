using System.Collections;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float minSpawnInterval = .2f; // Minimum oluşturulma aralığı
    public float maxSpawnInterval = 5f; // Maximum oluşturulma aralığı
    public float asteroidLifetime = 7f; // Asteroidlerin ömrü
    public int maxAsteroidCount = 10; // Maksimum oluşturulacak asteroid sayısı

    private void Start()
    {
        StartCoroutine(SpawnAsteroids());
    }

    private IEnumerator SpawnAsteroids()
    {
        while (true)
        {
            int asteroidCount = Random.Range(1, maxAsteroidCount + 1);

            for (int i = 0; i < asteroidCount; i++)
            {
                GameObject asteroid = GetRandomAsteroid();
                SetRandomPosition(asteroid); // SetRandomPosition fonksiyonunu çağır
                asteroid.SetActive(true);
            }

            yield return new WaitForSeconds(asteroidLifetime);

            ReturnAsteroids(asteroidCount);

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    private void SpawnAsteroidsInPattern(int count, float startX, float startY, float spacingX, float spacingY)
    {
        float x = startX;
        float y = startY;

        for (int i = 0; i < count; i++)
        {
            GameObject asteroid = GetRandomAsteroid();
            asteroid.transform.position = new Vector2(x, y);
            asteroid.SetActive(true);

            x += spacingX;
            y += spacingY;
        }
    }

    private void ReturnAsteroids(int count) //kontrol et burayi
    {
        for (int i = 0; i < count; i++)
        {
            GameObject asteroid = GetRandomAsteroid();
            if (asteroid.activeSelf)
            {
                ObjectPooler.Instance.SetPoolObject(asteroid, asteroid.GetComponent<ObjectTypeScript>().objectType);
            }
            asteroid.SetActive(false);
        }
    }

    private void SetRandomPosition(GameObject obj)
    {
        float x = Random.Range(-2f, 2f);
        float y = Random.Range(12f, 15f);
        obj.transform.position = new Vector2(x, y);
    }

    private GameObject GetRandomAsteroid()
    {
        int randomType = Random.Range(0, 7); // 0'dan 6'ya kadar olan objectType değerlerini kullanarak objeleri oluştur
        GameObject asteroid = GetAsteroidByObjectType(randomType);
        if (asteroid.activeSelf)
        {
            ObjectPooler.Instance.SetPoolObject(asteroid, randomType);
        }
        return asteroid;
    }

    private GameObject GetAsteroidByObjectType(int objectType)
    {
        foreach (var pool in ObjectPooler.Instance.pools)
        {
            ObjectTypeScript objectTypeScript = pool.objectPrefab.GetComponent<ObjectTypeScript>();
            if (objectTypeScript != null && objectTypeScript.objectType == objectType)
            {
                return ObjectPooler.Instance.GetPoolObject(objectType);
            }
        }
        return null;
    }
}
