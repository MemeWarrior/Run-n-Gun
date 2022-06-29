using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPickUp : MonoBehaviour
{
    public GameObject weapon;
    public Collider2D target;
    public bool OnPlayer = false;

    void Update(){
        if(OnPlayer && Input.GetKeyDown(KeyCode.E)){
            target.gameObject.GetComponent<PlayerManagerSky>().currentweapon = weapon;
                target.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            target = other;
            OnPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnPlayer = false;
    }
}
