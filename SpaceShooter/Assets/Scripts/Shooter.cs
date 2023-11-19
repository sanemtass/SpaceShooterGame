using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform firePoint;           // Ateş etme noktası
    public float bulletSpeed = 10f;       // Mermi hızı
    public float fireRate = 3f;         // Ateş etme hızı
    private float nextFireTime = 0f;      // Bir sonraki ateş zamanı

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // nextFireTime'ı güncelle
        }
    }

    private void Shoot()
    {
        // Object pooler'dan mermiyi al
        GameObject bullet = ObjectPooler.Instance.GetPoolObject(7);

        if (bullet != null)
        {
            // Mermiyi ateş etme noktasına yerleştir
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            // Mermiye hız ver
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = bullet.transform.up * bulletSpeed;

            // Mermi etkisiz hale getirildiğinde geri döndürmek için coroutine başlat
            StartCoroutine(ReturnBulletToPool(bullet));
        }
    }

    private IEnumerator ReturnBulletToPool(GameObject bullet)
    {
        // Belirli bir süre sonra mermiyi havuza geri ekle
        yield return new WaitForSeconds(2f);
        ObjectPooler.Instance.SetPoolObject(bullet, 7);
    }
}
