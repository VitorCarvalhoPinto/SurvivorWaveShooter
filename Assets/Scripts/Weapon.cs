using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
// using UnityEngine.

public class Weapon : MonoBehaviour
{
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

    public float impulseForce;
    private float rayCastLength = 30f;

    RagdollController ragdollController;
    void Start()
    {
        currentAmmo = magazineSize;
        playerCamera = Camera.main;
        ragdollController = GetComponent<RagdollController>();
    }
    public void Fire()
    {
        if (currentAmmo > 0 && Time.time >= nextFireTime && !isReloading)
        {
            nextFireTime = Time.time + fireRate;
            RaycastHit hit;
            currentAmmo--;
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, rayCastLength);
            
            Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.forward * rayCastLength, Color.red, 1f);
            
            if (hit.collider)
            {
                Debug.Log("Hit: " + hit.collider.name);

                RagdollController ragdoll = hit.collider.GetComponentInParent<RagdollController>();

                if (ragdoll != null)
                    ragdoll.EnableRagdoll();

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
