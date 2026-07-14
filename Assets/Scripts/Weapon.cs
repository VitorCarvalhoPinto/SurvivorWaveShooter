using System.Collections;
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
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100f);
            Debug.Log("Hit: " + hit.collider.name);
            
            hit.collider?.GetComponent<Rigidbody>()?.AddForce(hit.normal * -10f, ForceMode.Impulse); // provisorio, tem q tirar vida do inimigo
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
