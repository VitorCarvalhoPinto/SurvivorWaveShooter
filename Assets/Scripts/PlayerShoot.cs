using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] public Transform weaponHolder;
    private bool isFiring = false;
    void OnFire(InputValue value)
    { 
        isFiring = value.isPressed;
        if (value.isPressed && !currentWeapon.isAutomatic)
            currentWeapon.Fire();
    }

    void Update()
    {
        if (isFiring && currentWeapon.isAutomatic)
            currentWeapon.Fire();
    }

    void Start()
    {
        Weapon weapon = Instantiate(currentWeapon, weaponHolder);
        if (weapon != null)
            currentWeapon = weapon;
    }
  
    void OnReload()
    { 
        StartCoroutine(currentWeapon.Reload());
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
