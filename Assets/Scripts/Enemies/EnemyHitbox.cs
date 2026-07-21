using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private EnemyBodyPart bodyPart;
    private EnemyHealth enemyHealth;

    [SerializeField] private float damageTorso = 1f;
    [SerializeField] private float damageNeck = 1.8f;
    [SerializeField] private float damageArm = 0.75f;
    [SerializeField] private float damageLeg = 0.6f;

    void Awake()
    {
        enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage;

        switch (bodyPart)
        {
            case EnemyBodyPart.Head:
                finalDamage = enemyHealth.currentHealth;
                break;

            case EnemyBodyPart.Neck:
                finalDamage *= damageNeck;
                break;

            case EnemyBodyPart.Torso:
                finalDamage *= damageTorso;
                break;

            case EnemyBodyPart.Arm:
                finalDamage *= damageArm;
                break;

            case EnemyBodyPart.Leg:
                finalDamage *= damageLeg;
                break;
        }

        enemyHealth.OnDamage(finalDamage);
    }
}