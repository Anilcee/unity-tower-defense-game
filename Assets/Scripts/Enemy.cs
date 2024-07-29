using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startHealth = 15; 
    public Image healthBar;
    public float currentHealth;
    public float speed;

    public bool isBoss=false;
    void Start()
    {
        currentHealth=startHealth;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("kalan can :"+currentHealth);
        currentHealth -= damage;
        healthBar.fillAmount=currentHealth/startHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    
    void Die()
    {
        EnemyWaweManager enemyWaweManager = FindObjectOfType<EnemyWaweManager>();
        MoneyManager moneyManager=FindObjectOfType<MoneyManager>();
        moneyManager.EarnMoney(5);
        enemyWaweManager.enemyInstances.Remove(gameObject);
        Destroy(gameObject);

    }
}

