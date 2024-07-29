using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Obsctacle : MonoBehaviour
{
    public float  startHealth=40;
    public Image healthBar;
    public float currentHealth;
    public GameObject ObstacleHitEffect;
    
    void Start()
    {
        currentHealth=startHealth;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy =other.gameObject.GetComponent<Enemy>();
            if(enemy.isBoss)
            {
                Destroy(gameObject);
                GameObject obstacleHitEffectİns=(GameObject) Instantiate(ObstacleHitEffect,transform.position,transform.rotation); 
                Destroy(obstacleHitEffectİns,0.2f);
            }
            else
            {
            EnemyWaweManager enemyWaweManager = FindObjectOfType<EnemyWaweManager>();
            enemyWaweManager.enemyInstances.Remove(other.gameObject);
            Destroy(other.gameObject);
            currentHealth-=enemy.currentHealth;
            healthBar.fillAmount=currentHealth/startHealth;
            GameObject obstacleHitEffectİns=(GameObject) Instantiate(ObstacleHitEffect,transform.position,transform.rotation);
            Destroy(obstacleHitEffectİns,0.2f);
            if(currentHealth<=0)
            {
                Destroy(gameObject);
            }
            }
        }
    }
}
