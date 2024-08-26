using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int health = 100;
    public GameObject deathPanel; 
    public TextMeshProUGUI healthText; 

    void Start()
    {
        UpdateHealthUI(); 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHealthUI(); //hasar güncelle

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        deathPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + health.ToString();
    }
}
