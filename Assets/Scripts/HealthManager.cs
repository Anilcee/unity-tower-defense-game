using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI healthText;
    void Start()
    {
        healthText.text="Health : "+health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health-=damage;
        healthText.text="Health : "+health;
        if(health<=0)
        {
            Debug.Log("a");
            SceneManager.LoadScene("GameOver");
        }
    }

}
