using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public int health;
    public Vector2Int size;
    public bool isBreakable = false;
    public bool invcincibility = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && isBreakable)
        {
            if(gameObject.name == "Object1(Clone)") //Mush
            {
                Instantiate(Resources.Load("MushRemains"), transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if(gameObject.name == "Object2(Clone)") //Fossil
            {
                Instantiate(Resources.Load("FossilRemains"), transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            if(gameObject.name == "Object3(Clone)") //Wood
            {
                Instantiate(Resources.Load("WoodRemains"), transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
