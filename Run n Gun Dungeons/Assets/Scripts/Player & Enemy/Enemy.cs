using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Transform player;
    public float lineOfSite;
    public float AttackingRange;
    public int health;


    void Start()
    {
        Invoke("Chase", 5);
    }

    public void Chase()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
 
        if (health <= 0)
        {
            Instantiate(Resources.Load("DeadSnake"), transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > AttackingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, AttackingRange);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
