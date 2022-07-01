using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{
    public Collider2D target;
    public bool OnPlayer = false;
    public bool PlayerClicked = false;
    private GameObject canvas;

    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
    }

    void Update(){
        if(OnPlayer && Input.GetKeyDown(KeyCode.E))
        {
            PlayerClicked = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            target = other;
            OnPlayer = true;
            canvas.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnPlayer = false;
        canvas.transform.GetChild(2).gameObject.SetActive(false);
    }
}
