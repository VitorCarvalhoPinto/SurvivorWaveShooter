using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] public Transform weaponHolder;
    [SerializeField] private AnimationHandler animationHandler;
    private bool isFiring = false;

    void Update()
    {
        if (isFiring && currentWeapon != null && currentWeapon.isAutomatic)
            TryFire();
    }

    void Start()
    {
        animationHandler = GetComponentInChildren<AnimationHandler>();

        if (currentWeapon == null)
            return;

        if (currentWeapon.transform.parent != weaponHolder)
        {
            if (currentWeapon.gameObject.scene.IsValid())
            {
                currentWeapon.transform.SetParent(weaponHolder, true);
            }
            else
            {
                Weapon weapon = Instantiate(currentWeapon, weaponHolder, false);
                if (weapon != null)
                    currentWeapon = weapon;
            }
        }

        animationHandler?.SetWeapon(currentWeapon);
    }
  
    void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
        if (value.isPressed && currentWeapon != null && !currentWeapon.isAutomatic)
            TryFire();
    }

    void OnReload(InputValue value)
    {
        if (value.isPressed)
            TryReload();
    }

    private void TryFire()
    {
        if (currentWeapon == null || currentWeapon.isReloading)
            return;

        if (currentWeapon.currentAmmo <= 0)
        {
            TryReload();
            return;
        }

        currentWeapon.Fire();

        if (currentWeapon.currentAmmo <= 0)
            TryReload();
    }

    private void TryReload()
    {
        if (currentWeapon == null || currentWeapon.isReloading)
            return;

        if (currentWeapon.currentAmmo >= currentWeapon.magazineSize)
            return;

        animationHandler?.TriggerReloadAnimation();
        StartCoroutine(currentWeapon.Reload());
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}