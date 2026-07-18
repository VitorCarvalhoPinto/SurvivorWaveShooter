using TMPro;
using UnityEngine;

public class UIAmmoHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAmmo;
    private PlayerShoot currentWeaponInfo;

    void Awake()
    {
        currentWeaponInfo = FindFirstObjectByType<PlayerShoot>();
    }

    void Update()
    {
        Weapon weapon = currentWeaponInfo.GetCurrentWeapon();

        if (weapon != null)
        {
            textAmmo.text = $"{weapon.currentAmmo}/{weapon.magazineSize}";
        }
    }
}
