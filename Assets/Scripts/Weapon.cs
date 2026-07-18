using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
// using UnityEngine.

public class Weapon : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
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
    private bool isReloading = false;

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
        if (currentAmmo > 0 && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            currentAmmo--;

            RaycastHit hit;

            // Raycast sai da câmera
            Vector3 rayOrigin = playerCamera.transform.position;
            Vector3 rayDirection = playerCamera.transform.forward;

            if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayCastLength, hitboxLayer))
            {
                Debug.Log($"Collider atingido: {hit.collider.name}");

                EnemyHitbox enemyHitbox = hit.collider.GetComponent<EnemyHitbox>();

                if (enemyHitbox != null)
                {
                    enemyHitbox.TakeDamage(damage);
                }

                hit.collider.attachedRigidbody?.AddForce(
                    rayDirection * impulseForce,
                    ForceMode.Impulse);

                // Linha vermelha da ponta do cano até o alvo atingido
                Debug.DrawLine(
                    bulletPoint.position,
                    hit.point,
                    Color.red,
                    1f);
            }
            Debug.Log(currentAmmo);
        }
        else if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        isReloading = false;
    }
}
