using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyBehaviour enemyBehaviour;
    public float maxHealth = 100f;
    public float currentHealth;

    private void Awake()
    {
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        currentHealth = maxHealth;
    }

    public void OnDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            GameManager.Instance.AddKill();
            enemyBehaviour.Die();
        }
    }
}