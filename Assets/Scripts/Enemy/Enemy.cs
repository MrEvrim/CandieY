using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 50;

    private GameHandler gameHandler; 

    void Start()
    {
        gameHandler = FindObjectOfType<GameHandler>();

        if (gameHandler == null)
        {
            Debug.LogWarning("No GameHandler found in the scene.");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died");

        if (gameHandler != null)
        {
            gameHandler.IncrementKillCount();
        }
        Destroy(gameObject);
    }
}
