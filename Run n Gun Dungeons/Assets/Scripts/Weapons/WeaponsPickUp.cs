using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPickUp : MonoBehaviour
{
    public GameObject weapon;
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.CompareTag("Player"))
        {
            target.gameObject.GetComponent<PlayerManagerSky>().currentweapon = weapon;
            target.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
            Destroy(gameObject);
        }
    }
}
