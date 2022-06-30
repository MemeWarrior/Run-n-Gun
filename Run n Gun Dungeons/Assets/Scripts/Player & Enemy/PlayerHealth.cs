using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int health;

    public SpriteRenderer playerSr;
    public PlayerManagerSky playerManagerSky;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            playerSr.enabled = false;
            playerManagerSky.enabled = false;
        }
        
    }
}
