using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandler : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] private Animator armAnimator;
    [SerializeField] private Animator gunAnimator;

    private const string SHOOT = "triggerShoot";
    private const string INSPECT = "triggerInspect";
    private const string RELOAD = "triggerReload";
    private const string DRAW = "triggerDraw";
    private const string HOLSTER = "triggerHolster";
    
    private void Awake()
    {
        ;
    }

    public void OnInspect(InputValue value) => TriggerAnimation(INSPECT);
    public void OnFire(InputValue value) => TriggerAnimation(SHOOT);
    // public void OnReload(InputValue value) => TriggerAnimation(RELOAD);

    public void OnReload(InputValue value)
    {
        if (value.isPressed)
            TriggerAnimation(RELOAD);
    }

    private void TriggerAnimation(string triggerName)
    {
        armAnimator?.SetTrigger(triggerName);
        gunAnimator?.SetTrigger(triggerName);
    }
}