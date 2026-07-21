using TMPro;
using UnityEngine;

public class UIAmmoHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAmmo;
    private PlayerShoot currentWeaponInfo;
    private int lastAmmo = -1;
    private int lastMagazineSize = -1;

    void Awake()
    {
        currentWeaponInfo = FindFirstObjectByType<PlayerShoot>();
    }

    void Update()
    {
        Weapon weapon = currentWeaponInfo.GetCurrentWeapon();

        if (weapon == null)
            return;

        if (weapon.currentAmmo == lastAmmo && weapon.magazineSize == lastMagazineSize)
            return;

        lastAmmo = weapon.currentAmmo;
        lastMagazineSize = weapon.magazineSize;
        textAmmo.text = $"{lastAmmo}/{lastMagazineSize}";
    }
}
