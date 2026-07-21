using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform bulletPoint;

    public string weaponName;
    public float damage;
    public float reloadTime;
    public float fireRate;
    public float nextFireTime;
    public bool isAutomatic;
    public int magazineSize;
    public int currentAmmo;
    public Camera playerCamera;
    public bool isReloading = false;

    public LayerMask hitboxLayer;

    public float impulseForce;
    private float rayCastLength = 30f;

    void Start()
    {
        currentAmmo = magazineSize;
        playerCamera = Camera.main;
    }
    public void Fire()
    {
        if (isReloading)
            return;

        if (currentAmmo > 0 && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            currentAmmo--;

            Vector3 rayOrigin = playerCamera.transform.position;
            Vector3 rayDirection = playerCamera.transform.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayCastLength, hitboxLayer))
            {
                EnemyHitbox enemyHitbox = hit.collider.GetComponent<EnemyHitbox>();

                if (enemyHitbox != null)
                    enemyHitbox.TakeDamage(damage);

                hit.collider.attachedRigidbody?.AddForce(rayDirection * impulseForce, ForceMode.Impulse);

                Debug.DrawLine(bulletPoint.position, hit.point, Color.red, 1f);
            }
        }
    }

    public IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        isReloading = false;
    }
}
