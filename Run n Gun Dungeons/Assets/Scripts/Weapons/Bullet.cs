using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public float arrowDistance;

    private Rigidbody2D rg2d;
    private Vector3 startPos;

    //private CameraShake shake;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        rg2d.velocity = transform.right * speed;
        startPos = transform.position;

        //shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<CameraShake>();
    }

    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > arrowDistance)
        {
            //Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            //shake.CamShake();
            //Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            other.GetComponent<Enemy>().TakeDamage(damage);
            Debug.Log("Hit something");
        }
        else if(other.gameObject.CompareTag("Object"))
        {
            if(other.GetComponent<ObjectScript>().isBreakable)
            {
                Destroy(gameObject);
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
}
