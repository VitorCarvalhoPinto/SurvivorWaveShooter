using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
// using UnityEngine.

public class Weapon : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;

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
        if (currentAmmo > 0 && Time.time >= nextFireTime && !isReloading)
        {
            nextFireTime = Time.time + fireRate;
            RaycastHit hit;
            currentAmmo--;
            // Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayCastLength);
            Physics.Raycast(
                playerCamera.transform.position,
                playerCamera.transform.forward,
                out hit,
                rayCastLength,
                hitboxLayer
            );
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.forward * rayCastLength, Color.red, 1f);

            if (hit.collider)
            {

                EnemyHitbox enemyHitbox = hit.collider.GetComponent<EnemyHitbox>();

                Debug.Log($"Collider atingido: {hit.collider.name} | Vida: {enemyHealth.currentHealth}");

                if (enemyHitbox != null)
                {
                    enemyHitbox.TakeDamage(damage);
                }

                hit.collider.attachedRigidbody?.AddForce(
                    playerCamera.transform.forward * impulseForce,
                    ForceMode.Impulse);
            }
            Debug.Log(currentAmmo);
        }
        else if (currentAmmo <= 0 && !isReloading) StartCoroutine(Reload());
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
