using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandler : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator armAnimator;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Weapon currentWeapon;

    private const string SHOOT = "triggerShoot";
    private const string INSPECT = "triggerInspect";
    private const string RELOAD = "triggerReload";
    private const string DRAW = "triggerDraw";
    private const string HOLSTER = "triggerHolster";

    public void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    public void OnInspect(InputValue value) => TriggerAnimation(INSPECT);
    public void OnFire(InputValue value)
    {
        if (value.isPressed && currentWeapon != null && !currentWeapon.isReloading && currentWeapon.currentAmmo > 0)
            TriggerAnimation(SHOOT);
    }

    public void OnReload(InputValue value)
    {
        if (value.isPressed)
            TriggerReloadAnimation();
    }

    public void TriggerReloadAnimation()
    {
        if (currentWeapon != null && !currentWeapon.isReloading)
            TriggerAnimation(RELOAD);
    }

    private void TriggerAnimation(string triggerName)
    {
        armAnimator?.SetTrigger(triggerName);
        gunAnimator?.SetTrigger(triggerName);
    }
}