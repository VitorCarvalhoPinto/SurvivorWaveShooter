using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private BodyPart bodyPart;
    [SerializeField] private EnemyHealth enemyHealth;

    private float damageHead = 2f;
    private float damageTorso = 1f;
    private float damageArm = 0.75f;
    private float damageLeg = 0.6f;

    public void TakeDamage(float damage)
    {
        float finalDamage = damage;

        switch (bodyPart)
        {
            case BodyPart.Head:
                finalDamage *= damageHead;
                break;

            case BodyPart.Torso:
                finalDamage *= damageTorso;
                break;

            case BodyPart.Arm:
                finalDamage *= damageArm;
                break;

            case BodyPart.Leg:
                finalDamage *= damageLeg;
                break;
        }

        enemyHealth.OnDamage(finalDamage);
    }
}