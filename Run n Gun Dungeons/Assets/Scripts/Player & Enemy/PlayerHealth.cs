using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    public int health;

    public SpriteRenderer playerSr;
    public PlayerManagerSky playerManagerSky;
    int timer = 0;
    public bool invincible = false;
    public TextMeshProUGUI gameOver;
    public Image gameOverBlack;

    Material mWhite;
    Material mDefault;
    SpriteRenderer sRend;
    // Start is called before the first frame update

    void Start()
    {
        health = maxHealth;
        sRend = GetComponent<SpriteRenderer>();
        mDefault = sRend.material;
        mWhite = Resources.Load("mWhite", typeof(Material)) as Material;
        gameOver.gameObject.SetActive(false);
        gameOverBlack.gameObject.SetActive(false);
    }

    void Update()
    {

    }
    public void TakeDamage(int amount)
    {
        Debug.Log(amount);
        if(!invincible)
        {
            health -= amount;
        }
        else
        {
            health -= 0;
        }


        if (health <= 0)
        {
            playerSr.enabled = false;
            playerManagerSky.enabled = false;
            gameOverBlack.gameObject.SetActive(true);
            gameOver.gameObject.SetActive(true);
            StartCoroutine("ReturnToMenu");
        }

    }

    IEnumerator InvinciblePeriod()
    {

        invincible = true;
        yield return new WaitForSeconds(1.5f);
        invincible = false;
    }

    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {

            if(!invincible)
            {
                //Debug.Log("hit");
                //TakeDamage(1);
                invincible = true;
                StartCoroutine("Flash");
                StartCoroutine("InvinciblePeriod");
            }

        }
    }
    void Recalc()
    {
        StartCoroutine("Flash");
        StartCoroutine("InvinciblePeriod");
    }

    IEnumerator Flash()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.25f);
            sRend.material = mWhite;
            Invoke("ResetMaterial", 0.23f);
        }
    }

    void ResetMaterial()
    {
        sRend.material = mDefault;
    }

}
