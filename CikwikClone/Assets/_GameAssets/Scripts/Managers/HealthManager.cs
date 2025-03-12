using UnityEngine;

public class HealtManager : MonoBehaviour
{
    public static HealtManager instance;

    [Header("References")]
    [SerializeField] PlayerHealthUI playerHealthUI;

    [Header("Settings")]
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }
    public void Damage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            playerHealthUI.AnimationDamage();
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }
    void Die()
    {
        GameManager.Instance.GetGameOver();
    }
}
