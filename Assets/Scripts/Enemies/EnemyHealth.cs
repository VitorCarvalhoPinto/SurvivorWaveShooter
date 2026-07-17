using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour enemyBehaviour;

    RaycastHit hit;
    public float maxHealth = 100f;
    public float currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void OnDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            enemyBehaviour.Die();
        }
    }


}
