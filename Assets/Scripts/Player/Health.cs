using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private HealthBar healthBar;

    [SerializeField] private bool isPlayer = false; // отмечаем, игрок это или враг
    [SerializeField] Animator animator;
    private void Start()
    {
        currentHealth = maxHealth;

        // ищем HealthBar среди дочерних объектов
        healthBar = GetComponentInChildren<HealthBar>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);

        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        if (isPlayer)
        {
            Debug.Log("Player died!");
            // TODO: экран смерти / рестарт сцены
        }
        else
        {
            Debug.Log($"{gameObject.name} died!");
            animator.Play("EnemyDie");
            Destroy(gameObject,0.2f);
        }
    }
}