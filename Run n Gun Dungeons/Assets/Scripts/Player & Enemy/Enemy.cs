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
    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(health <= 0)
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

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(body.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (body.velocity.x > 0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (body.velocity.x < -0.1f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
