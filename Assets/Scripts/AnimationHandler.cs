using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationHandler : MonoBehaviour
{
    [Header("Input Actions")]
    public InputActionReference shootAction;
    public InputActionReference inspectAction;
    public InputActionReference reloadAction;
    public InputActionReference drawAction;
    public InputActionReference holsterAction;

    [Header("Animators")]
    [SerializeField] private Animator armAnimator;
    [SerializeField] private Animator gunAnimator;

    private const string SHOOT = "triggerShoot";
    private const string INSPECT = "triggerInspect";
    private const string RELOAD = "triggerReload";
    private const string DRAW = "triggerDraw";
    private const string HOLSTER = "triggerHolster";

    private Weapon weapon;
    
    private void OnEnable()
    {
        if (shootAction != null)
        {
            shootAction.action.Enable();
            shootAction.action.performed += OnShoot;
        }

        if (inspectAction != null)
        {
            inspectAction.action.Enable();
            inspectAction.action.performed += OnInspect;
        }
        if (reloadAction != null)
        {
            reloadAction.action.Enable();
            reloadAction.action.performed += OnReload;
        }
        if (drawAction != null)
        {
            drawAction.action.Enable();
            drawAction.action.performed += OnDraw;
        }
        if (holsterAction != null)
        {
            holsterAction.action.Enable();
            holsterAction.action.performed += OnHolster;
        }
    }

    private void OnDisable()
    {
        if (shootAction != null)
        {
            shootAction.action.performed -= OnShoot;
            shootAction.action.Disable();
        }

        if (inspectAction != null)
        {
            inspectAction.action.performed -= OnInspect;
            inspectAction.action.Disable();
        }

        if (reloadAction != null)
        {
            reloadAction.action.performed -= OnReload;
            reloadAction.action.Disable();
        }
        if (drawAction != null)
        {
            drawAction.action.performed -= OnDraw;
            drawAction.action.Disable();
        }
        if (holsterAction != null)
        {
            holsterAction.action.performed -= OnHolster;
            holsterAction.action.Disable();
        }
    }

    
    private void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    public void OnInspect(InputAction.CallbackContext context) => TriggerAnimation(INSPECT);
    public void OnShoot(InputAction.CallbackContext context)
    {
        // if (weapon.currentAmmo > 0)
        // {
            TriggerAnimation(SHOOT);
            // Debug.Log($"Shot fired! Remaining bullets: {weapon.currentAmmo}");
        // }
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        // if (weapon.currentAmmo < 0 && !shootAction.action.IsPressed())
        // {
        Debug.Log(context);
            TriggerAnimation(RELOAD);
        //     Debug.Log($"Bullets refilled to {weapon.magazineSize}.");
        // }
    }
    public void OnDraw(InputAction.CallbackContext context)
    {
        // Debug.Log("Got gun");
        TriggerAnimation(DRAW);
    }
    public void OnHolster(InputAction.CallbackContext context)
    {
        // Debug.Log("Holstered gun");
        TriggerAnimation(HOLSTER);
    }
    private void TriggerAnimation(string triggerName)
    {
        Debug.Log(triggerName);
        armAnimator?.SetTrigger(triggerName);
        // gunAnimator?.SetTrigger(triggerName);
    }
}